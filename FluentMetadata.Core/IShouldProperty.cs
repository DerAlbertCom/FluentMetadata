namespace FluentMetadata
{
    public interface IShouldNotProperty<T, TResult>
    {
        IProperty<T,TResult> HiddenInput();
        IProperty<T,TResult> ShowInDisplay();
        IProperty<T,TResult> ShowInEditor();
        IProperty<T,TResult> HideSurroundingHtml();
    }

    public interface IShouldProperty<T, TResult> : IShouldNotProperty<T, TResult>
    {
        IShouldNotProperty<T, TResult> Not { get; }
    }
}