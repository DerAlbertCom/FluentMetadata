using System;

namespace FluentMetadata.Rules
{
    public interface IClassRule : IRule
    {
        Type ClassType { get; }
    }

    public interface IClassRule<in T> : IClassRule
    {
        bool IsValid(T instance);
    }
}