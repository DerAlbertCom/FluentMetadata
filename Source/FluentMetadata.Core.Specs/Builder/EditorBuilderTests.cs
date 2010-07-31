using FluentMetadata.Builder;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class EditorBuilderTests
    {
        private readonly Metadata metadata;
        private IEditorProperty<DummyClass, string> builder;

        public EditorBuilderTests()
        {
            metadata = new Metadata();
            builder = new EditorBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [Fact]
        public void EditorBuilder_Ctor_ErrorMessage_IsNull()
        {
            Assert.Null(metadata.ErrorMessage);
        }

        [Fact]
        public void EditorBuilder_Ctor_Format_IsNull()
        {
            Assert.Null(metadata.EditorFormat);
        }

        [Fact]
        public void EditorBuilder_Ctor_Watermark_IsNull()
        {
            Assert.Null(metadata.Watermark);
        }

        [Fact]
        public void EditorBuilder_ErrorMessage_ErrorMessage_IsValue()
        {
            builder.ErrorMessage("TheNullText");
            Assert.Equal("TheNullText", metadata.ErrorMessage);
            Assert.Null(metadata.EditorFormat);
            Assert.Null(metadata.Watermark);
        }

        [Fact]
        public void EditorBuilder_Name_Name_IsValue()
        {
            builder.Watermark("TheNameText");
            Assert.Equal("TheNameText", metadata.Watermark);
            Assert.Null(metadata.EditorFormat);
        }

        [Fact]
        public void EditorBuilder_Format_Format_IsValue()
        {
            builder.Format("TheFormatText");
            Assert.Equal("TheFormatText", metadata.EditorFormat);
            Assert.Null(metadata.Watermark);
        }
    }
}