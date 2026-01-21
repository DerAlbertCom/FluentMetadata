using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RangeRule : Rule
    {
        /// <summary>A customizable function returning the error message format for the rule.
        /// Contains placeholders {0} for the display name of the property, {1} for the minimum and {2} for the maximum.</summary>
        public static Func<string> GetErrorMessageFormat = () => "the value of '{0}' must be between {1} and {2}";

        readonly Type propertyType;
        readonly IComparable maximum, minimum;

        public override Type PropertyType => propertyType;
        internal object Minimum => minimum;
        internal object Maximum => maximum;

        RangeRule() : base(GetErrorMessageFormat()) { }

        public RangeRule(IComparable minimum, IComparable maximum, Type propertyType)
            : this()
        {
            if (minimum.CompareTo(maximum) > 0)
            {
                throw new ArgumentOutOfRangeException(
                    "maximum",
                    maximum,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "the minimum value {0} is higher then the maximum value {1}",
                        minimum,
                        maximum));
            }
            this.minimum = minimum;
            this.maximum = maximum;
            this.propertyType = propertyType;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var valueAsString = value as string;
            if (valueAsString != null && string.IsNullOrEmpty(valueAsString))
            {
                return true;
            }
            return minimum.CompareTo(value) <= 0 &&
                maximum.CompareTo(value) >= 0;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name, minimum, maximum);
        }

        protected override bool EqualsRule(Rule rule)
        {
            return rule is RangeRule;
        }
    }
}