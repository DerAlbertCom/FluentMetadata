using System;
using System.Linq;
using FluentMetadata.Rules;
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

        [Fact]
        public void Generic_name_rule_is_valid_when_Username_is_not_equal_to_AutorName()
        {
            var nameRule = classMetadata.Rules
                .OfType<GenericClassRule<WebUser>>()
                .Single();

            var webUser = new WebUser();
            webUser.Username = "Holger";
            webUser.Autor = new Autor { Name = "Albert" };

            Assert.True(nameRule.IsValid(webUser));
        }

        [Fact]
        public void Generic_name_rule_is_invalid_when_Username_is_equal_to_AutorName()
        {
            var nameRule = classMetadata.Rules
                .OfType<GenericClassRule<WebUser>>()
                .Single();

            var webUser = new WebUser();
            webUser.Username = "Holger";
            webUser.Autor = new Autor { Name = "Holger" };

            Console.WriteLine(nameRule.FormatErrorMessage(classMetadata.GetDisplayName()));
            Assert.False(nameRule.IsValid(webUser));
        }
    }
}