using System.Data.Entity.ModelConfiguration.Configuration;
using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Internal.ConfigurationAdapters;

namespace FluentMetadata.EntityFramework
{
    public class EntityFrameworkAdapter
    {
        readonly ConfigurationAdapterFactory factory = new ConfigurationAdapterFactory();

        internal void MapProperties<T>(StructuralTypeConfiguration<T> configuration) where T : class
        {
            var metaDatas = QueryFluentMetadata.GetMetadataFor(typeof(T)).Properties;

            foreach (var data in metaDatas)
            {
                if (data.ModelName == null)
                {
                    continue;
                }
                if (!data.GetMaximumLength().HasValue && !data.Required.HasValue)
                {
                    continue;
                }
                var methodInfo = PropertyMethodMapping.GetPropertyMappingMethod(
                    configuration.GetType(),
                    typeof(T),
                    data.ModelType);
                if (methodInfo == null)
                {
                    continue;
                }

                var lambda = ExpressionGenerator.CreateExpressionForProperty(typeof(T), data.ModelName);
                if (lambda != null)
                {
                    var propertyConfiguration = (PrimitivePropertyConfiguration)methodInfo.Invoke(configuration, new[] { lambda });

                    factory.Create(propertyConfiguration).Convert(data, propertyConfiguration);
                }
            }
        }
    }
}