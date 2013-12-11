using System.Linq;
using FluentMetadata.Builder;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class IsBuilderTests
    {
        readonly IIsProperty<DummyClass, string> isBuilder;
        readonly Metadata metadata;

        public IsBuilderTests()
        {
            metadata = new Metadata();
            isBuilder = new IsBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [Fact]
        public void IsBuilder_Ctor_IsNotSet()
        {
            Assert.False(metadata.Required.HasValue);
        }

        [Fact]
        public void IsBuilder_Ctor_IsNotReadOnly()
        {
            Assert.False(metadata.ReadOnly);
        }

        [Fact]
        public void SettingRequiredResultsInMetadataRequiredAnd1RequiredRule()
        {
            isBuilder.Required();
            Assert.True(metadata.Required.Value);
            Assert.Equal(1, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [Fact]
        public void SettingNotRequiredResultsInMetadataNotRequiredAnd0RequiredRules()
        {
            isBuilder.Not.Required();
            Assert.False(metadata.Required.Value);
            Assert.Equal(0, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [Fact]
        public void SettingNotRequiredAfterRequiredResultsInMetadataNotRequiredAnd0RequiredRules()
        {
            isBuilder.Required();
            isBuilder.Not.Required();
            Assert.False(metadata.Required.Value);
            Assert.Equal(0, metadata.Rules.OfType<RequiredRule>().Count());
        }

        [Fact]
        public void IsBuilder_Readonly_IsReadOnly()
        {
            isBuilder.ReadOnly();
            Assert.True(metadata.ReadOnly);
        }

        [Fact]
        public void IsBuilder_Not_Readonly_IsNotReadOnly()
        {
            isBuilder.Not.ReadOnly();
            Assert.False(metadata.ReadOnly);
        }
    }
}