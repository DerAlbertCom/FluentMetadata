using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RangeRule : Rule
    {
        private Func<object, object> valueConversion;
        private object valueMaximum;
        private object valueMinimum;

        private RangeRule()
        {
            ErrorMessageFormat = "the value of {0} must be between {0} and {1}";
        }

        public RangeRule(double minimum, double maximum) : this()
        {
            Initialize(minimum, maximum, o => Convert.ToDouble(o));
        }

        public RangeRule(int minimum, int maximum) : this()
        {
            Initialize(minimum, maximum, o => Convert.ToInt32(o));
        }

        public RangeRule(DateTime minimum, DateTime maximum)
            : this()
        {
            Initialize(minimum, maximum, o => Convert.ToDateTime(o));
        }

        private void Initialize(IComparable minimum, IComparable maximum, Func<object, object> conversion)
        {
            if (minimum.CompareTo(maximum) > 0)
            {
                throw new ArgumentOutOfRangeException("maximum", maximum,
                                                      string.Format(CultureInfo.CurrentCulture,
                                                                    "the minimum vallue {1} is higher then the maximum value {0}",
                                                                    minimum, maximum));
            }
            valueMinimum = minimum;
            valueMaximum = maximum;
            valueConversion = conversion;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if ((value is string) && string.IsNullOrEmpty((string) value))
            {
                return true;
            }
            object currentValue = valueConversion(value);
            var min = (IComparable) valueMinimum;
            var max = (IComparable) valueMaximum;
            return ((min.CompareTo(currentValue) <= 0) && (max.CompareTo(currentValue) >= 0));
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name, valueMinimum, valueMaximum);
        }
    }
}