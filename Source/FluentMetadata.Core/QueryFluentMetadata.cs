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

        public Metadata GetMetadataFor(Type type, string propertyName)
        {
            var metadata = GetMetadataFor(type);
            if (!metadata.Properties.Contains(propertyName))
            {
                throw new ArgumentOutOfRangeException("propertyName","Unknow Property");
            }
            return metadata.Properties[propertyName];
        }
    }
}