using System;
using System.Linq.Expressions;
using FluentMetadata.Builder;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    internal interface IClassMetadata
    {
    }

    public abstract class ClassMetadata<T> : IClassMetadata
    {
        protected ClassMetadata()
        {
            GetTypeBuilder<T>().ClassBuilder();
        }

        protected IProperty<T, TResult> Property<TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetTypeBuilder<T>().MapProperty(expression);
        }

        protected IProperty<T, TResult> Property<TResult>(T value)
        {
            return GetTypeBuilder<T>().MapEnum<TResult>(value);
        }

        protected IClassBuilder<T> Class
        {
            get { return GetTypeBuilder<T>().ClassBuilder(); }
        }

        protected void ClassRule(IClassRule<T> classRule)
        {
            var typeBuilder = GetTypeBuilder<T>();
            typeBuilder.Metadata.AddRule(classRule);
        }

        protected void CopyMetadataFrom<TBaseType>()
        {
            MetadataHelper.CopyMetadata(typeof (TBaseType), typeof (T));
        }

        private static TypeMetadataBuilder<TBuilder> GetTypeBuilder<TBuilder>()
        {
            return (TypeMetadataBuilder<TBuilder>) FluentMetadataBuilder.GetTypeBuilder<TBuilder>();
        }
    }
}