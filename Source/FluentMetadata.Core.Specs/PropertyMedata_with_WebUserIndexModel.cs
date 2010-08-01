using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class PropertyMedata_with_WebUserIndexModel : MetadataTestBase
    {
        private Metadata username;
        private Metadata id;
        private Metadata autorName;

        public PropertyMedata_with_WebUserIndexModel()
        {
            var query = new QueryFluentMetadata();
            username = query.GetMetadataFor(typeof(WebUserIndexModel), "Username");
            id = query.GetMetadataFor(typeof(WebUserIndexModel), "Id");
            autorName = query.GetMetadataFor(typeof(WebUserIndexModel), "AutorName");
        }

        [Fact]
        public void Username_ModelName_is_Username()
        {
            Assert.Equal("Username", username.ModelName);
        }

        [Fact]
        public void Username_ModelType_is_string()
        {
            Assert.Equal(typeof(string), username.ModelType);
        }

        [Fact]
        public void Username_DisplayName_is_Benutzername()
        {
            Assert.Equal("Benutzername", username.DisplayName);
        }

        [Fact]
        public void Username_Required_is_true()
        {
            Assert.True(username.Required);
        }

        [Fact]
        public void Id_ModelName_is_Id()
        {
            Assert.Equal("Id", id.ModelName);
        }

        [Fact]
        public void Id_ModelType_is_int()
        {
            Assert.Equal(typeof(int), id.ModelType);
        }

        [Fact]
        public void Id_Required_is_false()
        {
            Assert.False(id.Required);
        }

        [Fact]
        public void AutorName_DisplayName_is_emaN()
        {
            Assert.Equal("emaN",autorName.DisplayName);
        }
    }
}