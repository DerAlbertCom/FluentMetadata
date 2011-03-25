using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    public class FluentValidationProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            var validators = new List<ModelValidator>();

            if (metadata is FluentModelMetadata)
            {
                var isPropertyMetadata = !string.IsNullOrEmpty(metadata.PropertyName);
                var rules = (metadata as FluentModelMetadata).Metadata.Rules;

                if (isPropertyMetadata)
                {
                    validators.AddRange(rules.Select(rule => new RuleModelValidator(rule, metadata, context)));
                }
                else
                {
                    validators.AddRange(rules.Select(rule => new ClassRuleModelValidator(rule as IClassRule, metadata, context)));
                }
            }

            return validators;
        }
    }
}