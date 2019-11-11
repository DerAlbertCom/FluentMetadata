using System.Linq.Expressions;

namespace FluentMetadata
{
    internal static class ExpressionHelper
    {
        public static string GetPropertyName(LambdaExpression expression)
        {
            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression is UnaryExpression unaryExpression)
            {
                return GetMemberName(unaryExpression.Operand);
            }

            if (expression is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            return string.Empty;
        }
    }
}