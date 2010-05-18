namespace FluentMetadata.Rules
{
    public interface IClassRule : IRule
    {
        
    }
    public interface IClassRule<T> : IClassRule
    {
        bool IsValid(T instance);
    }
}