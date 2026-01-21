using System.Collections.Generic;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    internal static class ModelClientValidationRuleFactory
    {
        internal static IEnumerable<ModelClientValidationRule> Create(IRule rule, string displayName)
        {
            if (rule is StringLengthRule)
            {
                yield return GetStringLengthRule(rule.FormatErrorMessage(displayName), rule as StringLengthRule);
            }
            else if (rule is RangeRule)
            {
                yield return GetRangeRule(rule.FormatErrorMessage(displayName), rule as RangeRule);
            }
            else if (rule is PropertyMustMatchRegexRule)
            {
                yield return new ModelClientValidationRegexRule(rule.FormatErrorMessage(displayName), (rule as PropertyMustMatchRegexRule).Pattern);
            }
            else if (rule is ClassRuleValidatingAPropertyWrapper)
            {
                var propertyComparingRule = (rule as ClassRuleValidatingAPropertyWrapper).PropertyValidatingRule as ICompareProperties;

                if (propertyComparingRule != null)
                {
                    yield return new ModelClientValidationEqualToRule(propertyComparingRule.FormatErrorMessage(displayName), propertyComparingRule.OtherPropertyName);
                }
            }
            else
            {
                yield break;
            }
        }

        private static ModelClientValidationRule GetStringLengthRule(string errorMessage, StringLengthRule stringLengthRule)
        {
            return new ModelClientValidationStringLengthRule(errorMessage, stringLengthRule.Minimum ?? 0, stringLengthRule.Maximum ?? int.MaxValue);
        }

        private static ModelClientValidationRule GetRangeRule(string errorMessage, RangeRule rangeRule)
        {
            return new ModelClientValidationRangeRule(errorMessage, rangeRule.Minimum, rangeRule.Maximum);
        }
    }
}