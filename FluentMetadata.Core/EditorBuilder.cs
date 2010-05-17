namespace FluentMetadata
{
    public class EditorBuilder<T,TResult> : IEditorProperty<T,TResult>
    {
        private readonly PropertyMetadataBuilder<T,TResult> propertyMetaDataBuilder;

        public EditorBuilder(PropertyMetadataBuilder<T,TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> ErrorMessage(string errorMessage)
        {
            propertyMetaDataBuilder.MetaData.ErrorMessage = errorMessage;
            return propertyMetaDataBuilder;
        }


        public IProperty<T,TResult> Format(string editorFormat)
        {
            propertyMetaDataBuilder.MetaData.EditorFormat = editorFormat;
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Watermark(string watermark)
        {
            propertyMetaDataBuilder.MetaData.Watermark = watermark;
            return propertyMetaDataBuilder;
        }
    }
}