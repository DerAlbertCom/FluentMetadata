namespace FluentMetadata
{
    public interface IProperty<T,TResult>
    {
        IProperty<T,TResult> Length(int length);
        IProperty<T,TResult> UIHint(string templateHint);

        IProperty<T,TResult> Description(string description);

        IEditorProperty<T,TResult> Editor { get; }
        IDisplayProperty<T,TResult> Display { get; }
        IAsProperty<T, TResult> As { get; }
        IIsProperty<T, TResult> Is { get; }
        IShouldProperty<T, TResult> Should { get; }
    }
}