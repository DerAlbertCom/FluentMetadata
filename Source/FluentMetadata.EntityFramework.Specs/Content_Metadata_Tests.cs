using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class Content_Metadata_Tests : IUseFixture<MetadataSetup>
    {
        public void SetFixture(MetadataSetup data)
        {
        }

        [Fact]
        public void Content_Title_Is_Required()
        {
            var metaData = QueryFluentMetadata.GetMetadataFor(typeof(Content), "Title");
            Assert.True(metaData.Required.Value);
        }
    }
}