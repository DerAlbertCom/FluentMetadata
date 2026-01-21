using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentMetadata.Builder
{
    internal abstract class TypeMetadataBuilder
    {
        protected readonly List<PropertyMetadataBuilder> PropertyBuilders = new List<PropertyMetadataBuilder>();

        public Metadata Metadata { get; protected set; } = new Metadata();

        public Metadata MetaDataFor(string propertyName)
        {
            var propertyBuilder = GetPropertyBuilder(propertyName);
            return propertyBuilder == null ? null : propertyBuilder.Metadata;
        }

        protected bool TryGetPropertyBuilder(string propertyName, out PropertyMetadataBuilder propertyMetadataBuilder)
        {
            propertyMetadataBuilder = GetPropertyBuilder(propertyName);
            return propertyMetadataBuilder != null;
        }

        private PropertyMetadataBuilder GetPropertyBuilder(string propertyName)
        {
            return PropertyBuilders.SingleOrDefault(p => p.Metadata.ModelName == propertyName);
        }

        public abstract Metadata MapProperty(Type containerType, string propertyName, Metadata otherMetadata);

        public Metadata MapProperty(Type containerType, string propertyName, Type propertyType)
        {
            return MapProperty(
                containerType,
                propertyName,
                new Metadata
                {
                    ContainerType = containerType,
                    ModelName = propertyName,
                    ModelType = propertyType
                });
        }

        public abstract void Init();
    }

    internal class TypeMetadataBuilder<T> : TypeMetadataBuilder
    {
        private IClassBuilder<T> classBuilder;

        public IClassBuilder<T> ClassBuilder() { return classBuilder ?? (classBuilder = new ClassMetadataBuilder<T>(Metadata)); }
        public IProperty<T, TResult> MapProperty<TResult>(Expression<Func<T, TResult>> expression) { return GetBuilder(expression); }
        public override void Init() { ClassBuilder(); }

        private PropertyMetadataBuilder<T, TResult> GetBuilder<TResult>(Expression<Func<T, TResult>> expression)
        {
            if (!TryGetPropertyBuilder(ExpressionHelper.GetPropertyName(expression), out var propertyBuilder))
            {
                propertyBuilder = new PropertyMetadataBuilder<T, TResult>(expression);
                PropertyBuilders.Add(propertyBuilder);
            }

            return (PropertyMetadataBuilder<T, TResult>)propertyBuilder;
        }

        public override Metadata MapProperty(Type containerType, string propertyName, Metadata otherMetadata)
        {
            if (!TryGetPropertyBuilder(propertyName, out var propertyBuilder))
            {
                propertyBuilder = CreatePropertyMetaDataBuilder(
                    otherMetadata,
                    containerType,
                    new Metadata(otherMetadata, containerType) { ModelName = propertyName });

                PropertyBuilders.Add(propertyBuilder);
            }

            propertyBuilder.Metadata.CopyMetaDataFrom(otherMetadata);
            return propertyBuilder.Metadata;
        }

        private static PropertyMetadataBuilder CreatePropertyMetaDataBuilder(Metadata metadata, Type containerType, Metadata newMetadata)
        {
            return typeof(PropertyMetadataBuilder<,>).CreateGenericInstance(containerType, metadata.ModelType, newMetadata) as PropertyMetadataBuilder;
        }
    }
}