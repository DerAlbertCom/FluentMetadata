using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentMetadata
{
    public static class FluentMetadataBuilder
    {
        private static readonly Dictionary<Type, TypeMetadataBuilder> metaData =
            new Dictionary<Type, TypeMetadataBuilder>();

        public static void Reset()
        {
            metaData.Clear();
        }

        public static TypeMetadataBuilder GetTypeBuilder(Type type)
        {
            TypeMetadataBuilder data;
            metaData.TryGetValue(type, out data);
            return data;
        }

        public static TypeMetadataBuilder<T> GetTypeBuilder<T>()
        {
            TypeMetadataBuilder builder = GetTypeBuilder(typeof (T));
            if (builder == null)
            {
                builder = new TypeMetadataBuilder<T>();
                metaData[typeof (T)] = builder;
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
            var types = GetPublicClassMetadataTypes(assembly);
            foreach (var type in types)
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