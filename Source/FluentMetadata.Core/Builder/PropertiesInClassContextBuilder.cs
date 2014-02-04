using System;
using System.Linq.Expressions;
using FluentMetadata.Rules;

namespace FluentMetadata.Builder
{
    internal class PropertiesInClassContextBuilder<T> : IPropertiesInClassContextBuilder<T>
    {
        readonly IClassBuilder<T> classBuilder;
        readonly Expression<Func<T, object>> propertyExpression;

        public PropertiesInClassContextBuilder(IClassBuilder<T> classBuilder, Expression<Func<T, object>> propertyExpression)
        {
            this.classBuilder = classBuilder;
            this.propertyExpression = propertyExpression;
        }

        public IClassBuilder<T> ShouldEqual(Expression<Func<T, object>> otherPropertyExpression)
        {
            classBuilder.Metadata.AddRule(new PropertyMustMatchRule<T>(propertyExpression, otherPropertyExpression));
            return classBuilder;
        }
    }

    internal class ComparablePropertiesInClassContextBuilder<T> : IComparablePropertiesInClassContextBuilder<T>
    {
        readonly IClassBuilder<T> classBuilder;
        readonly Expression<Func<T, IComparable>> propertyExpression;

        public ComparablePropertiesInClassContextBuilder(IClassBuilder<T> classBuilder, Expression<Func<T, IComparable>> propertyExpression)
        {
            this.classBuilder = classBuilder;
            this.propertyExpression = propertyExpression;
        }

        public IClassBuilder<T> ShouldBeLessThan(Expression<Func<T, IComparable>> otherPropertyExpression)
        {
            classBuilder.Metadata.AddRule(new PropertyMustBeLessThanOtherRule<T>(propertyExpression, otherPropertyExpression));
            return classBuilder;
        }
    }
}