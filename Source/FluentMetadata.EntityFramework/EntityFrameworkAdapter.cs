using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Internal.ConfigurationAdapters;

namespace FluentMetadata.EntityFramework
{
    public class EntityFrameworkAdapter
    {
        private readonly QueryFluentMetadata query = new QueryFluentMetadata();
        private readonly ExpressionGenerator generator = new ExpressionGenerator();
        private readonly PropertyMethodMapping methodMapping = new PropertyMethodMapping();

        private readonly ConfigurationAdapterFactory factory = new ConfigurationAdapterFactory();

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
            var metaDatas = query.GetMetadataFor(instanceType).Properties;

            foreach (var data in metaDatas)
            {
                if (data.ModelName == null)
                {
                    continue;
                }
                if (!data.StringLength.HasValue && !data.Required.HasValue)
                {
                    continue;
                }
                var methodInfo = methodMapping.GetPropertyMappingMethod(configuration.GetType(), instanceType,
                                                                        data.ModelType);
                if (methodInfo == null)
                {
                    continue;
                }

                var lambda = generator.CreateExpressionForProperty(instanceType, data.ModelName);
                if (lambda != null)
                {
                    var propertyConfiguration = (PropertyConfiguration) methodInfo.Invoke(configuration, new[] {lambda});

                    factory.Create(propertyConfiguration).Convert(data, propertyConfiguration);
                }
            }
        }
    }
}