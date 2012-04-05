using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    // ~ System.ComponentModel.DataAnnotations.StringLengthAttribute.MaximumLength
    // ~ System.ComponentModel.DataAnnotations.StringLengthAttribute.MinimumLength
    public class StringLengthRule : Rule
    {
        readonly int? minLength, maxLength;

        internal int? Minimum
        {
            get
            {
                return minLength;
            }
        }

        internal int? Maximum
        {
            get
            {
                return maxLength;
            }
        }

        public StringLengthRule(int maxLength)
            : base("the string for '{0}' should not be longer than {1} characters")
        {
            this.maxLength = maxLength;
        }

        public StringLengthRule(int minLength, int? maxLength)
            : base("'{0}' must be " +
                (maxLength.HasValue ? " between {2} and {1}" : " at least {2}") +
                " characters long")
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString == null)
            {
                return minLength.HasValue ? false : true;
            }

            var length = valueAsString.Length;
            if (maxLength.HasValue && length > maxLength ||
                minLength.HasValue && length < minLength)
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                ErrorMessageFormat,
                name,
                maxLength,
                minLength);
        }

        protected override bool EqualsRule(Rule rule)
        {
            return rule is StringLengthRule;
        }
    }

    //TODO [DerAlbertCom] implement or delete: What does this rule validate?
    public class EqualToRule : Rule
    {
        public EqualToRule(string errorMessageFormat)
            : base(errorMessageFormat)
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

        protected override bool EqualsRule(Rule rule)
        {
            throw new NotImplementedException();
        }
    }
}