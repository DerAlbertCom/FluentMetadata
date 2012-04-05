namespace FluentMetadata.Rules
{
    public abstract class ClassRule<T> : IClassRule<T>
    {
        protected string ErrorMessageFormat { get; set; }

        protected ClassRule(string errorMessageFormat)
        {
            ErrorMessageFormat = errorMessageFormat;
        }

        public abstract bool IsValid(T instance);
        public abstract string FormatErrorMessage(string name);

        public bool IsValid(object value)
        {
            return IsValid((T)value);
        }
    }
}