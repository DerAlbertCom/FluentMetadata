using System.Collections.Generic;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    class RuleModelValidator : ModelValidator
    {
        readonly IRule rule;

        public override bool IsRequired
        {
            get
            {
                return rule is RequiredRule;
            }
        }

        internal RuleModelValidator(IRule rule, ModelMetadata metadata, ControllerContext controllerContext)
            : base(metadata, controllerContext)
        {
            this.rule = rule;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (rule.IsValid(container.GetType().GetProperty(Metadata.PropertyName).GetValue(container, null)))
            {
                yield break;
            }
            yield return new ModelValidationResult
            {
                Message = rule.FormatErrorMessage(Metadata.GetDisplayName())
            };
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return ModelClientValidationRuleFactory.Create(rule, Metadata.GetDisplayName());
        }
    }

    class ClassRuleModelValidator : ModelValidator
    {
        readonly IClassRule rule;

        internal ClassRuleModelValidator(IClassRule rule, ModelMetadata metadata, ControllerContext controllerContext)
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
            yield return new ModelValidationResult
            {
                Message = rule.FormatErrorMessage(Metadata.GetDisplayName())
            };
        }
    }
}