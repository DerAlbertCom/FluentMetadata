using System;
using System.Globalization;
using System.Linq.Expressions;

namespace FluentMetadata.Rules
{
    public class PropertyMustMatchRule<T, TResult> : ClassRule<T>
    {
        private const string DefaultErrorMessage = "'{0}' and '{1}' do not match.";


        private readonly string originalPropertyName;
        private readonly string confirmPropertyName;
        private object currentInstance;

        public PropertyMustMatchRule(Expression<Func<T, TResult>> expression,
                                     Expression<Func<T, TResult>> confirmExpression) : base(DefaultErrorMessage)
        {
            originalPropertyName = ExpressionHelper.GetPropertyName(expression);
            confirmPropertyName = ExpressionHelper.GetPropertyName(confirmExpression);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
                                 ErrorMessageFormat,
                                 GetPropertyDisplayName(originalPropertyName),
                                 GetPropertyDisplayName(confirmPropertyName)
                );
        }

        private string GetPropertyDisplayName(string propertyName)
        {
            return propertyName;
            //var displayName = GetMetaDataFor(currentInstance).Properties
            //    .Where(s => s.PropertyName == propertyName)
            //    .Single().DisplayName;
            //return string.IsNullOrEmpty(displayName) ? propertyName : displayName;
        }

        //private static MetaData GetMetaDataFor(T instance)
        //{
        //    return ModelMetadataProviders.Current.GetMetadataForType(() => instance, instance.GetType());
        //}

        public override bool IsValid(T instance)
        {
            currentInstance = instance;

            return Equals(GetValueFromProperty(currentInstance, originalPropertyName),
                          GetValueFromProperty(currentInstance, confirmPropertyName));
        }

        private static object GetValueFromProperty(object instance, string propertyName)
        {
            return instance.GetType().GetProperty(propertyName).GetValue(instance, null);
        }
    }
}