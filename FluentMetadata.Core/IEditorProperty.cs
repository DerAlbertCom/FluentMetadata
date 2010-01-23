namespace FluentMetadata
{
    public interface IEditorProperty
    {
        IProperty ErrorMessage(string errorMessage);

        IProperty Format(string editorFormat);
        IProperty Watermark(string watermark);
    }
}