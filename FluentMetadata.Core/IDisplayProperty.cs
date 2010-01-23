namespace FluentMetadata
{
    public interface IDisplayProperty
    {
        IProperty NullText(string nullDisplayText);
        IProperty Name(string displayName);
        IProperty Format(string displayFormat);
    }
}