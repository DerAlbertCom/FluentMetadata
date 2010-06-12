using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public abstract class TypeMetadataBuilder
    {
        protected readonly List<PropertyMetadataBuilder> PropertyBuilders = new List<PropertyMetadataBuilder>();
        private readonly MetaData metaData = new MetaData();

        private IEnumerable<MetaData> MetaDataProperties
        {
            get { return from p in PropertyBuilders select p.MetaData; }
        }

        public MetaData MetaData
        {
            get { return metaData; }
        }

        public MetaData MetaDataFor(string propertyName)
        {
            return MetaDataProperties.SingleOrDefault(md => md.PropertyName == propertyName);
        }

        protected bool TryGetPropertyBuilder(string propertyName, out PropertyMetadataBuilder propertyMetadataBuilder)
        {
            propertyMetadataBuilder = PropertyBuilders.SingleOrDefault(p => p.MetaData.PropertyName == propertyName);
            return propertyMetadataBuilder != null;
        }
    }

    public class TypeMetadataBuilder<T> : TypeMetadataBuilder
    {
        public IProperty<T, TResult> MapProperty<TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetBuilder(expression);
        }

        private PropertyMetadataBuilder<T,TResult> GetBuilder<TResult>(Expression<Func<T, TResult>> expression)
        {
            string propertyName = ExpressionHelper.GetPropertyName(expression);

            PropertyMetadataBuilder propertyBuilder;
            if (!TryGetPropertyBuilder(propertyName, out propertyBuilder))
            {
                propertyBuilder = new PropertyMetadataBuilder<T, TResult>(expression);
                PropertyBuilders.Add(propertyBuilder);
            }
            return (PropertyMetadataBuilder<T, TResult>) propertyBuilder;
        }

        public void MapProperty(Type containerType, string propertyName, MetaData metaData)
        {
            var newMetaData = new MetaData(metaData, containerType) {PropertyName = propertyName};
            PropertyMetadataBuilder propertyBuilder;
            if (!TryGetPropertyBuilder(propertyName, out propertyBuilder))
            {
                propertyBuilder = CreatePropertyMetaDataBuilder(metaData, containerType, newMetaData);
                PropertyBuilders.Add(propertyBuilder);
            }
        }

        private PropertyMetadataBuilder CreatePropertyMetaDataBuilder(MetaData metaData, Type containerType,
                                                                      MetaData newMetaData)
        {
            return (PropertyMetadataBuilder) typeof (PropertyMetadataBuilder<,>)
                                                 .CreateGenericInstance(containerType, metaData.ModelType, newMetaData);
        }

        public IProperty<T, TResult> MapEnum<TResult>(object value)
        {
            string propertyName = Enum.GetName(typeof (TResult), value);
            PropertyMetadataBuilder builder;
            if (!TryGetPropertyBuilder(propertyName, out builder))
            {
                builder = new PropertyMetadataBuilder<T, TResult>(propertyName);
                PropertyBuilders.Add(builder);
            }
            return (IProperty<T, TResult>) builder;
        }
    }
}