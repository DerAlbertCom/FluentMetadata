using System;
using System.Linq.Expressions;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    public abstract class ClassMetadata
    {
    }

    public abstract class ClassMetadata<T> : ClassMetadata
    {
        protected IProperty<T, TResult> Property<TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetTypeBuilder<T>().MapProperty(expression);
        }

        protected IProperty<T, TResult> Property<TResult>(T value)
        {
            return GetTypeBuilder<T>().MapEnum<TResult>(value);
        }

        protected void ClassRule(IClassRule<T> classRule)
        {
            var typeBuilder = GetTypeBuilder<T>();
            typeBuilder.MetaData.AddRule(classRule);
        }

        protected void CopyMetadataFrom<TBaseType>()
        {
            GetTypeBuilder<T>().CopyMetadataFrom<T, TBaseType>();
        }

        private static TypeMetadataBuilder<TBuilder> GetTypeBuilder<TBuilder>()
        {
            return FluentMetadataBuilder.GetTypeBuilder<TBuilder>();
        }
    }
}