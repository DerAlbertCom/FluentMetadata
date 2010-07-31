using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using FluentMetadata.Builder;

namespace FluentMetadata.MVC
{
    public class FluentMetadataProvider : ModelMetadataProvider
    {
        private readonly QueryFluentMetadata query = new QueryFluentMetadata();

        public FluentMetadataProvider()
        {
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            Metadata classMetadata = query.GetMetadataFor(containerType);
            foreach (var metadata in classMetadata.Properties)
            {
                yield return new FluentModelMetadata(metadata, this, () => container);
            }
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType,
                                                             string propertyName)
        {
            Metadata metadata = query.GetMetadataFor(containerType).Properties[propertyName];
            return new FluentModelMetadata(metadata, this, modelAccessor);
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            var metadata = query.GetMetadataFor(modelType);
            return new FluentModelMetadata(metadata, this, modelAccessor);
        }
    }
}