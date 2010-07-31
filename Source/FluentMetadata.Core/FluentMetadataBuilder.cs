using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMetadata.Builder;

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

        public static bool HasTypeBuilder(Type type)
        {
            return MetaData.ContainsKey(type);
        }

        public static ITypeMetadataBuilder GetTypeBuilder(Type type)
        {
            if (type == null)
                return null;
            TypeMetadataBuilder builder;
            MetaData.TryGetValue(type, out builder);
            if (builder == null)
            {
                builder = (TypeMetadataBuilder) typeof (TypeMetadataBuilder<>).CreateGenericInstance(type);
                MetaData[type] = builder;
            }
            return builder;
        }

        public static ITypeMetadataBuilder<T> GetTypeBuilder<T>()
        {
            return (ITypeMetadataBuilder<T>) GetTypeBuilder(typeof (T));
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