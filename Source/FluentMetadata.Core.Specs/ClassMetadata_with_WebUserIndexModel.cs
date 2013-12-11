using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class ClassMetadata_with_WebUserIndexModel : MetadataTestBase
    {
        readonly Metadata classMetadata;

        public ClassMetadata_with_WebUserIndexModel()
        {
            classMetadata = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexModel));
        }

        [Fact]
        public void ModelName_is_Null()
        {
            Assert.Null(classMetadata.ModelName);
        }

        [Fact]
        public void ModelType_is_WebUserIndexModel()
        {
            Assert.Equal(typeof(WebUserIndexModel), classMetadata.ModelType);
        }

        [Fact]
        public void DisplayName_is_Benutzer()
        {
            Assert.Equal("Benutzer", classMetadata.GetDisplayName());
        }
    }

    public class ClassMetadata_with_WebUserIndexGetModel : MetadataTestBase
    {
        readonly Metadata classMetadata;

        public ClassMetadata_with_WebUserIndexGetModel()
        {
            classMetadata = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel));
        }

        [Fact]
        public void DisplayName_is_Benutzer()
        {
            Assert.Equal("Benutzer", classMetadata.GetDisplayName());
        }
    }
}