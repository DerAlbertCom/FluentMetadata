namespace FluentMetadata
{
    public interface IProperty
    {
        IProperty Length(int length);
        IProperty TemplateHint(string templateHint);

        IProperty Description(string description);

        IEditorProperty Editor { get; }
        IDisplayProperty Display { get; }
        IAsProperty As { get; }
        IIsProperty Is { get; }
        IShouldProperty Should { get; }
    }
}