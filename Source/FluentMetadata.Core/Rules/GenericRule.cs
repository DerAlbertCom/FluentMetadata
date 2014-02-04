using System;

namespace FluentMetadata.Rules
{
    class GenericRule<TProperty> : Rule
    {
        readonly Func<TProperty, bool> assertFunc;

        public GenericRule(string errorMessageFormat, Func<TProperty, bool> assertFunc)
            : base(errorMessageFormat)
        {
            this.assertFunc = assertFunc;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageFormat, name);
        }

        public override bool IsValid(object value)
        {
            return IsValid((TProperty)value);
        }

        public bool IsValid(TProperty value)
        {
            return assertFunc(value);
        }
    }
}