using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentMetadata.EntityFramework.Internal
{
    internal class ExpressionGenerator
    {
        public Expression CreateExpressionForProperty(Type type, string propertyName)
        {
            Expression lambda = null;
            Type entityType = type;
            do
            {
                var propertyAccessor = type.GetProperty(propertyName,
                                                        BindingFlags.NonPublic | BindingFlags.Public |
                                                        BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                if (propertyAccessor == null || !propertyAccessor.CanWrite)
                {
                    type = type.BaseType;
                    continue;
                }

                lambda = CreateLambda(entityType, propertyAccessor);
            } while (lambda == null && type != typeof (object));

            return lambda;
        }

        private Expression CreateLambda(Type type, PropertyInfo propertyAccessor)
        {
            Expression lambda;
            var parameterExpression = Expression.Parameter(type, "p");
            var propertyExpression = Expression.Property(parameterExpression, propertyAccessor);
            lambda = Expression.Lambda(propertyExpression, parameterExpression);
            return lambda;
        }
    }
}