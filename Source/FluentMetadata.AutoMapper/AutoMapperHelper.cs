using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;

namespace FluentMetadata.AutoMapper
{
    internal class AutoMapperHelper
    {
        /// <summary>
        /// Gets the mapped members for a source/destination type pair.
        /// </summary>
        /// <param name="source">The source Type.</param>
        /// <param name="destination">The destination Type.</param>
        /// <returns></returns>
        internal static IEnumerable<MemberMap> GetMemberMapsOf(Type source, Type destination)
        {
            var maps = new Collection<MemberMap>();

            foreach (var propertyMap in GetRelevantMappedMembersOf(source, destination))
            {
                // get the source property of the PropertyMap
                var sourceValueResolvers = propertyMap.GetSourceValueResolvers()
                    // just plain property maps, there's no interesting metadata on a custom mapping function
                    .OfType<IMemberGetter>();
                var sourceProperty = sourceValueResolvers
                    // "There can be only one"
                    .SingleOrDefault(svr => svr.MemberType == propertyMap.DestinationProperty.MemberType);

                if (sourceProperty != null)
                {
                    maps.Add(new MemberMap
                    {
                        SourceName = sourceValueResolvers.Aggregate(string.Empty, (result, svr) => result + svr.Name),
                        DestinationName = propertyMap.DestinationProperty.Name
                    });
                }
            }

            return maps;
        }

        /// <summary>
        /// Gets the mapped members for a source/destination type pair
        /// leaving out mapped members that are irrelevant.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns></returns>
        static IEnumerable<PropertyMap> GetRelevantMappedMembersOf(Type source, Type destination)
        {
            var typeMap = Mapper.FindTypeMapFor(source, destination);
            return
                typeMap != null ?
                // filter by non-ignored PropertyMaps
                    typeMap.GetPropertyMaps().Where(m => m.IsIgnored() == false) :
                    Enumerable.Empty<PropertyMap>();
        }
    }
}