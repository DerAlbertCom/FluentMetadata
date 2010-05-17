namespace FluentMetadata
{
    public interface IEditorProperty<T,TResult>
    {
        IProperty<T,TResult> ErrorMessage(string errorMessage);

        IProperty<T,TResult> Format(string editorFormat);
        IProperty<T,TResult> Watermark(string watermark);
    }
}