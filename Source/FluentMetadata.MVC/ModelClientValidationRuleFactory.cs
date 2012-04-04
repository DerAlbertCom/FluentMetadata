using System.Collections.Generic;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    static class ModelClientValidationRuleFactory
    {
        internal static IEnumerable<ModelClientValidationRule> Create(IRule rule, string displayName)
        {
            if (rule is StringLengthRule)
            {
                yield return GetStringLengthRule(rule.FormatErrorMessage(displayName), rule as StringLengthRule);
            }
            else
            {
                yield break;
            }
        }

        static ModelClientValidationRule GetStringLengthRule(string errorMessage, StringLengthRule stringLengthRule)
        {
            return new ModelClientValidationStringLengthRule(
                errorMessage,
                stringLengthRule.Minimum ?? 0,
                stringLengthRule.Maximum ?? int.MaxValue);
        }
    }
}