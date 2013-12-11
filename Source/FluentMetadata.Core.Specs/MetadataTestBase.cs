using System;
using Xunit;

namespace FluentMetadata.Specs
{
    public abstract class MetadataTestBase : IUseFixture<MetadataSetup>
    {
        Exception exception;

        public void SetFixture(MetadataSetup data)
        {
            exception = data.Exception;
        }

        [Fact]
        public void MetadataSetupDoesNotThrowAnException()
        {
            Assert.Null(exception);
        }
    }
}