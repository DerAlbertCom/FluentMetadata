using System;

namespace FluentMetadata
{
    public static class QueryFluentMetadata
    {
        public static Metadata GetMetadataFor(Type type) { return FluentMetadataBuilder.GetTypeBuilder(type).Metadata; }

        public static Metadata GetMetadataFor(Type type, string propertyName)
        {
            var metadataProperties = GetMetadataFor(type).Properties;

            if (!metadataProperties.Contains(propertyName))
            {
                throw new ArgumentOutOfRangeException(nameof(propertyName), "Unknown Property");
            }

            return metadataProperties[propertyName];
        }

        public static Metadata FindMetadataFor(Type type, string propertyName) { return GetMetadataFor(type).Properties.Find(propertyName); }
    }
}