using System;
using System.Collections.Generic;
using System.Linq;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    public static class MetadataHelper
    {
        readonly static QueryFluentMetadata query = new QueryFluentMetadata();

        internal static void CopyMetadata(Type source, Type target)
        {
            var targetBuilder = FluentMetadataBuilder.GetTypeBuilder(target);

            //copy property metadata
            foreach (var sourcePropertyMetaData in new PropertyNameMetadataBuilder(source).NamedMetaData)
            {
                if (target.GetProperty(sourcePropertyMetaData.PropertyName) != null)
                {
                    targetBuilder.MapProperty(target, sourcePropertyMetaData.PropertyName, sourcePropertyMetaData.Metadata);
                }
            }

            //copy type metadata
            query.GetMetadataFor(target).CopyMetaDataFrom(query.GetMetadataFor(source));
        }

        public static void CopyMappedMetadata(Type source, Type target, IEnumerable<MemberMap> memberMaps)
        {
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
            query.GetMetadataFor(target).CopyMetaDataFrom(query.GetMetadataFor(source));
        }
    }

    public class MemberMap
    {
        public string SourceName { get; set; }
        public string DestinationName { get; set; }
    }
}