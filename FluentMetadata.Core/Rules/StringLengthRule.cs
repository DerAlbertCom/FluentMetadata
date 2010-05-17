using System;

namespace FluentMetadata.Rules
{
    public class StringLengthRule : Rule
    {
        private readonly int maxLength;

        public StringLengthRule(int maxLength)
            : base("the string for {0} should be longer than {1} characters")
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

    public class EqualToRule : Rule
    {
        public EqualToRule(string errorMessageFormat) : base(errorMessageFormat)
        {
        }

        public override bool IsValid(object value)
        {
            throw new NotImplementedException();
        }

        public override string FormatErrorMessage(string name)
        {
            throw new NotImplementedException();
        }
    }
}