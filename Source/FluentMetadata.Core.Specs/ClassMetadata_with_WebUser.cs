using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class ClassMetadata_with_WebUser : MetadataTestBase
    {
        private Metadata classMetadata;

        public ClassMetadata_with_WebUser()
        {
            var query = new QueryFluentMetadata();
            classMetadata = query.GetMetadataFor(typeof(WebUser));
        }

        [Fact]
        public void ModelName_is_Null()
        {
            Assert.Null(classMetadata.ModelName);
        }

        [Fact]
        public void ModeType_is_WebUser()
        {
            Assert.Equal(typeof(WebUser), classMetadata.ModelType);
        }

        [Fact]
        public void DisplayName_is_Benutzer()
        {
            Assert.Equal("Benutzer", classMetadata.GetDisplayName());
        }

    }
}