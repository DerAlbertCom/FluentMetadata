using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentMetadata.Builder
{
    internal abstract class TypeMetadataBuilder
    {
        protected readonly List<PropertyMetadataBuilder> PropertyBuilders = new List<PropertyMetadataBuilder>();

        readonly Metadata metadata = new Metadata();
        public Metadata Metadata
        {
            get { return metadata; }
        }

        public Metadata MetaDataFor(string propertyName)
        {
            return PropertyBuilders
                .Select(p => p.Metadata)
                .SingleOrDefault(md => md.ModelName == propertyName);
        }

        protected bool TryGetPropertyBuilder(string propertyName, out PropertyMetadataBuilder propertyMetadataBuilder)
        {
            propertyMetadataBuilder = PropertyBuilders.SingleOrDefault(p => p.Metadata.ModelName == propertyName);
            return propertyMetadataBuilder != null;
        }

        public abstract Metadata MapProperty(Type containerType, string propertyName, Metadata metadata);

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
        public IProperty<T, TResult> MapProperty<TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetBuilder(expression);
        }

        PropertyMetadataBuilder<T, TResult> GetBuilder<TResult>(Expression<Func<T, TResult>> expression)
        {
            PropertyMetadataBuilder propertyBuilder;
            if (!TryGetPropertyBuilder(ExpressionHelper.GetPropertyName(expression), out propertyBuilder))
            {
                propertyBuilder = new PropertyMetadataBuilder<T, TResult>(expression);
                PropertyBuilders.Add(propertyBuilder);
            }
            return (PropertyMetadataBuilder<T, TResult>)propertyBuilder;
        }

        public override Metadata MapProperty(Type containerType, string propertyName, Metadata metadata)
        {
            PropertyMetadataBuilder propertyBuilder;
            if (!TryGetPropertyBuilder(propertyName, out propertyBuilder))
            {
                propertyBuilder = CreatePropertyMetaDataBuilder(
                    metadata,
                    containerType,
                    new Metadata(metadata, containerType)
                    {
                        ModelName = propertyName
                    });
                PropertyBuilders.Add(propertyBuilder);
            }
            propertyBuilder.Metadata.CopyMetaDataFrom(metadata);
            return propertyBuilder.Metadata;
        }

        public override void Init()
        {
            ClassBuilder();
        }

        PropertyMetadataBuilder CreatePropertyMetaDataBuilder(Metadata metadata, Type containerType, Metadata newMetadata)
        {
            return typeof(PropertyMetadataBuilder<,>)
                .CreateGenericInstance(containerType, metadata.ModelType, newMetadata) as PropertyMetadataBuilder;
        }

        IClassBuilder<T> classBuilder;
        public IClassBuilder<T> ClassBuilder()
        {
            if (classBuilder == null)
            {
                classBuilder = new ClassMetadataBuilder<T>(Metadata);
            }
            return classBuilder;
        }
    }
}