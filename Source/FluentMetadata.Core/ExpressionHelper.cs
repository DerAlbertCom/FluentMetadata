using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentMetadata
{
    internal static class ExpressionHelper
    {
        public static string GetPropertyName<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            var memberExpression = GetMemberInfo(expression);
            return memberExpression.Name;
        }

        public static Type GetPropertyType<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            var memberExpression = (PropertyInfo)GetMemberInfo(expression);
            return memberExpression.PropertyType;
        }

        static MemberInfo GetMemberInfo<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetMemberInfoFromExpression(expression.Body);
        }

        static MemberInfo GetMemberInfoFromExpression(Expression expression)
        {
            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                return GetMemberInfoFromExpression(unaryExpression.Operand);
            }

            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                return memberExpression.Member;
            }

            throw new InvalidOperationException(expression.Type.Name + " not handled");
        }

        public static Type GetDeclaringType(Expression<Func<object, object>> expression)
        {
            return GetMemberInfo(expression).DeclaringType;
        }
    }
}