using System;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public interface IClassBuilder<T>
    {
        Metadata Metadata { get; }
        IDisplayClass<T> Display { get; }
        IClassBuilder<T> PropertiesShouldMatch(
            Expression<Func<T, object>> expression,
            Expression<Func<T, object>> confirmExpression
        );
    }
}