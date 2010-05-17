namespace FluentMetadata
{
    public interface IDisplayProperty<T,TResult>
    {
        IProperty<T,TResult> NullText(string nullDisplayText);
        IProperty<T,TResult> Name(string displayName);
        IProperty<T,TResult> Format(string displayFormat);
    }
}