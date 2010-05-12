namespace FluentMetadata
{
    public class ShouldBuilder : IShouldProperty
    {
        private readonly PropertyMetadataBuilder propertyMetaDataBuilder;
        private bool notted;

        public ShouldBuilder(PropertyMetadataBuilder propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        private MetaData MetaData { get { return propertyMetaDataBuilder.MetaData; } }
 
        public IProperty HiddenInput()
        {
            MetaData.Hidden = !notted;
            MetaData.HideSurroundingHtml = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty ShowInDisplay()
        {
            MetaData.ShowDisplay = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty ShowInEditor()
        {
            MetaData.ShowEditor = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IProperty HideSurroundingHtml()
        {
            MetaData.HideSurroundingHtml = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IShouldNotProperty Not
        {
            get
            {
                notted = true;
                return this;
            }
        }
    }
}