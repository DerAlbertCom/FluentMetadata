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
        private static readonly Dictionary<Type, TypeMetadataBuilder> TypeBuilders =
            new Dictionary<Type, TypeMetadataBuilder>();

        public static void Reset()
        {
            TypeBuilders.Clear();
        }

        internal static TypeMetadataBuilder GetTypeBuilder(Type type)
        {
            if (type == null)
                return null;
            TypeMetadataBuilder builder;
            if (!TypeBuilders.TryGetValue(type, out builder))
            {
                builder = (TypeMetadataBuilder)typeof(TypeMetadataBuilder<>).CreateGenericInstance(type);
                TypeBuilders[type] = builder;
                builder.Init();
            }
            return builder;
        }

        internal static TypeMetadataBuilder<T> GetTypeBuilder<T>()
        {
            return (TypeMetadataBuilder<T>)GetTypeBuilder(typeof(T));
        }

        public static void ForAssemblyOfType<T>()
        {
            ForAssembly(typeof(T).Assembly);
        }

        public static void ForAssembly(Assembly assembly)
        {
            foreach (Type type in PublicMetadataDefinitionsFrom(assembly))
            {
                if (type.IsAbstract)
                {
                    throw new InvalidOperationException("The " + type.FullName + " may not abstract");
                }
                if (type.ContainsGenericParameters)
                {
                    CreateWithGenericParameters(type);
                }
                else
                {
                    Activator.CreateInstance(type);
                }
            }
        }

        private static void CreateWithGenericParameters(Type type)
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

        private static IEnumerable<Type> PublicMetadataDefinitionsFrom(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => typeof(IClassMetadata).IsAssignableFrom(t));
        }
    }
}