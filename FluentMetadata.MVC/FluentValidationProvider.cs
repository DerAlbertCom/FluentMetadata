using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    public class FluentValidationProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            TypeMetadataBuilder builder = FluentMetadataBuilder.GetTypeBuilder(metadata.ContainerType);
            if (builder == null)
            {
                yield break;
            }
            var data = builder.MetaDataFor(metadata.PropertyName);
            if (data == null)
            {
                yield break;
            }
            foreach (var rule in data.Rules)
            {
                yield return new RuleModelValidator(rule, metadata, context);                
            }
        }
    }
}