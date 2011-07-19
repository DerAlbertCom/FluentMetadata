using System;

namespace FluentMetadata
{
    public class QueryFluentMetadata
    {
        public Metadata GetMetadataFor(Type type)
        {
            return FluentMetadataBuilder.GetTypeBuilder(type).Metadata;
        }

        public Metadata GetMetadataFor(Type type, string propertyName)
        {
            var metadataProperties = GetMetadataFor(type).Properties;
            if (!metadataProperties.Contains(propertyName))
            {
                throw new ArgumentOutOfRangeException("propertyName", "Unknown Property");
            }
            return metadataProperties[propertyName];
        }

        public Metadata FindMetadataFor(Type type, string propertyName)
        {
            var metadataProperties = GetMetadataFor(type).Properties;
            return metadataProperties.Contains(propertyName) ?
                metadataProperties[propertyName] :
                null;
        }
    }
}