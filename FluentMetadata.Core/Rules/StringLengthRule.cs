namespace FluentMetadata.Rules
{
    public class StringLengthRule : Rule
    {
        private readonly int maxLength;

        public StringLengthRule()
        {
            ErrorMessageFormat = "the string for {0} should be longer than {1} characters";
        }

        public StringLengthRule(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var strValue = (string) value;
            return strValue.Length <= maxLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageFormat, name, maxLength);
        }
    }
}