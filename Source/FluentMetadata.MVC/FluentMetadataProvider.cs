using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    public class FluentMetadataProvider : ModelMetadataProvider
    {
        readonly QueryFluentMetadata query = new QueryFluentMetadata();

        public FluentMetadataProvider()
        {
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            return query.GetMetadataFor(containerType).Properties
                .Select(md => new FluentModelMetadata(md, this, GetProperyAccessor(container, md)))
                .Cast<ModelMetadata>(); //TODO unnecessary for .NET 4
        }

        Func<object> GetProperyAccessor(object container, Metadata metadata)
        {
            var info = container.GetType().GetProperty(metadata.ModelName);
            if (info == null)
            {
                return () => null;
            }
            return () => info.GetValue(container, null);
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            return new FluentModelMetadata(query.GetMetadataFor(containerType, propertyName), this, modelAccessor);
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            return new FluentModelMetadata(query.GetMetadataFor(modelType), this, modelAccessor);
        }
    }
}