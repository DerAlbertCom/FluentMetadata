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
    }
}