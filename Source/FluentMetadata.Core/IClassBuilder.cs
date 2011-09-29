using System;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public interface IClassBuilder<T>
    {
        Metadata Metadata { get; }
        IDisplayClass<T> Display { get; }

        /// <summary>
        /// Entry point for class rules (i.e. rules that must be evaluated in class context)
        /// that concern more than one property.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        IPropertiesInClassContextBuilder<T> Property(Expression<Func<T, object>> propertyExpression);
    }
}