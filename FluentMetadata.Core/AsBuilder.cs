using System.ComponentModel.DataAnnotations;

namespace FluentMetadata
{
    public class AsBuilder : IAsProperty
    {
        private readonly PropertyMetadataBuilder propertyMetaDataBuilder;

        public AsBuilder(PropertyMetadataBuilder propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty EmailAddress()
        {
            SetDataTypeName(DataType.EmailAddress);
            return propertyMetaDataBuilder;
        }

        private void SetDataTypeName(DataType dataType)
        {
            propertyMetaDataBuilder.MetaData.DataTypeName = dataType.ToString();
        }

        public IProperty Url()
        {
            SetDataTypeName(DataType.Url);
            return propertyMetaDataBuilder;
        }

        public IProperty Html()
        {
            SetDataTypeName(DataType.Html);
            return propertyMetaDataBuilder;
        }

        public IProperty Text()
        {
            SetDataTypeName(DataType.Text);
            return propertyMetaDataBuilder;
        }

        public IProperty MultilineText()
        {
            SetDataTypeName(DataType.MultilineText);
            return propertyMetaDataBuilder;
        }

        public IProperty Password()
        {
            SetDataTypeName(DataType.Password);
            return propertyMetaDataBuilder;
        }
    }
}