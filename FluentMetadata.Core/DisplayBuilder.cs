namespace FluentMetadata
{
    public class DisplayBuilder : IDisplayProperty
    {
        private readonly PropertyMetadataBuilder propertyMetaDataBuilder;

        public DisplayBuilder(PropertyMetadataBuilder propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty NullText(string nullDisplayText)
        {
            propertyMetaDataBuilder.MetaData.NullDisplayText = nullDisplayText;
            return propertyMetaDataBuilder;
        }

        public IProperty Name(string displayName)
        {
            propertyMetaDataBuilder.MetaData.DisplayName = displayName;
            return propertyMetaDataBuilder;
        }

        public IProperty Format(string displayFormat)
        {
            propertyMetaDataBuilder.MetaData.DisplayFormat = displayFormat;
            return propertyMetaDataBuilder;
        }
    }
}