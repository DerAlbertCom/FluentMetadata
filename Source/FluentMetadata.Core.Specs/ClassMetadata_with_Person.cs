using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class ClassMetadata_with_Person : MetadataTestBase
    {
        private Metadata classMetadata;

        public ClassMetadata_with_Person()
        {
            var query = new QueryFluentMetadata();
            classMetadata = query.GetMetadataFor(typeof (Person));
        }

        [Fact]
        public void Metadata_ModelType_is_Person()
        {
            Assert.Equal(typeof (Person), classMetadata.ModelType);
        }

        [Fact]
        public void Metadata_ModelName_is_Null()
        {
                Assert.Null(classMetadata.ModelName);
//            Assert.Equal("Person", classMetadata.ModelName);
        }

        [Fact]
        public void Metadata_Display_is_Benutzer()
        {
            Assert.Equal("Benutzer", classMetadata.DisplayName);
        }
    }
}