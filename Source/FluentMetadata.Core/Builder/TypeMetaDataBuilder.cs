using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentMetadata.Builder
{
    internal abstract class TypeMetadataBuilder
    {
        protected readonly List<PropertyMetadataBuilder> PropertyBuilders = new List<PropertyMetadataBuilder>();

        private readonly Metadata metadata = new Metadata();

        private IEnumerable<Metadata> MetaDataProperties
        {
            get { return from p in PropertyBuilders select p.Metadata; }
        }

        public Metadata Metadata
        {
            get { return metadata; }
        }

        public Metadata MetaDataFor(string propertyName)
        {
            return MetaDataProperties.SingleOrDefault(md => md.ModelName == propertyName);
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

        public TypeMetadataBuilder()
        {
        }

        private PropertyMetadataBuilder<T, TResult> GetBuilder<TResult>(Expression<Func<T, TResult>> expression)
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

        public override Metadata MapProperty(Type containerType, string propertyName, Metadata metadata)
        {
            PropertyMetadataBuilder propertyBuilder;
            if (!TryGetPropertyBuilder(propertyName, out propertyBuilder))
            {
                var newMetaData = new Metadata(metadata, containerType) { ModelName = propertyName };
                propertyBuilder = CreatePropertyMetaDataBuilder(metadata, containerType, newMetaData);
                PropertyBuilders.Add(propertyBuilder);
            }
            propertyBuilder.Metadata.CopyMetaDataFrom(metadata);
            return propertyBuilder.Metadata;
        }

        public override void Init()
        {
            ClassBuilder();
        }

        private PropertyMetadataBuilder CreatePropertyMetaDataBuilder(Metadata metadata, Type containerType,
                                                                      Metadata newMetadata)
        {
            return (PropertyMetadataBuilder) typeof (PropertyMetadataBuilder<,>)
                                                 .CreateGenericInstance(containerType, metadata.ModelType, newMetadata);
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


        private IClassBuilder<T> classBuilder;

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