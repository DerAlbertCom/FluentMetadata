using Xunit;

namespace FluentMetadata.Specs
{
    public class AsBuilderTests
    {
        private readonly MetaData metaData;
        private readonly IAsProperty<DummyClass, string> asBuilder;

        public AsBuilderTests()
        {
            metaData = new MetaData();
            asBuilder = new AsBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metaData));
        }

        [Fact]
        public void AsBuilder_Ctor_DataTypeName_IsNull()
        {
            Assert.Null(metaData.DataTypeName);
        }

        [Fact]
        public void AsBuilder_EmailAdress_DataTypeName_is_EmailAdress()
        {
            asBuilder.EmailAddress();
            Assert.Equal("EmailAddress", metaData.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Url_DataTypeName_is_Url()
        {
            asBuilder.Url();
            Assert.Equal("Url", metaData.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Html_DataTypeName_is_Html()
        {
            asBuilder.Html();
            Assert.Equal("Html", metaData.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Text_DataTypeName_is_Text()
        {
            asBuilder.Text();
            Assert.Equal("Text", metaData.DataTypeName);
        }

        [Fact]
        public void AsBuilder_MultilineText_DataTypeName_is_MultilineText()
        {
            asBuilder.MultilineText();
            Assert.Equal("MultilineText", metaData.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Password_DataTypeName_is_Password()
        {
            asBuilder.Password();
            Assert.Equal("Password", metaData.DataTypeName);
        }
    }
}