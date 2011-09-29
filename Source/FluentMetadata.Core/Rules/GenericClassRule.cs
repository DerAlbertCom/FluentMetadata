using System;

namespace FluentMetadata.Rules
{
    class GenericClassRule<T> : ClassRule<T>
    {
        readonly Func<T, bool> assertFunc;

        public GenericClassRule(string errorMessageFormat, Func<T, bool> assertFunc)
            : base(errorMessageFormat)
        {
            this.assertFunc = assertFunc;
        }

        public override bool IsValid(T instance)
        {
            return assertFunc(instance);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageFormat, name);
        }
    }
}