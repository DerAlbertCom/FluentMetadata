namespace FluentMetadata.Rules
{
    public interface IClassRule : IRule { }

    public interface IClassRule<in T> : IClassRule
    {
        bool IsValid(T instance);
    }
}