using FluentMetadata.Builder;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class AsBuilderTests
    {
        private readonly Metadata metadata;
        private readonly IAsProperty<DummyClass, string> asBuilder;

        public AsBuilderTests()
        {
            metadata = new Metadata();
            asBuilder = new AsBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [Fact]
        public void AsBuilder_Ctor_DataTypeName_IsNull()
        {
            Assert.Null(metadata.DataTypeName);
        }

        [Fact]
        public void AsBuilder_EmailAdress_DataTypeName_is_EmailAdress()
        {
            asBuilder.EmailAddress();
            Assert.Equal("EmailAddress", metadata.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Url_DataTypeName_is_Url()
        {
            asBuilder.Url();
            Assert.Equal("Url", metadata.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Html_DataTypeName_is_Html()
        {
            asBuilder.Html();
            Assert.Equal("Html", metadata.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Text_DataTypeName_is_Text()
        {
            asBuilder.Text();
            Assert.Equal("Text", metadata.DataTypeName);
        }

        [Fact]
        public void AsBuilder_MultilineText_DataTypeName_is_MultilineText()
        {
            asBuilder.MultilineText();
            Assert.Equal("MultilineText", metadata.DataTypeName);
        }

        [Fact]
        public void AsBuilder_Password_DataTypeName_is_Password()
        {
            asBuilder.Password();
            Assert.Equal("Password", metadata.DataTypeName);
        }
    }
}