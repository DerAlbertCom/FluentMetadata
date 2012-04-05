using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentMetadata.Builder
{
    class PropertyNameMetadataBuilder
    {
        const int MaxLevel = 5;
        readonly Type modelType;
        int currentLevel;

        internal PropertyNameMetadataBuilder(Type modelType)
        {
            this.modelType = modelType;
        }

        internal IEnumerable<NameMetaData> NamedMetaData
        {
            get { return GetNamedMetaData(modelType, string.Empty); }
        }

        IEnumerable<NameMetaData> GetNamedMetaData(Type type, string prefix)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (IsSimpleType(propertyInfo.PropertyType))
                {
                    var metadata = QueryFluentMetadata.FindMetadataFor(type, propertyInfo.Name);
                    if (metadata != null)
                    {
                        yield return new NameMetaData(prefix + propertyInfo.Name, metadata);
                    }
                }
                else
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

        static bool IsSimpleType(Type type)
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
        }
    }
}