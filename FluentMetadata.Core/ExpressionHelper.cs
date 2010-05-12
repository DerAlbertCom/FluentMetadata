using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentMetadata
{
    internal static class ExpressionHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            MemberInfo memberExpression = GetMemberInfo(expression);
            return memberExpression.Name;
        }

        public static Type GetPropertyType<T>(Expression<Func<T, object>> expression)
        {
            var memberExpression = (PropertyInfo) GetMemberInfo(expression);
            return memberExpression.PropertyType;
        }

        private static MemberInfo GetMemberInfo<T>(Expression<Func<T, object>> expression)
        {
            return GetMemberInfoFromExpression(expression.Body);
        }

        private static MemberInfo GetMemberInfoFromExpression(Expression expression)
        {
            if (expression is UnaryExpression)
            {
                var unary = (UnaryExpression) expression;
                return GetMemberInfoFromExpression(unary.Operand);
            }
            if (expression is MemberExpression)
            {
                return ((MemberExpression) expression).Member;
            }
            throw new InvalidOperationException(string.Format("{0} not handled", expression.Type.Name));
        }

        public static Type GetDeclaringType(Expression<Func<object, object>> expression)
        {
            MemberInfo memberExpression = GetMemberInfo(expression);
            return memberExpression.DeclaringType;
        }
    }
}