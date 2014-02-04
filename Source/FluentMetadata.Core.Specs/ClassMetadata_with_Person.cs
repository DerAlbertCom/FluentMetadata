using System.Linq;
using FluentMetadata.Rules;
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
            classMetadata = query.GetMetadataFor(typeof(Person));
        }

        [Fact]
        public void Metadata_ModelType_is_Person()
        {
            Assert.Equal(typeof(Person), classMetadata.ModelType);
        }

        [Fact]
        public void Metadata_ModelName_is_Null()
        {
            Assert.Null(classMetadata.ModelName);
        }

        [Fact]
        public void Metadata_Display_is_Benutzer()
        {
            Assert.Equal("Benutzer", classMetadata.GetDisplayName());
        }

        [Fact]
        public void Instance_with_FirstName_different_from_LastName_is_invalid()
        {
            var rule = classMetadata.Rules.OfType<PropertyMustMatchRule<Person>>().Last();
            var person = new Person { FirstName = "foo", LastName = "bar" };
            Assert.False(rule.IsValid(person));
        }

        [Fact]
        public void Instance_with_FirstName_equal_to_LastName_is_valid()
        {
            var rule = classMetadata.Rules.OfType<PropertyMustMatchRule<Person>>().Last();
            var person = new Person { FirstName = "foo", LastName = "foo" };
            Assert.True(rule.IsValid(person));
        }
    }
}