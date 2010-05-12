namespace FluentMetadata
{
    public interface IAsProperty
    {
        IProperty EmailAddress();
        IProperty Url();
        IProperty Html();
        IProperty Text();
        IProperty MultilineText();
        IProperty Password();
    }
}