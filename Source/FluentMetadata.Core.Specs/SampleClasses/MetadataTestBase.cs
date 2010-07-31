using System;
using Xunit;

namespace FluentMetadata.Specs.SampleClasses
{
    public abstract class MetadataTestBase : IUseFixture<MetadataSetup>
    {
        public void SetFixture(MetadataSetup data)
        {
        }
    }
}