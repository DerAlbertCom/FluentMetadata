using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RequiredRule : Rule
    {
        public RequiredRule()
        {
            ErrorMessageFormat = "a value for {0} is required";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value is string && string.IsNullOrEmpty((string) value))
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name);
        }
    }
}