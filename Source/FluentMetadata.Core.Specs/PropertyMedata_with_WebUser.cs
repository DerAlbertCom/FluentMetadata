using System;
using System.Linq;
using FluentMetadata.Rules;
using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class PropertyMedata_with_WebUser : MetadataTestBase
    {
        Metadata lastLogin, username, id, passWordHash, role, bounceCount;

        public PropertyMedata_with_WebUser()
        {
            var query = new QueryFluentMetadata();
            username = query.GetMetadataFor(typeof(WebUser), "Username");
            id = query.GetMetadataFor(typeof(WebUser), "Id");
            lastLogin = query.GetMetadataFor(typeof(WebUser), "LastLogin");
            passWordHash = query.GetMetadataFor(typeof(WebUser), "PasswordHash");
            role = query.GetMetadataFor(typeof(WebUser), "Role");
            bounceCount = query.GetMetadataFor(typeof(WebUser), "BounceCount");
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
        public void Username_MinLength_is_3()
        {
            Assert.Equal(3, username.GetMinimumLength());
        }

        [Fact]
        public void Username_MaxLength_is_256()
        {
            Assert.Equal(256, username.GetMaximumLength());
        }

        [Fact]
        public void PassWordHash_MinLength_is_32()
        {
            Assert.Equal(32, passWordHash.GetMinimumLength());
        }

        [Fact]
        public void PassWordHash_MaxLength_is_null()
        {
            Assert.Null(passWordHash.GetMaximumLength());
        }

        [Fact]
        public void Role_MinLength_is_null()
        {
            Assert.Null(role.GetMinimumLength());
        }

        [Fact]
        public void Role_MaxLength_is_256()
        {
            Assert.Equal(256, role.GetMaximumLength());
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

        [Fact]
        public void Generic_bounceCount_rule_is_valid_when_email_has_bounced_twice()
        {
            var bounceCountRule = bounceCount.Rules
                .OfType<GenericRule<int>>()
                .Single();
            var webUser = new WebUser();

            webUser.MailHasBounced();
            webUser.MailHasBounced();

            Assert.True(bounceCountRule.IsValid(webUser.BounceCount));
        }

        [Fact]
        public void Generic_bounceCount_rule_is_invalid_when_email_has_bounced_thrice()
        {
            var bounceCountRule = bounceCount.Rules
                .OfType<GenericRule<int>>()
                .Single();
            var webUser = new WebUser();

            webUser.MailHasBounced();
            webUser.MailHasBounced();
            webUser.MailHasBounced();

            Console.WriteLine(bounceCountRule.FormatErrorMessage(bounceCount.GetDisplayName()));
            Assert.False(bounceCountRule.IsValid(webUser.BounceCount));
        }
    }
}