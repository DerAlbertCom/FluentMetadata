using System;
using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class PropertyMedata_with_WebUser : MetadataTestBase
    {
        private Metadata lastLogin;
        private Metadata username;
        private Metadata id;

        public PropertyMedata_with_WebUser()
        {
            var query = new QueryFluentMetadata();
            username = query.GetMetadataFor(typeof(WebUser), "Username");
            id = query.GetMetadataFor(typeof(WebUser), "Id");
            lastLogin = query.GetMetadataFor(typeof(WebUser), "LastLogin");
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
            Assert.True(username.Required.Value);
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
            Assert.False(id.Required.HasValue);
        }

        [Fact]
        public void Last_Login_Minimum_is_2010_1_23()
        {
            Assert.Equal(new DateTime(2010, 1, 23), lastLogin.GetRangeMinimum());
        }

        [Fact]
        public void Last_Login_Maximum_is_DoomsDay()
        {
            Assert.Equal(DateTime.MaxValue, lastLogin.GetRangeMaximum());
        }
    }
}