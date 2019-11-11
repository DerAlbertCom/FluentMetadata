using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentMetadata.Builder
{
    internal class PropertyNameMetadataBuilder
    {
        private const int MaxLevel = 5;
        private readonly Type modelType;
        private int currentLevel;

        internal PropertyNameMetadataBuilder(Type modelType) { this.modelType = modelType; }

        internal IEnumerable<NameMetaData> NamedMetaData => GetNamedMetaData(modelType, string.Empty);

        private IEnumerable<NameMetaData> GetNamedMetaData(Type type, string prefix)
        {
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var metadata = QueryFluentMetadata.FindMetadataFor(type, propertyInfo.Name);

                if (metadata != null)
                {
                    yield return new NameMetaData(prefix + propertyInfo.Name, metadata);
                }

                if (!IsSimpleType(propertyInfo.PropertyType))
                {
                    currentLevel++;

                    if (currentLevel <= MaxLevel)
                    {
                        foreach (var named in GetNamedMetaData(propertyInfo.PropertyType, prefix + propertyInfo.Name))
                        {
                            yield return named;
                        }
                    }

                    currentLevel--;
                }
            }
        }

        private static bool IsSimpleType(Type type)
        {
            if (type.IsValueType || type == typeof(string))
            {
                return true;
            }

            return !type.IsClass;
        }

        internal class NameMetaData
        {
            internal string PropertyName { get; private set; }
            internal Metadata Metadata { get; private set; }

            internal NameMetaData(string propertyName, Metadata metadata)
            {
                PropertyName = propertyName;
                Metadata = metadata;
            }

            //for easy debugging
            public override string ToString() { return PropertyName; }
        }
    }
}