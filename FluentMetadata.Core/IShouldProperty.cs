namespace FluentMetadata
{
    public interface IShouldNotProperty
    {
        IProperty HiddenInput();
        IProperty ShowInDisplay();
        IProperty ShowInEditor();
        IProperty HideSurroundingHtml();
    }

    public interface IShouldProperty : IShouldNotProperty
    {
        IShouldNotProperty Not { get; }
    }
}