using System;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    public class FluentModelMetadata : ModelMetadata
    {
        internal readonly Metadata Metadata;

        public FluentModelMetadata(Metadata metadata, ModelMetadataProvider provider, Func<object> modelAccessor)
            : base(provider, metadata.ContainerType, modelAccessor, metadata.ModelType, metadata.ModelName)
        {
            Metadata = metadata;
            MetadataMapper.CopyMetadata(metadata, this);
        }
    }
}