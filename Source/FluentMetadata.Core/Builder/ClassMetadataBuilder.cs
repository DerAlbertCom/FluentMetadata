using System;
using System.Linq.Expressions;
using FluentMetadata.Rules;

namespace FluentMetadata.Builder
{
    class ClassMetadataBuilder<T> : IClassBuilder<T>
    {
        IDisplayClass<T> displayBuilder;
        readonly Metadata metadata;

        internal ClassMetadataBuilder(Metadata metadata)
        {
            this.metadata = metadata;
            metadata.ModelType = typeof(T);
            InitPropertyMetadata();
        }

        void InitPropertyMetadata()
        {
            var builder = FluentMetadataBuilder.GetTypeBuilder(typeof(T));
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    metadata.Properties.Add(
                        builder.MapProperty(
                            typeof(T),
                            propertyInfo.Name,
                            propertyInfo.PropertyType));
                }
            }
        }

        public IDisplayClass<T> Display
        {
            get
            {
                if (displayBuilder == null)
                {
                    displayBuilder = new DisplayBuilder<T>(this);
                }
                return displayBuilder;
            }
        }

        public Metadata Metadata
        {
            get { return metadata; }
        }

        public IClassBuilder<T> AssertThat(Func<T, bool> assertFunc, string errorMessageFormat)
        {
            AssertThat(assertFunc, () => errorMessageFormat);
            return this;
        }

        public IClassBuilder<T> AssertThat(Func<T, bool> assertFunc, Func<string> errorMessageFormatFunc)
        {
            metadata.AddRule(new GenericClassRule<T>(assertFunc, errorMessageFormatFunc));
            return this;
        }

        public IPropertiesInClassContextBuilder<T> Property(Expression<Func<T, object>> propertyExpression)
        {
            return new PropertiesInClassContextBuilder<T>(this, propertyExpression);
        }

        public IComparablePropertiesInClassContextBuilder<T> ComparableProperty(Expression<Func<T, IComparable>> propertyExpression)
        {
            return new ComparablePropertiesInClassContextBuilder<T>(this, propertyExpression);
        }
    }
}