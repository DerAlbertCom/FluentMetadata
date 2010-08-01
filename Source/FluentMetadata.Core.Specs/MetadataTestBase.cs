using Xunit;

namespace FluentMetadata.Specs
{
    public abstract class MetadataTestBase : IUseFixture<MetadataSetup>
    {
        public void SetFixture(MetadataSetup data)
        {
        }
    }
}