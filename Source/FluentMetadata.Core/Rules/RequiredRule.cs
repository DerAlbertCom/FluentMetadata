using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RequiredRule : Rule
    {
        public override Type PropertyType
        {
            get { return typeof(object); }
        }

        public RequiredRule()
            : base("a value for {0} is required")
        {
        }

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