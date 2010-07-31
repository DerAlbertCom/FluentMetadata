namespace FluentMetadata
{
    public interface IDisplayClass<T>
    {
        IClassBuilder<T> Name(string displayName);
        IClassBuilder<T> Format(string displayFormat);
    }
}