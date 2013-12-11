using System.Linq;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs
{
    public class MetadataTests
    {
        readonly Metadata metadata;

        public MetadataTests()
        {
            metadata = new Metadata();
        }

        [Fact]
        public void RulesAreEmptyAndRequiredHasNoValueWhenCreatingANewInstance()
        {
            Assert.False(metadata.Required.HasValue);
            Assert.Equal(0, metadata.Rules.Count());
        }

        [Fact]
        public void SettingRequiredAddsARequiredRule()
        {
            metadata.Required = true;
            Assert.Equal(1, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [Fact]
        public void SettingNotRequiredAfterSettingRequiredRemovesTheRequiredAgain()
        {
            metadata.Required = true;
            metadata.Required = false;
            Assert.Equal(0, metadata.Rules.OfType<RequiredRule>().Count());
        }
    }
}