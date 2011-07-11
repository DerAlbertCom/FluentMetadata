using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FluentMetadata.Rules
{
    public class PropertyMustMatchRegexRule : Rule
    {
        string pattern;
        Regex regex;

        public PropertyMustMatchRegexRule(string pattern)
            : base("the value for {0} is not in the correct format")
        {
            this.pattern = pattern;
            regex = new Regex(pattern);
        }

        public override bool IsValid(object value)
        {
            // because validating this is not the responsibility of the PropertyMustMatchRegexRule
            if (value == null || value.ToString() == string.Empty)
            {
                return true;
            }
            return Matches(value);
        }

        protected bool Matches(object value)
        {
            return regex.Match(Convert.ToString(value, CultureInfo.CurrentCulture)).Success;
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
            // because validating this is not the responsibility of the PropertyMustNotMatchRegexRule
            if (value == null || value.ToString() == string.Empty)
            {
                return true;
            }
            return !Matches(value);
        }
    }
}