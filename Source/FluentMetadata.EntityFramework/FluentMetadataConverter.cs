using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentMetadata.EntityFramework
{
    public class FluentMetadataConverter
    {
        public void MapProperties(Type instanceType, StructuralTypeConfiguration configuration)
        {
            MapProperties(instanceType, configuration.GetType(), configuration);
        }

        private void MapProperties(Type instanceType, Type configurationType, StructuralTypeConfiguration configuration)
        {
            var metaDatas = new Metadata[0];// =  // FluentMetadataBuilder.GetTypeBuilder(instanceType);

            var metadataCopier = new MetadataCopier();

            foreach (var data in metaDatas)
            {
                if (data.ModelName == null)
                {
                    continue;
                }

                var methodInfo = GetPropertyMappingMethod(configurationType, instanceType, data.ModelType);
                if (methodInfo == null)
                {
                    continue;
                }

                var lambda = CreateExpressionForProperty(instanceType, data.ModelName);
                if (lambda != null)
                {
                    metadataCopier.CopyMetadata(
                        (PropertyConfiguration) methodInfo.Invoke(configuration, new[] {lambda}), data);
                }
            }
        }

        private MethodInfo GetPropertyMappingMethod(Type configurationType, Type instanceType, Type propertyType)
        {
            var expressionFuncType = CreateExpressionFuncTypeOf(instanceType, propertyType);

            return (from methodInfo in configurationType.GetMethods()
                    let parameters = methodInfo.GetParameters()
                    where methodInfo.Name == "Property" &&
                          parameters[0].ParameterType == expressionFuncType
                    select methodInfo).FirstOrDefault();
        }

        private Type CreateExpressionFuncTypeOf(Type instanceType, Type propertyType)
        {
            var funcType = typeof (Func<,>).MakeGenericType(instanceType, propertyType);
            return typeof (Expression<>).MakeGenericType(funcType);
        }

        private Expression CreateExpressionForProperty(Type type, string propertyName)
        {
            var propertyAccessor = type.GetProperty(propertyName,
                                                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance|BindingFlags.FlattenHierarchy);

            if (propertyAccessor == null || !propertyAccessor.CanWrite)
            {
                return null;
            }

            var parameterExpression = Expression.Parameter(type, "p");
            var propertyExpression = Expression.Property(parameterExpression, propertyAccessor);
            return Expression.Lambda(propertyExpression, parameterExpression);
        }
    }
}