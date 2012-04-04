using FluentMetadata.Builder;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class DisplayBuilderTests
    {
        private readonly Metadata metadata;
        private readonly IDisplayProperty<DummyClass, string> builder;

        public DisplayBuilderTests()
        {
            metadata = new Metadata();
            builder = new DisplayBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [Fact]
        public void DisplayBuilder_Ctor_NullText_IsNull()
        {
            Assert.Null(metadata.NullDisplayText);
        }

        [Fact]
        public void DisplayBuilder_Ctor_Format_IsNull()
        {
            Assert.Null(metadata.GetDisplayFormat());
        }

        [Fact]
        public void DisplayBuilder_Ctor_Name_IsNull()
        {
            Assert.Null(metadata.GetDisplayName());
        }

        [Fact]
        public void DisplayBuilder_NullText_NullText_IsValue()
        {
            builder.NullText("TheNullText");
            Assert.Equal("TheNullText", metadata.NullDisplayText);
            Assert.Null(metadata.GetDisplayName());
            Assert.Null(metadata.GetDisplayFormat());
        }

        [Fact]
        public void DisplayBuilder_Name_Name_IsValue()
        {
            builder.Name("TheNameText");
            Assert.Equal("TheNameText", metadata.GetDisplayName());
            Assert.Null(metadata.NullDisplayText);
            Assert.Null(metadata.GetDisplayFormat());
        }

        [Fact]
        public void DisplayBuilder_Name_Function_Equals_Metadata_DisplayName()
        {
            const string displayName = "asdf";
            builder.Name(() => displayName);
            Assert.Equal(displayName, metadata.GetDisplayName());
            Assert.Null(metadata.NullDisplayText);
            Assert.Null(metadata.GetDisplayFormat());
        }

        [Fact]
        public void DisplayBuilder_Format_Format_IsValue()
        {
            builder.Format("TheFormatText");
            Assert.Equal("TheFormatText", metadata.GetDisplayFormat());
            Assert.Null(metadata.NullDisplayText);
            Assert.Null(metadata.GetDisplayName());
        }

        [Fact]
        public void DisplayBuilder_Allset_IsValue()
        {
            builder.Format("TheFormatText");
            builder.Name("TheNameText");
            builder.NullText("TheNullText");
            Assert.Equal("TheFormatText", metadata.GetDisplayFormat());
            Assert.Equal("TheNameText", metadata.GetDisplayName());
            Assert.Equal("TheNullText", metadata.NullDisplayText);
        }
    }
}