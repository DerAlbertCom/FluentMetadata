using System;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    internal static class MetadataHelper
    {
        static QueryFluentMetadata query = new QueryFluentMetadata();

        internal static void CopyMetadata(Type from, Type to)
        {
            var toBuilder = FluentMetadataBuilder.GetTypeBuilder(to);
            var nameBuilder = new PropertyNameMetadataBuilder(from);

            foreach (var namedMetaData in nameBuilder.NamedMetaData)
            {
                if (to.GetProperty(namedMetaData.PropertyName) != null)
                {
                    toBuilder.MapProperty(to, namedMetaData.PropertyName, namedMetaData.Metadata);
                }
            }

            query.GetMetadataFor(to).CopyMetaDataFrom(query.GetMetadataFor(from));
        }
    }
}