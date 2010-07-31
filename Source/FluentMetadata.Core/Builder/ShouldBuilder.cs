namespace FluentMetadata.Builder
{
    internal class ShouldBuilder<T, TResult> : IShouldProperty<T, TResult>
    {
        private readonly PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder;
        private bool notted;

        public ShouldBuilder(PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        private Metadata Metadata { get { return propertyMetaDataBuilder.Metadata; } }

        public IProperty<T,TResult> HiddenInput()
        {
            Metadata.Hidden = !notted;
            Metadata.HideSurroundingHtml = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> ShowInDisplay()
        {
            Metadata.ShowDisplay = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> ShowInEditor()
        {
            Metadata.ShowEditor = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> HideSurroundingHtml()
        {
            Metadata.HideSurroundingHtml = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IShouldNotProperty<T, TResult> Not
        {
            get
            {
                notted = true;
                return this;
            }
        }
    }
}