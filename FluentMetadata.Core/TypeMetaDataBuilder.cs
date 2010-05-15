using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public abstract class TypeMetadataBuilder
    {
        protected readonly List<PropertyMetadataBuilder> Properties = new List<PropertyMetadataBuilder>();

        public IEnumerable<MetaData> MetaDataProperties
        {
            get { return from p in Properties select p.MetaData; }
        }

        public MetaData MetaDataFor(string propertyName)
        {
            return MetaDataProperties.FirstOrDefault(metaData => metaData.PropertyName == propertyName);
        }

        internal PropertyMetadataBuilder BuilderFor(string propertyName)
        {
            return Properties.Where(p => p.MetaData.PropertyName == propertyName).FirstOrDefault();
        }
    }

    public class TypeMetadataBuilder<T> : TypeMetadataBuilder
    {
        public IProperty MapProperty(Expression<Func<T, object>> expression)
        {
            return GetBuilder(expression);
        }

        private PropertyMetadataBuilder GetBuilder(Expression<Func<T, object>> expression)
        {
            string propertyName = ExpressionHelper.GetPropertyName(expression);

            foreach (PropertyMetadataBuilder builder in Properties)
            {
                if (builder.MetaData.PropertyName == propertyName)
                {
                    return builder;
                }
            }
            var metaDataBuilder = new PropertyMetaDataBuilder<T>(expression);
            Properties.Add(metaDataBuilder);
            return metaDataBuilder;
        }

        public void MapProperty(Type containerType, string propertyName, MetaData metaData)
        {
            var newMetaData = new MetaData(metaData, containerType) {PropertyName = propertyName};
            PropertyMetadataBuilder builder = BuilderFor(propertyName);
            if (builder == null)
            {
                builder = new PropertyMetadataBuilder(newMetaData);
                Properties.Add(builder);
            }
        }

        public IProperty MapEnum(object value, Type type)
        {
            string propertyName = Enum.GetName(type, value);

            foreach (PropertyMetadataBuilder builder in Properties)
            {
                if (builder.MetaData.PropertyName == propertyName)
                {
                    return builder;
                }
            }
            var metadataBuilder= new PropertyMetadataBuilder(type,propertyName);
            Properties.Add(metadataBuilder);
            return metadataBuilder;
        }
    }
}