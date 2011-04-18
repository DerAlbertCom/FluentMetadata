using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RangeRule : Rule
    {
        private IComparable valueMaximum;
        private IComparable valueMinimum;

        private RangeRule()
            : base("the value of '{0}' must be between {1} and {2}")
        {
        }

        public RangeRule(IComparable minimum, IComparable maximum)
            : this()
        {
            Initialize(minimum, maximum, o => Convert.ToDateTime(o));
        }

        private void Initialize(IComparable minimum, IComparable maximum, Func<object, object> conversion)
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
            if ((value is string) && string.IsNullOrEmpty(value as string))
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
    }
}