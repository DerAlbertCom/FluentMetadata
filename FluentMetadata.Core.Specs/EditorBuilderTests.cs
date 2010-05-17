using Xunit;

namespace FluentMetadata.Specs
{
    public class EditorBuilderTests
    {
        private readonly MetaData metaData;
        private IEditorProperty<DummyClass, string> builder;

        public EditorBuilderTests()
        {
            metaData = new MetaData();
            builder = new EditorBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metaData));
        }

        [Fact]
        public void EditorBuilder_Ctor_ErrorMessage_IsNull()
        {
            Assert.Null(metaData.ErrorMessage);
        }

        [Fact]
        public void EditorBuilder_Ctor_Format_IsNull()
        {
            Assert.Null(metaData.EditorFormat);
        }

        [Fact]
        public void EditorBuilder_Ctor_Watermark_IsNull()
        {
            Assert.Null(metaData.Watermark);
        }

        [Fact]
        public void EditorBuilder_ErrorMessage_ErrorMessage_IsValue()
        {
            builder.ErrorMessage("TheNullText");
            Assert.Equal("TheNullText", metaData.ErrorMessage);
            Assert.Null(metaData.EditorFormat);
            Assert.Null(metaData.Watermark);
        }

        [Fact]
        public void EditorBuilder_Name_Name_IsValue()
        {
            builder.Watermark("TheNameText");
            Assert.Equal("TheNameText", metaData.Watermark);
            Assert.Null(metaData.EditorFormat);
        }

        [Fact]
        public void EditorBuilder_Format_Format_IsValue()
        {
            builder.Format("TheFormatText");
            Assert.Equal("TheFormatText", metaData.EditorFormat);
            Assert.Null(metaData.Watermark);
        }
    }
}