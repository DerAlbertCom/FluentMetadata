using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class ClassMetadata_with_WebUserIndexModel
    {
        private Metadata classMetadata;

        public ClassMetadata_with_WebUserIndexModel()
        {
            var query = new QueryFluentMetadata();
            classMetadata = query.GetMetadataFor(typeof(WebUserIndexModel));
        }

        [Fact]
        public void ModelName_is_WebUserIndexModel()
        {
            Assert.Equal("WebUserIndexModel", classMetadata.ModelName);
        }

        [Fact]
        public void ModeType_is_WebUserIndexModel()
        {
            Assert.Equal(typeof(WebUserIndexModel), classMetadata.ModelType);
        }

        [Fact]
        public void DisplayName_is_Benutzer()
        {
            Assert.Equal("Benutzer",classMetadata.DisplayName);
        }
    }
}