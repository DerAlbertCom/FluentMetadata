namespace FluentMetadata.Rules
{
    public class PropertyMustMatchRegexRule : Rule
    {
        string pattern;

        public PropertyMustMatchRegexRule(string pattern)
            : base("the value for {0} is not in the correct format")
        {
            this.pattern = pattern;
        }

        public override bool IsValid(object value)
        {
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name);
        }
    }
}