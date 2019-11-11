using System;
using System.Collections.Generic;
using System.Linq;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    public static class MetadataHelper
    {
        internal static void CopyMetadata(Type source, Type target)
        {
            FluentMetadataBuilder.RegisterDependency(source, target);
            var targetBuilder = FluentMetadataBuilder.GetTypeBuilder(target);

            //copy property metadata
            foreach (var sourcePropertyMetaData in new PropertyNameMetadataBuilder(source).NamedMetaData)
            {
                if (target.GetProperties().Count(p => p.Name == sourcePropertyMetaData.PropertyName) > 0)
                {
                    targetBuilder.MapProperty(target, sourcePropertyMetaData.PropertyName, sourcePropertyMetaData.Metadata);
                }
            }

            //copy type metadata
            QueryFluentMetadata.GetMetadataFor(target).CopyMetaDataFrom(QueryFluentMetadata.GetMetadataFor(source));
        }

        public static void CopyMappedMetadata(Type source, Type target, IEnumerable<MemberMap> memberMaps)
        {
            FluentMetadataBuilder.RegisterDependency(source, target);
            var targetBuilder = FluentMetadataBuilder.GetTypeBuilder(target);

            //copy property metadata
            foreach (var sourcePropertyMetaData in new PropertyNameMetadataBuilder(source).NamedMetaData)
            {
                var memberMap = memberMaps.SingleOrDefault(mm => mm.SourceName == sourcePropertyMetaData.PropertyName);

                if (memberMap != null)
                {
                    targetBuilder.MapProperty(target, memberMap.DestinationName, sourcePropertyMetaData.Metadata);
                }
            }

            //copy type metadata
            QueryFluentMetadata.GetMetadataFor(target).CopyMetaDataFrom(QueryFluentMetadata.GetMetadataFor(source));
        }
    }

    public class MemberMap
    {
        public string SourceName { get; set; }
        public string DestinationName { get; set; }
    }
}