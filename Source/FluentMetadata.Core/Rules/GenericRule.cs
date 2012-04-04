using System;

namespace FluentMetadata.Rules
{
    class GenericRule<TProperty> : Rule
    {
        readonly Func<TProperty, bool> assertFunc;
        readonly Func<string> errorMessageFormatFunc;

        public GenericRule(Func<TProperty, bool> assertFunc, Func<string> errorMessageFormatFunc)
            : base(errorMessageFormatFunc())
        {
            this.assertFunc = assertFunc;
            this.errorMessageFormatFunc = errorMessageFormatFunc;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(errorMessageFormatFunc(), name);
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