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
            if (metaData.ContainsKey(type))
            {
                return metaData[type];
            }
            return null;
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

        private static void ForAssembly(Assembly assembly)
        {
            List<Type> types =
                assembly.GetTypes().Where(t => typeof (ClassMetadata).IsAssignableFrom(t) && !t.IsAbstract).
                    ToList();
            foreach (Type type in types)
            {
                Activator.CreateInstance(type);
            }
        }

        public static void ForAssemblyOfType<T>()
        {
            ForAssembly(typeof(T).Assembly);
        }
    }
}

  