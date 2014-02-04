using System;
using System.Linq.Expressions;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    internal interface IClassMetadata
    {
    }

    public abstract class ClassMetadata<T> : IClassMetadata
    {
        protected IClassBuilder<T> Class
        {
            get
            {
                return GetTypeBuilder<T>().ClassBuilder();
            }
        }

        protected IProperty<T, TResult> Property<TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetTypeBuilder<T>().MapProperty(expression);
        }

        protected void CopyMetadataFrom<TBaseType>()
        {
            MetadataHelper.CopyMetadata(typeof(TBaseType), typeof(T));
        }

        static TypeMetadataBuilder<TBuilder> GetTypeBuilder<TBuilder>()
        {
            return FluentMetadataBuilder.GetTypeBuilder<TBuilder>();
        }
    }
}