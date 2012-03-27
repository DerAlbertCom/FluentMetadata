using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class PropertyMedata_with_WebUserIndexGetModel : MetadataTestBase
    {
        Metadata username, id, autorName, email, role;

        public PropertyMedata_with_WebUserIndexGetModel()
        {
            var query = new QueryFluentMetadata();
            username = query.GetMetadataFor(typeof(WebUserIndexGetModel), "Username");
            id = query.GetMetadataFor(typeof(WebUserIndexGetModel), "Id");
            email = query.GetMetadataFor(typeof(WebUserIndexGetModel), "EMail");
            autorName = query.GetMetadataFor(typeof(WebUserIndexGetModel), "AutorName");
            role = query.GetMetadataFor(typeof(WebUserIndexGetModel), "Role");
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
            Assert.Equal("Benutzername", username.GetDisplayName());
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
            Assert.Equal("emaN", autorName.GetDisplayName());
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

        [Fact]
        public void Role_TemplateHint_is_Roles()
        {
            Assert.Equal("Roles", role.TemplateHint);
        }

        [Fact]
        public void EMail_Watermark_is_dummy_address()
        {
            Assert.Equal("john@doe.com", email.Watermark);
        }

        [Fact]
        public void Username_ConvertEmptyStringToNull_is_false()
        {
            Assert.False(username.ConvertEmptyStringToNull);
        }

        [Fact]
        public void Id_Hidden_is_true()
        {
            Assert.True(id.Hidden.HasValue);
            Assert.True(id.Hidden.Value);
        }

        [Fact]
        public void Username_GetMaximumLength_is_256()
        {
            var maxLength = username.GetMaximumLength();
            Assert.True(maxLength.HasValue);
            Assert.Equal(256, maxLength);
        }

        [Fact]
        public void Username_ContainerType_is_WebUserIndexGetModel()
        {
            Assert.Equal(typeof(WebUserIndexGetModel), username.ContainerType);
        }
    }
}