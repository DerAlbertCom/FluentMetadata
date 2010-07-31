using System;
using System.Collections.Generic;
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
                builder = (TypeMetadataBuilder) typeof (TypeMetadataBuilder<>).CreateGenericInstance(type);
                TypeBuilders[type] = builder;
            }
            return builder;
        }

        internal static TypeMetadataBuilder<T> GetTypeBuilder<T>()
        {
            return (TypeMetadataBuilder<T>) GetTypeBuilder(typeof (T));
        }

        public static void ForAssemblyOfType<T>()
        {
            ForAssembly(typeof (T).Assembly);
        }

        private static void ForAssembly(Assembly assembly)
        {
            foreach (Type type in PublicMetadataDefinitionsFrom(assembly))
            {
                Activator.CreateInstance(type);
            }
        }

        private static IEnumerable<Type> PublicMetadataDefinitionsFrom(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => typeof (IClassMetadata).IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}