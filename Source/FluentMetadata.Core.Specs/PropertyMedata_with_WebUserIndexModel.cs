using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class PropertyMedata_with_WebUserIndexModel : MetadataTestBase
    {
        Metadata username, id, autorName, email;

        public PropertyMedata_with_WebUserIndexModel()
        {
            var query = new QueryFluentMetadata();
            username = query.GetMetadataFor(typeof(WebUserIndexModel), "Username");
            id = query.GetMetadataFor(typeof(WebUserIndexModel), "Id");
            email = query.GetMetadataFor(typeof(WebUserIndexModel), "EMail");
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
        public void AutorName_DisplayName_is_emaN()
        {
            Assert.Equal("emaN", autorName.DisplayName);
        }

        [Fact]
        public void EMail_DataTypeName_is_EmailAddress()
        {
            Assert.Equal("EmailAddress", email.DataTypeName);
        }

        [Fact]
        public void Username_Description_is_Name_des_Benutzers()
        {
            Assert.Equal("Name des Benutzers", username.Description);
        }

        [Fact]
        public void EMail_DisplayFormat_is_MailtoLink()
        {
            Assert.Equal("<a href='mailto:{0}'>{0}</a>", email.DisplayFormat);
        }

        [Fact]
        public void EMail_EditorFormat_is_plain_value()
        {
            Assert.Equal("{0}", email.EditorFormat);
        }

        [Fact]
        public void Id_HideSurroundingHtml_is_true()
        {
            Assert.True(id.HideSurroundingHtml.HasValue);
            Assert.True(id.HideSurroundingHtml.Value);
        }

        [Fact]
        public void Username_ReadOnly_is_true()
        {
            Assert.True(username.Readonly);
        }

        [Fact]
        public void AutorName_NullDisplayText_is_Anonymous_Autor()
        {
            Assert.Equal("Anonymous Autor", autorName.NullDisplayText);
        }

        [Fact]
        public void Id_ShowDisplay_is_false()
        {
            Assert.False(id.ShowDisplay);
        }

        [Fact]
        public void Id_ShowEditor_is_false()
        {
            Assert.False(id.ShowEditor);
        }
    }
}