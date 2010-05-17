namespace FluentMetadata
{
    public class ShouldBuilder<T, TResult> : IShouldProperty<T, TResult>
    {
        private readonly PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder;
        private bool notted;

        public ShouldBuilder(PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        private MetaData MetaData { get { return propertyMetaDataBuilder.MetaData; } }

        public IProperty<T,TResult> HiddenInput()
        {
            MetaData.Hidden = !notted;
            MetaData.HideSurroundingHtml = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> ShowInDisplay()
        {
            MetaData.ShowDisplay = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> ShowInEditor()
        {
            MetaData.ShowEditor = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> HideSurroundingHtml()
        {
            MetaData.HideSurroundingHtml = !notted;
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