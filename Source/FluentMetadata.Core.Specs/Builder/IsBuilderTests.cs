using FluentMetadata.Builder;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class IsBuilderTests
    {
        private readonly IIsProperty<DummyClass, string> isBuilder;
        private readonly Metadata metadata;

        public IsBuilderTests()
        {
            metadata = new Metadata();
            isBuilder = new IsBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [Fact]
        public void IsBuilder_Ctor_IsNotRequired()
        {
            Assert.False(metadata.Required.HasValue);
        }

        [Fact]
        public void IsBuilder_Ctor_IsNotReadOnly()
        {
            Assert.False(metadata.Readonly.HasValue);
        }

        [Fact]
        public void IsBuilder_Required_IsRequired()
        {
            isBuilder.Required();
            Assert.True(metadata.Required.Value);
        }

        [Fact]
        public void IsBuilder_Not_Required_IsNotRequired()
        {
            isBuilder.Not.Required();
            Assert.False(metadata.Required.Value);
        }

        [Fact]
        public void IsBuilder_Readonly_IsReadOnly()
        {
            isBuilder.ReadOnly();
            Assert.True(metadata.Readonly.Value);
        }

        [Fact]
        public void IsBuilder_Not_Readonly_IsNotReadOnly()
        {
            isBuilder.Not.ReadOnly();
            Assert.False(metadata.Readonly.Value);
        }
    }
}