using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentMetadata
{
    public static class FluentMetadataBuilder
    {
        private static readonly Dictionary<Type, TypeMetadataBuilder> MetaData =
            new Dictionary<Type, TypeMetadataBuilder>();

        public static void Reset()
        {
            MetaData.Clear();
        }

        public static TypeMetadataBuilder GetTypeBuilder(Type type)
        {
            if (type == null)
                return null;
            TypeMetadataBuilder builder;
            MetaData.TryGetValue(type, out builder);
            if (builder==null)
            {
                builder = (TypeMetadataBuilder) typeof (TypeMetadataBuilder<>).CreateGenericInstance(type);
                MetaData[type] = builder;
            }
            return builder;
        }

        public static TypeMetadataBuilder<T> GetTypeBuilder<T>()
        {
            TypeMetadataBuilder builder = GetTypeBuilder(typeof (T));
            if (builder == null)
            {
                builder = new TypeMetadataBuilder<T>();
                MetaData[typeof (T)] = builder;
            }
            return (TypeMetadataBuilder<T>) builder;
        }

        public static void ForAssembly(string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            ForAssembly(assembly);
        }

        public static void ForAssemblyOfType<T>()
        {
            ForAssembly(typeof (T).Assembly);
        }

        private static void ForAssembly(Assembly assembly)
        {
            IEnumerable<Type> types = GetPublicClassMetadataTypes(assembly);
            foreach (Type type in types)
            {
                Activator.CreateInstance(type);
            }
        }

        private static IEnumerable<Type> GetPublicClassMetadataTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => typeof (ClassMetadata).IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}