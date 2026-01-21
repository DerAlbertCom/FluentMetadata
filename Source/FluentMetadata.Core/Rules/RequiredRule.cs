using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RequiredRule : Rule
    {
        /// <summary>A customizable function returning the error message format for the rule.
        /// Contains placeholder {0} for the display name of the property.</summary>
        public static Func<string> GetErrorMessageFormat = () => "a value for {0} is required";

        public override Type PropertyType => typeof(object);

        public RequiredRule() : base(GetErrorMessageFormat()) { }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            var valueAsString = value as string;
            if (valueAsString != null && string.IsNullOrEmpty(valueAsString))
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name);
        }

        protected override bool EqualsRule(Rule rule)
        {
            return rule is RequiredRule;
        }
    }
}