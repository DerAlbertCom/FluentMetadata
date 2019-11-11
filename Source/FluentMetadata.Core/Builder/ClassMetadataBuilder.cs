using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentMetadata.Rules;

namespace FluentMetadata.Builder
{
    internal class ClassMetadataBuilder<T> : IClassBuilder<T>
    {
        private IDisplayClass<T> displayBuilder;

        public Metadata Metadata { get; }

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

        internal ClassMetadataBuilder(Metadata metadata)
        {
            Metadata = metadata;
            metadata.ModelType = typeof(T);
            InitPropertyMetadata();
        }

        private void InitPropertyMetadata()
        {
            var builder = FluentMetadataBuilder.GetTypeBuilder(typeof(T));

            foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    Metadata.Properties.Add(
                        builder.MapProperty(
                            typeof(T),
                            propertyInfo.Name,
                            propertyInfo.PropertyType));
                }
            }
        }

        public IClassBuilder<T> AssertThat(Func<T, bool> assertFunc, string errorMessageFormat)
        {
            AssertThat(assertFunc, () => errorMessageFormat);
            return this;
        }

        public IClassBuilder<T> AssertThat(Func<T, bool> assertFunc, Func<string> errorMessageFormatFunc)
        {
            Metadata.AddRule(new GenericClassRule<T>(assertFunc, errorMessageFormatFunc));
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