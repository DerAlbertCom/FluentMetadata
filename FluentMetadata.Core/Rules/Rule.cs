namespace FluentMetadata.Rules
{
    public abstract class Rule : IRule
    {
        public abstract bool IsValid(object value);

        public string ErrorMessageFormat { get; set; }

        public abstract string FormatErrorMessage(string name);
    }
}