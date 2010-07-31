using System;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    public static class MetadataHelper
    {
        public static void CopyMetadata(Type from, Type to)
        {
            var fromBuilder = FluentMetadataBuilder.GetTypeBuilder(from);
            var toBuilder = FluentMetadataBuilder.GetTypeBuilder(to);
            var nameBuilder = new PropertyNameMetadataBuilder(to);
            foreach (var namedMetaData in nameBuilder.NamedMetaData)
            {
                var propertyInfo = to.GetProperty(namedMetaData.PropertyName);
                if (propertyInfo != null)
                {
                    fromBuilder.MapProperty(to, propertyInfo.Name, namedMetaData.Metadata);
                }
            }
        }

        public static void CopyMetadataFrom<T, TBaseType>()
        {
            CopyMetadata(typeof (T), typeof (TBaseType));
        }
    }
}