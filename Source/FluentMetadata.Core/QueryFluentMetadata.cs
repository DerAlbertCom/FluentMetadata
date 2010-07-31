using System;

namespace FluentMetadata
{
    public class QueryFluentMetadata
    {
        public Metadata GetMetadataFor(Type type)
        {
            var builder = FluentMetadataBuilder.GetTypeBuilder(type);
            return builder.Metadata;
        }
    }
}