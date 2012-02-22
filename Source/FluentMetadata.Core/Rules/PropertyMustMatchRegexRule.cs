using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FluentMetadata.Rules
{
    public class PropertyMustMatchRegexRule : Rule
    {
        Regex regex;

        public PropertyMustMatchRegexRule(string pattern)
            : base("the value for {0} is not in the correct format")
        {
            regex = new Regex(pattern);
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
            return regex.Match(value).Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name);
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