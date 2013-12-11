using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    public static class FluentMetadataBuilder
    {
        readonly static IDictionary<Type, TypeMetadataBuilder> typeBuilders = new Dictionary<Type, TypeMetadataBuilder>();
        internal readonly static List<Type> BuiltMetadataDefininitions = new List<Type>();
        readonly static MetadataDefinitionSorter metadataDefinitionSorter = new MetadataDefinitionSorter();

        public static void Reset()
        {
            typeBuilders.Clear();
            metadataDefinitionSorter.Clear();
            BuiltMetadataDefininitions.Clear();
        }

        internal static TypeMetadataBuilder GetTypeBuilder(Type type)
        {
            if (type == null)
                return null;
            TypeMetadataBuilder builder;
            if (!typeBuilders.TryGetValue(type, out builder))
            {
                builder = typeof(TypeMetadataBuilder<>).CreateGenericInstance(type) as TypeMetadataBuilder;
                typeBuilders[type] = builder;
                builder.Init();
            }
            return builder;
        }

        internal static TypeMetadataBuilder<T> GetTypeBuilder<T>()
        {
            return (TypeMetadataBuilder<T>)GetTypeBuilder(typeof(T));
        }

        internal static void RegisterDependency(Type dependency, Type depender)
        {
            metadataDefinitionSorter.Register(dependency, depender);
        }

        public static void ForAssemblyOfType<T>()
        {
            ForAssembly(typeof(T).Assembly);
        }

        public static void ForAssembly(Assembly assembly)
        {
            BuildMetadataDefinitions(assembly.GetTypes());
        }

        internal static void BuildMetadataDefinitions(IEnumerable<Type> metadataDefinitionsToBuild)
        {
            metadataDefinitionSorter.AddMetadataDefinitionsToSort(
                metadataDefinitionsToBuild.Where(t => t.Is<IClassMetadata>()));

            List<Type> metadataDefinitions;
            while ((metadataDefinitions = metadataDefinitionSorter
                .GetNextUnbuiltDefinitions(BuiltMetadataDefininitions)).Count > 0)
            {
                metadataDefinitions
                    .ForEach(mdd => CreateMetadataDefinitionInstance(mdd));
            }
        }

        static void CreateMetadataDefinitionInstance(Type type)
        {
            BuiltMetadataDefininitions.Add(type);

            if (type.IsAbstract)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The type '{0}' may not abstract. Use generic classes for inheritance.",
                        type.FullName));
            }
            if (type.ContainsGenericParameters)
            {
                CreateWithGenericParameters(type);
            }
            else
            {
                try
                {
                    Activator.CreateInstance(type);
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException is MetadataDefinitionSorter.NoMetadataDefinedException ?
                        ex.InnerException :
                        ex;
                }
            }
        }

        static void CreateWithGenericParameters(Type type)
        {
            var constraints = new List<Type>();
            foreach (var genericArgument in type.GetGenericArguments())
            {
                constraints.Add(genericArgument.GetGenericParameterConstraints()[0]);
            }

            var genericType = type.MakeGenericType(constraints.ToArray());
            var constructors = genericType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            var invoked = false;
            foreach (var constructorInfo in constructors)
            {
                if (constructorInfo.GetParameters().Length == 0)
                {
                    constructorInfo.Invoke(BindingFlags.NonPublic | BindingFlags.Public, null, new object[0], CultureInfo.CurrentCulture);
                    invoked = true;
                }
            }
            if (!invoked)
            {
                throw new InvalidOperationException("No Constructor without parameters on  " + type.FullName);
            }
        }
    }
}