using System;

namespace FluentMetadata.Rules
{
    public interface IRule
    {
        Type PropertyType { get; }
        bool IsValid(object value);
        string FormatErrorMessage(string name);
    }
}