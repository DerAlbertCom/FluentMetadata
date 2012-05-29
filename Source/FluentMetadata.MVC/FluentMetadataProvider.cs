using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    /// <summary>
    /// A custom metadata provider for FluentMetadata.
    /// </summary>
    public class FluentMetadataProvider : ModelMetadataProvider
    {
        /// <summary>
        /// Gets a <see cref="T:System.Web.Mvc.ModelMetadata"/> object for each property of a model.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Mvc.ModelMetadata"/> object for each property of a model.
        /// </returns>
        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            return QueryFluentMetadata.GetMetadataFor(containerType).Properties
                .Select(md => new FluentModelMetadata(md, this, GetProperyAccessor(container, md)))
                .Cast<ModelMetadata>(); //TODO unnecessary for .NET 4
        }

        static Func<object> GetProperyAccessor(object container, Metadata metadata)
        {
            var properties = container.GetType().GetProperties().Where(p => p.Name == metadata.ModelName).ToArray();
            var count = properties.Length;
            if (count == 0)
            {
                return () => null;
            }
            else if (count == 1)
            {
                return () => properties[0].GetValue(container, null);
            }
            else
            {
                return () => properties.Single(single =>
                    properties.All(all => single.DeclaringType.Is(all.DeclaringType)));
            }
        }

        /// <summary>
        /// Gets metadata for the specified property.
        /// </summary>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="propertyName">The property to get the metadata model for.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Mvc.ModelMetadata"/> object for the property.
        /// </returns>
        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            return new FluentModelMetadata(QueryFluentMetadata.GetMetadataFor(containerType, propertyName), this, modelAccessor);
        }

        /// <summary>
        /// Gets metadata for the specified model accessor and model type.
        /// </summary>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Mvc.ModelMetadata"/> object for the specified model accessor and model type.
        /// </returns>
        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            return new FluentModelMetadata(QueryFluentMetadata.GetMetadataFor(modelType), this, modelAccessor);
        }
    }
}