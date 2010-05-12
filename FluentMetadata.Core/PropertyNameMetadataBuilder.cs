using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentMetadata
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
                        MetaData metaData = builder.MetaDataFor(propertyInfo.Name);
                        if (metaData != null)
                        {
                            yield return new NameMetaData(prefix + propertyInfo.Name, metaData);
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
            public NameMetaData(string propertyName, MetaData metaData)
            {
                PropertyName = propertyName;
                MetaData = metaData;
            }

            public string PropertyName { get; private set; }
            public MetaData MetaData { get; private set; }
        }

        #endregion
    }
}