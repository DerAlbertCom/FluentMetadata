using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentMetadata.Builder
{
    public class PropertyNameMetadataBuilder
    {
        private const int MaxLevel = 5;

        private readonly Type modelType;
        private int currentLevel;

        public PropertyNameMetadataBuilder(Type modelType)
        {
            this.modelType = modelType;
        }

        public IEnumerable<NameMetaData> NamedMetaData
        {
            get { return GetNamedMetaData(modelType, ""); }
        }

        private IEnumerable<NameMetaData> GetNamedMetaData(Type type, string prefix)
        {
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (IsSimpleType(propertyInfo.PropertyType))
                {
                    TypeMetadataBuilder builder = FluentMetadataBuilder.GetTypeBuilder(type);
                    if (builder != null)
                    {
                        Metadata metadata = builder.MetaDataFor(propertyInfo.Name);
                        if (metadata != null)
                        {
                            yield return new NameMetaData(prefix + propertyInfo.Name, metadata);
                        }
                    }
                }
                else
                {
                    currentLevel++;
                    if (currentLevel <= MaxLevel)
                    {
                        foreach (
                            NameMetaData named in
                                GetNamedMetaData(propertyInfo.PropertyType, prefix + propertyInfo.Name))
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
            if (type.IsValueType)
            {
                return true;
            }
            if (type == typeof (string))
            {
                return true;
            }
            return !type.IsClass;
        }

        #region Nested type: NameMetaData

        public class NameMetaData
        {
            public NameMetaData(string propertyName, Metadata metadata)
            {
                PropertyName = propertyName;
                Metadata = metadata;
            }

            public string PropertyName { get; private set; }
            public Metadata Metadata { get; private set; }
        }

        #endregion
    }
}