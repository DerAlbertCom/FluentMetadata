namespace FluentMetadata
{
    public class DisplayBuilder<T,TResult> : IDisplayProperty<T,TResult>
    {
        private readonly PropertyMetadataBuilder<T,TResult> propertyMetaDataBuilder;

        public DisplayBuilder(PropertyMetadataBuilder<T,TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> NullText(string nullDisplayText)
        {
            propertyMetaDataBuilder.MetaData.NullDisplayText = nullDisplayText;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Name(string displayName)
        {
            propertyMetaDataBuilder.MetaData.DisplayName = displayName;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Format(string displayFormat)
        {
            propertyMetaDataBuilder.MetaData.DisplayFormat = displayFormat;
            return propertyMetaDataBuilder;
        }
    }
}