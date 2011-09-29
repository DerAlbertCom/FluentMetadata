using System;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public interface IClassBuilder<T>
    {
        Metadata Metadata { get; }
        IDisplayClass<T> Display { get; }

        /// <summary>
        /// Creates a generic class rule (i.e. a rule that is evaluated in class context)
        /// asserting that the <paramref name="assertFunc"/> returns true.
        /// </summary>
        /// <param name="assertFunc">What to assert.</param>
        /// <param name="errorMessageFormat">The error message format. Can contain a placeholder for {0} the the class display name.</param>
        /// <returns></returns>
        IClassBuilder<T> AssertThat(Func<T, bool> assertFunc, string errorMessageFormat);

        /// <summary>
        /// Entry point for class rules (i.e. rules that must be evaluated in class context)
        /// that concern more than one property.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        IPropertiesInClassContextBuilder<T> Property(Expression<Func<T, object>> propertyExpression);

        /// <summary>
        /// Entry point for class rules (i.e. rules that must be evaluated in class context)
        /// that concern more than one property and need to compare properties.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        IComparablePropertiesInClassContextBuilder<T> ComparableProperty(Expression<Func<T, IComparable>> propertyExpression);
    }
}