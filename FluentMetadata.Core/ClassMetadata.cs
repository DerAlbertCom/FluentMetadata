using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentMetadata
{
    public abstract class ClassMetadata
    {
    }

    public abstract class ClassMetadata<T> : ClassMetadata
    {
        protected IProperty The(Expression<Func<T, object>> expression)
        {
            TypeMetadataBuilder<T> builder = GetBuilder<T>();

            return builder.MapProperty(expression);
        }

        protected IProperty The(T value)
        {
            TypeMetadataBuilder<T> builder = GetBuilder<T>();
            return builder.MapEnum(value, typeof (T));
        }

        protected void CopyMetadataFrom<TBaseType>()
        {
            TypeMetadataBuilder<T> builder = GetBuilder<T>();
            
            var nameBuilder = new PropertyNameMetadataBuilder(typeof (TBaseType));

            foreach (var namedMetaData in nameBuilder.NamedMetaData)
            {
                PropertyInfo propertyInfo = typeof (T).GetProperty(namedMetaData.PropertyName);
                if (propertyInfo != null)
                {
                    builder.MapProperty(typeof (T), propertyInfo.Name, namedMetaData.MetaData);
                }
            }
        }

        private static TypeMetadataBuilder<TBuilder> GetBuilder<TBuilder>()
        {
            return FluentMetadataBuilder.GetTypeBuilder<TBuilder>();
        }
    }
}