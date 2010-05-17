using Xunit;

namespace FluentMetadata.Specs
{
    public class DisplayBuilderTests
    {
        private readonly MetaData metaData;
        private readonly IDisplayProperty<DummyClass, string> builder;

        public DisplayBuilderTests()
        {
            metaData = new MetaData();
            builder = new DisplayBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metaData));
        }

        [Fact]
        public void DisplayBuilder_Ctor_NullText_IsNull()
        {
            Assert.Null(metaData.NullDisplayText);
        }

        [Fact]
        public void DisplayBuilder_Ctor_Format_IsNull()
        {
            Assert.Null(metaData.DisplayFormat);
        }

        [Fact]
        public void DisplayBuilder_Ctor_Name_IsNull()
        {
            Assert.Null(metaData.DisplayName);
        }

        [Fact]
        public void DisplayBuilder_NullText_NullText_IsValue()
        {
            builder.NullText("TheNullText");
            Assert.Equal("TheNullText", metaData.NullDisplayText);
            Assert.Null(metaData.DisplayName);
            Assert.Null(metaData.DisplayFormat);
        }

        [Fact]
        public void DisplayBuilder_Name_Name_IsValue()
        {
            builder.Name("TheNameText");
            Assert.Equal("TheNameText", metaData.DisplayName);
            Assert.Null(metaData.NullDisplayText);
            Assert.Null(metaData.DisplayFormat);
        }

        [Fact]
        public void DisplayBuilder_Format_Format_IsValue()
        {
            builder.Format("TheFormatText");
            Assert.Equal("TheFormatText", metaData.DisplayFormat);
            Assert.Null(metaData.NullDisplayText);
            Assert.Null(metaData.DisplayName);
        }

        [Fact]
        public void DisplayBuilder_Allset_IsValue()
        {
            builder.Format("TheFormatText");
            builder.Name("TheNameText");
            builder.NullText("TheNullText");
            Assert.Equal("TheFormatText", metaData.DisplayFormat);
            Assert.Equal("TheNameText", metaData.DisplayName);
            Assert.Equal("TheNullText", metaData.NullDisplayText);
        }

    }
}