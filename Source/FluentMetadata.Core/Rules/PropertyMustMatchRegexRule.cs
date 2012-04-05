using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FluentMetadata.Rules
{
    public class PropertyMustMatchRegexRule : Rule
    {
        internal readonly string Pattern;

        public PropertyMustMatchRegexRule(string pattern)
            : base("the value for {0} is not in the correct format")
        {
            Pattern = pattern;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = Convert.ToString(value, CultureInfo.CurrentCulture);
            // because validating this is not the responsibility of the PropertyMustMatchRegexRule
            if (string.IsNullOrEmpty(valueAsString))
            {
                return true;
            }
            return Matches(valueAsString);
        }

        protected bool Matches(string value)
        {
            return new Regex(Pattern).Match(value).Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name);
        }

        protected override bool EqualsRule(Rule rule)
        {
            var propertyMustMatchRegexRule = rule as PropertyMustMatchRegexRule;
            return propertyMustMatchRegexRule == null ?
                false :
                propertyMustMatchRegexRule.Pattern.Equals(Pattern);
        }
    }

    public class PropertyMustNotMatchRegexRule : PropertyMustMatchRegexRule
    {
        public PropertyMustNotMatchRegexRule(string pattern)
            : base(pattern)
        {
        }

        public override bool IsValid(object value)
        {
            var valueAsString = Convert.ToString(value, CultureInfo.CurrentCulture);
            // because validating this is not the responsibility of the PropertyMustNotMatchRegexRule
            if (string.IsNullOrEmpty(valueAsString))
            {
                return true;
            }
            return !Matches(valueAsString);
        }
    }
}