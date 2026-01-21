using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    // ~ System.ComponentModel.DataAnnotations.StringLengthAttribute.MaximumLength
    // ~ System.ComponentModel.DataAnnotations.StringLengthAttribute.MinimumLength
    public class StringLengthRule : Rule
    {
        /// <summary>A customizable function returning the error message format for the rule.
        /// Contains placeholders {0} for the display name of the property and {2} for the maximum length.</summary>
        public static Func<string> GetMaxLengthErrorMessageFormat = () => "'{0}' must not be longer than {2} characters";

        /// <summary>A customizable function returning the error message format for the rule.
        /// Contains placeholders {0} for the display name of the property and {1} for the minimum length.</summary>
        public static Func<string> GetMinLengthErrorMessageFormat = () => "'{0}' must be at least {1} characters long";

        /// <summary>A customizable function returning the error message format for the rule.
        /// Contains placeholders {0} for the display name of the property, {1} for the minimum and {2} for the maximum length.</summary>
        public static Func<string> GetLengthRangeErrorMessageFormat = () => "'{0}' must be between {1} and {2} characters long";

        internal int? Minimum { get; }
        internal int? Maximum { get; }
        public override Type PropertyType => typeof(string);

        public StringLengthRule(int maxLength)
            : base(GetMaxLengthErrorMessageFormat())
        {
            Maximum = maxLength;
        }

        public StringLengthRule(int minLength, int? maxLength)
            : base(maxLength.HasValue ? GetLengthRangeErrorMessageFormat() : GetMinLengthErrorMessageFormat())
        {
            Minimum = minLength;
            Maximum = maxLength;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString == null)
            {
                return Minimum.HasValue ? false : true;
            }

            var length = valueAsString.Length;
            if (Maximum.HasValue && length > Maximum ||
                Minimum.HasValue && length < Minimum)
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name, Minimum, Maximum);
        protected override bool EqualsRule(Rule rule) => rule is StringLengthRule;
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

        public override Type PropertyType
        {
            get { throw new NotImplementedException(); }
        }
    }
}