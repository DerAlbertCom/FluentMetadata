namespace FluentMetadata.Rules
{
    public interface IRule
    {
        bool IsValid(object value);
        string FormatErrorMessage(string name);
    }
}