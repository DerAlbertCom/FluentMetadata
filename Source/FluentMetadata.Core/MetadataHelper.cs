using System;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    public static class MetadataHelper
    {
        private static QueryFluentMetadata query = new QueryFluentMetadata();
        public static void CopyMetadata(Type from, Type to)
        {
            var toBuilder = FluentMetadataBuilder.GetTypeBuilder(to);
            var nameBuilder = new PropertyNameMetadataBuilder(from);

            foreach (var namedMetaData in nameBuilder.NamedMetaData)
            {
                var propertyInfo = to.GetProperty(namedMetaData.PropertyName);
                if (propertyInfo != null)
                {
                    toBuilder.MapProperty(to, namedMetaData.PropertyName, namedMetaData.Metadata);
                }
            }
            query.GetMetadataFor(to).CopyMetaDataFrom(query.GetMetadataFor(from));
        }

        public static void CopyMetadataFrom<T, TBaseType>()
        {
            CopyMetadata(typeof (T), typeof (TBaseType));
        }
    }
}