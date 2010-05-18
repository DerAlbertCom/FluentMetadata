using System;

namespace FluentMetadata.Rules
{
    public abstract class ClassRule<T> : IClassRule<T>
    {
        public ClassRule(string errorMessageFormat)
        {
            ErrorMessageFormat = errorMessageFormat;
        }

        protected string ErrorMessageFormat { get; set; }

        public abstract bool IsValid(T instance);

        public bool IsValid(object value)
        {
            return IsValid((T) value);
        }

        public abstract string FormatErrorMessage(string name);
    }
}