using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public abstract class TypeMetadataBuilder
    {
        protected readonly List<PropertyMetadataBuilder> Properties = new List<PropertyMetadataBuilder>();
        private readonly MetaData metaData = new MetaData();

        public IEnumerable<MetaData> MetaDataProperties
        {
            get { return from p in Properties select p.MetaData; }
        }

        public MetaData MetaData
        {
            get { return metaData; }
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
        public IProperty<T,TResult> MapProperty<TResult>(Expression<Func<T, TResult>> expression)
        {
            return (IProperty<T,TResult>)GetBuilder(expression);
        }

        private PropertyMetadataBuilder<T,TResult> GetBuilder<TResult>(Expression<Func<T, TResult>> expression)
        {
            string propertyName = ExpressionHelper.GetPropertyName(expression);

            foreach (PropertyMetadataBuilder builder in Properties)
            {
                if (builder.MetaData.PropertyName == propertyName)
                {
                    return (PropertyMetadataBuilder<T, TResult>) builder;
                }
            }
            var metaDataBuilder = new PropertyMetadataBuilder<T, TResult>(expression);
            Properties.Add(metaDataBuilder);
            return metaDataBuilder;
        }

        public void MapProperty(Type containerType, string propertyName, MetaData metaData)
        {
            var newMetaData = new MetaData(metaData, containerType) {PropertyName = propertyName};
            PropertyMetadataBuilder builder = BuilderFor(propertyName);
            if (builder == null)
            {
                var builderType = typeof(PropertyMetadataBuilder<,>).MakeGenericType(containerType, metaData.ModelType);
                builder = (PropertyMetadataBuilder) Activator.CreateInstance(builderType, newMetaData);
                Properties.Add(builder);
            }
        }

        public IProperty<T,TResult> MapEnum<TResult>(object value)
        {
            string propertyName = Enum.GetName(typeof(TResult), value);

            foreach (PropertyMetadataBuilder builder in Properties)
            {
                if (builder.MetaData.PropertyName == propertyName)
                {
                    return (IProperty<T,TResult>) builder;
                }
            }
            var metadataBuilder = new PropertyMetadataBuilder<T,TResult>(propertyName);
            Properties.Add(metadataBuilder);
            return metadataBuilder;
        }
    }
}