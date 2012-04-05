using System;
using System.Globalization;
using System.Linq.Expressions;

namespace FluentMetadata.Rules
{
    public class PropertyMustBeLessThanOtherRule<T> : ClassRule<T>
    {
        readonly string propertyName, otherPropertyName;
        readonly Func<T, IComparable> propertyFunc, otherPropertyFunc;

        public PropertyMustBeLessThanOtherRule(
            Expression<Func<T, IComparable>> propertyExpression,
            Expression<Func<T, IComparable>> otherPropertyExpression)
            : base("The value of '{0}.{1}' must be less than the value of '{0}.{2}'.")
        {
            propertyName = ((propertyExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
            propertyFunc = propertyExpression.Compile();
            otherPropertyName = ((otherPropertyExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
            otherPropertyFunc = otherPropertyExpression.Compile();
        }

        public override bool IsValid(T instance)
        {
            return propertyFunc(instance)
                .CompareTo(otherPropertyFunc(instance)) < 0;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                ErrorMessageFormat,
                name,
                propertyName,
                otherPropertyName);
        }

        protected override bool EqualsRule(ClassRule<T> rule)
        {
            throw new NotImplementedException();
        }
    }
}