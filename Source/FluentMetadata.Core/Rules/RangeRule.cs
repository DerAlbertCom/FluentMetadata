using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RangeRule : Rule
    {
        readonly IComparable maximum, minimum;

        internal object Minimum
        {
            get { return minimum; }
        }

        internal object Maximum
        {
            get { return maximum; }
        }

        RangeRule()
            : base("the value of '{0}' must be between {1} and {2}")
        {
        }

        public RangeRule(IComparable minimum, IComparable maximum)
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