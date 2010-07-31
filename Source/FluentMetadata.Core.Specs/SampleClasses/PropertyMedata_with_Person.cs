using Xunit;

namespace FluentMetadata.Specs.SampleClasses
{
    public class PropertyMedata_with_Person : MetadataTestBase
    {
        private Metadata firstName;
        private Metadata lastName;

        public PropertyMedata_with_Person()
        {
            var query = new QueryFluentMetadata();
            firstName = query.GetMetadataFor(typeof (Person), "FirstName");
            lastName = query.GetMetadataFor(typeof (Person), "LastName");
        }

        [Fact]
        public void FirstName_ModelName_is_FirstName()
        {
            Assert.Equal("FirstName", firstName.ModelName);
        }

        [Fact]
        public void FirstName_ModelType_is_string()
        {
            Assert.Equal(typeof (string), firstName.ModelType);
        }

        [Fact]
        public void FirstName_Required_is_true()
        {
            Assert.True(firstName.Required);
        }

        [Fact]
        public void LastName_ModelName_is_LastName()
        {
            Assert.Equal("LastName", lastName.ModelName);
        }

        [Fact]
        public void LastName_ModelType_is_string()
        {
            Assert.Equal(typeof (string), lastName.ModelType);
        }

        [Fact]
        public void LastName_Required_is_false()
        {
            Assert.False(lastName.Required);
        }
    }
}