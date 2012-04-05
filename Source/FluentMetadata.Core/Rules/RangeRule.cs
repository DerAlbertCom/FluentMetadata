using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RangeRule : Rule
    {
        IComparable valueMaximum;
        IComparable valueMinimum;

        internal object Minimum
        {
            get
            {
                return valueMinimum;
            }
        }

        internal object Maximum
        {
            get
            {
                return valueMaximum;
            }
        }

        RangeRule()
            : base("the value of '{0}' must be between {1} and {2}")
        {
        }

        public RangeRule(IComparable minimum, IComparable maximum)
            : this()
        {
            Initialize(minimum, maximum);
        }

        void Initialize(IComparable minimum, IComparable maximum)
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
                        maximum
                    )
                );
            }
            valueMinimum = minimum;
            valueMaximum = maximum;
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
            var min = (IComparable)valueMinimum;
            var max = (IComparable)valueMaximum;
            return ((min.CompareTo(value) <= 0) && (max.CompareTo(value) >= 0));
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name, valueMinimum, valueMaximum);
        }

        protected override bool EqualsRule(Rule rule)
        {
            return rule is RangeRule;
        }
    }
}