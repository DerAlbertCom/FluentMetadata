using System;
using System.Linq.Expressions;

namespace FluentMetadata
{
    /// <summary>
    /// Entry point for class rules (i.e. rules that must be evaluated in class context)
    /// that concern more than one property.
    /// </summary>
    public interface IPropertiesInClassContextBuilder<T>
    {
        /// <summary>
        /// Adds a rule validating that the property's value is equal to the value of another property.
        /// </summary>
        /// <param name="otherPropertyExpression">The other property expression.</param>
        /// <returns></returns>
        IClassBuilder<T> ShouldEqual(Expression<Func<T, object>> otherPropertyExpression);
    }

    /// <summary>
    /// Entry point for class rules (i.e. rules that must be evaluated in class context)
    /// that concern more than one property and need to compare properties.
    /// </summary>
    public interface IComparablePropertiesInClassContextBuilder<T>
    {
        /// <summary>
        /// Adds a rule validating that the property's value is less than the value of another property.
        /// </summary>
        /// <param name="otherPropertyExpression">The other property expression.</param>
        /// <returns></returns>
        IClassBuilder<T> ShouldBeLessThan(Expression<Func<T, IComparable>> otherPropertyExpression);
    }
}