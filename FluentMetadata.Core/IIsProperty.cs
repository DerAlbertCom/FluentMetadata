namespace FluentMetadata
{
    public interface IIsProperty : IIsNotProperty
    {
        IIsNotProperty Not { get; }
    }

    public interface IIsNotProperty
    {
        IProperty Required();
        IProperty ReadOnly();
    }
}