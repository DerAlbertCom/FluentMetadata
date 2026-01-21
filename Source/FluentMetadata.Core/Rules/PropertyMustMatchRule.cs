using System;
using System.Globalization;
using System.Linq.Expressions;

namespace FluentMetadata.Rules
{
    public class PropertyMustMatchRule<T> : ClassRule<T>, ICompareProperties
    {
        const string DefaultErrorMessage = "'{0}' and '{1}' do not match.";

        public string PropertyName { get; private set; }
        public string OtherPropertyName { get; private set; }

        public PropertyMustMatchRule(
            Expression<Func<T, object>> expression,
            Expression<Func<T, object>> confirmExpression)
            : base(DefaultErrorMessage)
        {
            PropertyName = ExpressionHelper.GetPropertyName(expression);
            OtherPropertyName = ExpressionHelper.GetPropertyName(confirmExpression);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                ErrorMessageFormat,
                GetPropertyDisplayName(PropertyName),
                GetPropertyDisplayName(OtherPropertyName));
        }

        static string GetPropertyDisplayName(string propertyName)
        {
            var metaData = FluentMetadataBuilder.GetTypeBuilder<T>().MetaDataFor(propertyName);
            if (metaData != null)
            {
                var metaDataDisplayName = metaData.GetDisplayName();
                propertyName = string.IsNullOrEmpty(metaDataDisplayName) ?
                    propertyName :
                    metaDataDisplayName;
            }
            return propertyName;
        }

        public override bool IsValid(T instance)
        {
            if (instance == null)
                return true;

            return Equals(
                GetValueFromProperty(instance, PropertyName),
                GetValueFromProperty(instance, OtherPropertyName));
        }

        static object GetValueFromProperty(object instance, string propertyName)
        {
            return instance.GetType().GetProperty(propertyName).GetValue(instance, null);
        }

        protected override bool EqualsRule(ClassRule<T> rule)
        {
            return rule is PropertyMustMatchRule<T>;
        }
    }
}