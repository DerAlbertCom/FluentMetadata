namespace FluentMetadata
{
    public interface IClassBuilder<T>
    {
        Metadata Metadata { get; }
        IDisplayClass<T> Display { get; }
    }
}