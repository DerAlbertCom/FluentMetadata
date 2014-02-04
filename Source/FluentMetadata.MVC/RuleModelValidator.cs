using System.Collections.Generic;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    internal class RuleModelValidator : ModelValidator
    {
        private readonly IRule rule;

        public RuleModelValidator(IRule rule, ModelMetadata metadata, ControllerContext controllerContext)
            : base(metadata, controllerContext)
        {
            this.rule = rule;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (rule.IsValid(GetPropertyValue(container, Metadata.PropertyName)))
            {
                yield break;
            }
            yield return
                new ModelValidationResult
                    {
                        Message = rule.FormatErrorMessage(Metadata.GetDisplayName())
                    };
        }

        private static object GetPropertyValue(object container, string propertyName)
        {
            var info = container.GetType().GetProperty(propertyName);
            return info.GetValue(container, null);
        }
    }

    internal class ClassRuleModelValidator : ModelValidator
    {
        private readonly IClassRule rule;

        public ClassRuleModelValidator(IClassRule rule, ModelMetadata metadata, ControllerContext controllerContext)
            : base(metadata, controllerContext)
        {
            this.rule = rule;
        }

        // TODO: write a test for this method using System.Web.Mvc.DefaultModelBinder.BindModel
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            // container is useless because System.Web.Mvc.DefaultModelBinder passes null for it
            if (rule.IsValid(container ?? Metadata.Model))
            {
                yield break;
            }
            yield return
                new ModelValidationResult
                {
                    Message = rule.FormatErrorMessage(Metadata.GetDisplayName())
                };
        }
    }

}