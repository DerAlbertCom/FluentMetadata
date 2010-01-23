using System;
using System.Globalization;

namespace FluentMetadata.Rules
{
    public class RangeRule : Rule
    {
        private object Minimum;
        private object Maximum;
        private Func<object, object> Conversion;

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
            Minimum = minimum;
            Maximum = maximum;
            Conversion = conversion;
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
            object currentValue = Conversion(value);
            var minimum = (IComparable) Minimum;
            var maximum = (IComparable) Maximum;
            return ((minimum.CompareTo(currentValue) <= 0) && (maximum.CompareTo(currentValue) >= 0));
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageFormat, name, Minimum, Maximum);
        }
    }
}