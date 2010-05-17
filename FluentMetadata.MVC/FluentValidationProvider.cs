using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    /* Maybe not needed, need some checks before deleting
     * 
    public class FluentValidationProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            var builder = FluentMetadataBuilder.GetTypeBuilder(metadata.ContainerType);
            if (builder == null)
            {
                yield break;
            }
            var metaData = builder.MetaDataFor(metadata.PropertyName);
            if (metaData == null)
            {
                yield break;
            }
            foreach (var rule in metaData.Rules)
            {
                yield return new RuleModelValidator(rule, metadata, context);                
            }
        }
    }
     * */
}