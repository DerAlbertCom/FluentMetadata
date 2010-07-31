namespace FluentMetadata.Builder
{
    internal class DisplayBuilder<T> : IDisplayClass<T>
    {
        private readonly IClassBuilder<T> classBuilder;

        public DisplayBuilder(IClassBuilder<T> classBuilder)
        {
            this.classBuilder = classBuilder;
        }

        public IClassBuilder<T> Name(string displayName)
        {
            classBuilder.Metadata.DisplayName = displayName;
            return classBuilder;
        }

        public IClassBuilder<T> Format(string displayFormat)
        {
            classBuilder.Metadata.DisplayFormat = displayFormat;
            return classBuilder;
        }
    }

    internal class DisplayBuilder<T,TResult> : IDisplayProperty<T,TResult>
    {
        private readonly PropertyMetadataBuilder<T,TResult> propertyMetaDataBuilder;

        public DisplayBuilder(PropertyMetadataBuilder<T,TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> NullText(string nullDisplayText)
        {
            propertyMetaDataBuilder.Metadata.NullDisplayText = nullDisplayText;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Name(string displayName)
        {
            propertyMetaDataBuilder.Metadata.DisplayName = displayName;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Format(string displayFormat)
        {
            propertyMetaDataBuilder.Metadata.DisplayFormat = displayFormat;
            return propertyMetaDataBuilder;
        }
    }
}