namespace FluentMetadata.Rules
{
    public interface IRule
    {
        bool IsValid(object value);
        string ErrorMessageFormat { get; set; }
        string FormatErrorMessage(string name);
    }
}