using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMetadata.Builder;

namespace FluentMetadata
{
    public static class MetadataHelper
    {
        static QueryFluentMetadata query = new QueryFluentMetadata();

        public static void CopyMetadata(Type from, Type to)
        {
            var toBuilder = FluentMetadataBuilder.GetTypeBuilder(to);
            var nameBuilder = new PropertyNameMetadataBuilder(from);
            //copy property metadata
            foreach (var namedMetaData in nameBuilder.NamedMetaData)
            {
                var propertyInfo = to.GetProperty(namedMetaData.PropertyName);
                if (propertyInfo != null)
                {
                    toBuilder.MapProperty(to, namedMetaData.PropertyName, namedMetaData.Metadata);
                }
            }
            //copy type metadata
            query.GetMetadataFor(to).CopyMetaDataFrom(query.GetMetadataFor(from));
        }

        public static void CopyMetadataFrom<T, TBaseType>()
        {
            CopyMetadata(typeof(T), typeof(TBaseType));
        }

        public static void CopyMappedMetadata(Type from, Type to, IEnumerable<MemberMap> memberMaps)
        {
            var toBuilder = FluentMetadataBuilder.GetTypeBuilder(to);
            var fromBuilder = new PropertyNameMetadataBuilder(from);
            //copy property metadata
            foreach (var fromMetaData in fromBuilder.NamedMetaData)
            {
                var memberMap = memberMaps.SingleOrDefault(mm => mm.Source.Name == fromMetaData.PropertyName);
                if (memberMap != null)
                {
                    toBuilder.MapProperty(to, memberMap.Destination.Name, fromMetaData.Metadata);
                }
            }
            //copy type metadata
            query.GetMetadataFor(to).CopyMetaDataFrom(query.GetMetadataFor(from));
        }
    }

    public class MemberMap
    {
        public MemberInfo Source { get; set; }
        public MemberInfo Destination { get; set; }
    }
}