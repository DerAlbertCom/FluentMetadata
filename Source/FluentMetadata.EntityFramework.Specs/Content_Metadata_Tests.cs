using System;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class Content_Metadata_Tests : IUseFixture<MetadataSetup>
    {
        private QueryFluentMetadata query;

        public Content_Metadata_Tests()
        {
            query = new QueryFluentMetadata();
        }
        public void SetFixture(MetadataSetup data)
        {
        }

        [Fact]
        public void Content_Title_Is_Required()
        {
            var metaData = query.GetMetadataFor(typeof (Content), "Title");
            Assert.True(metaData.Required);
        }
    }
}