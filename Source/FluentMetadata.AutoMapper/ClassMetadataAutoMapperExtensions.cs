using System;

namespace FluentMetadata.AutoMapper
{
    /// <summary>
    /// AutoMapper extensions to <see cref="FluentMetadata.ClassMetadata&lt;T>"/>
    /// </summary>
    public static class ClassMetadataAutoMapperExtensions
    {
        /// <summary>
        /// Copies the the source type's metadata to the destination type's metadata
        /// using the mapping information provided by AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="to">The destination metadata.</param>
        /// <param name="from">The source type.</param>
        public static void CopyAutoMappedMetadataFrom<TDestination>(this ClassMetadata<TDestination> to, Type from)
        {
            var destinationType = typeof(TDestination);
            MetadataHelper.CopyMappedMetadata(
                from,
                destinationType,
                AutoMapperHelper.GetMemberMapsOf(from, destinationType)
            );
        }
    }
}