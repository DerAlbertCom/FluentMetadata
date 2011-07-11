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
            return regex.Match(Convert.ToString(value, CultureInfo.CurrentCulture)).Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name);
        }
    }
}