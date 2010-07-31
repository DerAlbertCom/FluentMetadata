using System.ComponentModel.DataAnnotations;

namespace FluentMetadata.Builder
{
    internal class AsBuilder<T, TResult> : IAsProperty<T, TResult>
    {
        private readonly PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder;

        public AsBuilder(PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> EmailAddress()
        {
            SetDataTypeName(DataType.EmailAddress);
            return propertyMetaDataBuilder;
        }

        private void SetDataTypeName(DataType dataType)
        {
            propertyMetaDataBuilder.Metadata.DataTypeName = dataType.ToString();
        }

        public IProperty<T,TResult> Url()
        {
            SetDataTypeName(DataType.Url);
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Html()
        {
            SetDataTypeName(DataType.Html);
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Text()
        {
            SetDataTypeName(DataType.Text);
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> MultilineText()
        {
            SetDataTypeName(DataType.MultilineText);
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Password()
        {
            SetDataTypeName(DataType.Password);
            return propertyMetaDataBuilder;
        }
    }
}