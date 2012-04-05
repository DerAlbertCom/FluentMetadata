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
        protected abstract bool EqualsRule(ClassRule<T> rule);

        public bool IsValid(object value)
        {
            return IsValid((T)value);
        }

        public override bool Equals(object obj)
        {
            return EqualsRule(obj as ClassRule<T>);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}