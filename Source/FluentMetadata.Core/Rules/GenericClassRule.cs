using System;

namespace FluentMetadata.Rules
{
    class GenericClassRule<T> : ClassRule<T>
    {
        readonly Func<T, bool> assertFunc;
        readonly Func<string> errorMessageFormatFunc;

        public GenericClassRule(Func<T, bool> assertFunc, Func<string> errorMessageFormatFunc)
            : base(errorMessageFormatFunc())
        {
            this.assertFunc = assertFunc;
            this.errorMessageFormatFunc = errorMessageFormatFunc;
        }

        public override bool IsValid(T instance)
        {
            return assertFunc(instance);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(errorMessageFormatFunc(), name);
        }
    }
}