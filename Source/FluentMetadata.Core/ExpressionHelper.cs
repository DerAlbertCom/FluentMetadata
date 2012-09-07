using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentMetadata
{
    internal static class ExpressionHelper
    {
        public static string GetPropertyName(LambdaExpression expression)
        {
            return GetMemberName(expression.Body);
        }

        static string GetMemberName(Expression expression)
        {
            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                return GetMemberName(unaryExpression.Operand);
            }

            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            return string.Empty;
        }
    }
}