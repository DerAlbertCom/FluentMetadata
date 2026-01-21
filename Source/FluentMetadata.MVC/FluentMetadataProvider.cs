using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    /// <summary>A custom metadata provider for FluentMetadata.</summary>
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
            var publicProps = containerType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyMetadata in QueryFluentMetadata.GetMetadataFor(containerType).Properties)
            {
                var props = publicProps.Where(p => p.Name == propertyMetadata.ModelName).ToArray();
                var prop = props.Length > 1 ? props.GetHiding() : props.SingleOrDefault();

                if (prop != null)
                {
                    yield return new FluentModelMetadata(propertyMetadata, this, () => prop.GetValue(container, null));
                }
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
        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName) => new FluentModelMetadata(QueryFluentMetadata.GetMetadataFor(containerType, propertyName), this, modelAccessor);

        /// <summary>
        /// Gets metadata for the specified model accessor and model type.
        /// </summary>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Mvc.ModelMetadata"/> object for the specified model accessor and model type.
        /// </returns>
        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType) => new FluentModelMetadata(QueryFluentMetadata.GetMetadataFor(modelType), this, modelAccessor);
    }
}