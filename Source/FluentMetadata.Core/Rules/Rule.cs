namespace FluentMetadata.Rules
{
    public abstract class Rule : IRule
    {
        public Rule(string errorMessageFormat)
        {
            ErrorMessageFormat = errorMessageFormat;
        }

        public abstract bool IsValid(object value);

        protected string ErrorMessageFormat { get; set; }

        public abstract string FormatErrorMessage(string name);
    }
}