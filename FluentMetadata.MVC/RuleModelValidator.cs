using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    public class RuleModelValidator : ModelValidator
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
                new ModelValidationResult()
                    {
                        MemberName = this.Metadata.PropertyName,
                        Message = rule.FormatErrorMessage(Metadata.GetDisplayName())
                    };
        }

        private static object GetPropertyValue(object container, string propertyName)
        {
            var info = container.GetType().GetProperty(propertyName);
            return info.GetValue(container, new object[] {});
        }
    }
}