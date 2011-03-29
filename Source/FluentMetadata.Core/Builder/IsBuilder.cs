namespace FluentMetadata.Builder
{
    internal class IsBuilder<T, TResult> : IIsProperty<T, TResult>
    {
        private readonly PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder;
        private bool notted;

        private Metadata Metadata
        {
            get { return propertyMetaDataBuilder.Metadata; }
        }

        public IsBuilder(PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> Required()
        {
            Metadata.Required = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> ReadOnly()
        {
            Metadata.ReadOnly = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IIsNotProperty<T, TResult> Not
        {
            get
            {
                notted = true;
                return this;
            }
        }

        public IProperty<T, TResult> ConvertEmptyStringToNull()
        {
            Metadata.ConvertEmptyStringToNull = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> RequestValidationEnabled()
        {
            Metadata.RequestValidationEnabled = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }
    }
}