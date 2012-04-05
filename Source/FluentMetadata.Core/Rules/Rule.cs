namespace FluentMetadata.Rules
{
    public abstract class Rule : IRule
    {
        protected string ErrorMessageFormat { get; set; }

        protected Rule(string errorMessageFormat)
        {
            ErrorMessageFormat = errorMessageFormat;
        }

        public abstract bool IsValid(object value);
        public abstract string FormatErrorMessage(string name);
        protected abstract bool EqualsRule(Rule rule);

        public override bool Equals(object obj)
        {
            return EqualsRule(obj as Rule);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}