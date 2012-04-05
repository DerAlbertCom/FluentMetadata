using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Internal.ConfigurationAdapters;

namespace FluentMetadata.EntityFramework
{
    public class EntityFrameworkAdapter
    {
        readonly ConfigurationAdapterFactory factory = new ConfigurationAdapterFactory();

        public void MapProperties(IEnumerable<StructuralTypeConfiguration> configurations)
        {
            foreach (var configuration in configurations)
            {
                Type instanceType = configuration.GetType().GetGenericArguments()[0];
                MapProperties(instanceType, configuration);
            }
        }

        internal void MapProperties(Type instanceType, StructuralTypeConfiguration configuration)
        {
            var metaDatas = QueryFluentMetadata.GetMetadataFor(instanceType).Properties;

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
                    instanceType,
                    data.ModelType);
                if (methodInfo == null)
                {
                    continue;
                }

                var lambda = ExpressionGenerator.CreateExpressionForProperty(instanceType, data.ModelName);
                if (lambda != null)
                {
                    var propertyConfiguration = (PropertyConfiguration)methodInfo.Invoke(configuration, new[] { lambda });

                    factory.Create(propertyConfiguration).Convert(data, propertyConfiguration);
                }
            }
        }
    }
}