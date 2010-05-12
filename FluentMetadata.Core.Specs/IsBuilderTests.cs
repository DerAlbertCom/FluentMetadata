using Xunit;

namespace FluentMetadata.Specs
{
    public class IsBuilderTests
    {
        private readonly IIsProperty isBuilder;
        private readonly MetaData metaData;

        public IsBuilderTests()
        {
            metaData = new MetaData();
            isBuilder = new IsBuilder(new PropertyMetadataBuilder(metaData));
        }

        [Fact]
        public void IsBuilder_Ctor_IsNotRequired()
        {
            Assert.False(metaData.Required);
        }

        [Fact]
        public void IsBuilder_Ctor_IsNotReadOnly()
        {
            Assert.False(metaData.Readonly);
        }

        [Fact]
        public void IsBuilder_Required_IsRequired()
        {
            isBuilder.Required();
            Assert.True(metaData.Required);
        }

        [Fact]
        public void IsBuilder_Not_Required_IsNotRequired()
        {
            isBuilder.Not.Required();
            Assert.False(metaData.Required);
        }

        [Fact]
        public void IsBuilder_Readonly_IsReadOnly()
        {
            isBuilder.ReadOnly();
            Assert.True(metaData.Readonly);
        }

        [Fact]
        public void IsBuilder_Not_Readonly_IsNotReadOnly()
        {
            isBuilder.Not.ReadOnly();
            Assert.False(metaData.Readonly);
        }
    }
}