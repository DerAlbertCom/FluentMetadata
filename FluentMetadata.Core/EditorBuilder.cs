namespace FluentMetadata
{
    public class EditorBuilder : IEditorProperty
    {
        private readonly PropertyMetadataBuilder propertyMetaDataBuilder;

        public EditorBuilder(PropertyMetadataBuilder propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty ErrorMessage(string errorMessage)
        {
            propertyMetaDataBuilder.MetaData.ErrorMessage = errorMessage;
            return propertyMetaDataBuilder;
        }


        public IProperty Format(string editorFormat)
        {
            propertyMetaDataBuilder.MetaData.EditorFormat = editorFormat;
            return propertyMetaDataBuilder;
        }

        public IProperty Watermark(string watermark)
        {
            propertyMetaDataBuilder.MetaData.Watermark = watermark;
            return propertyMetaDataBuilder;
        }
    }
}