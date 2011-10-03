using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    public class MetadataSetup
    {
        public MetadataSetup()
        {
            FluentMetadataBuilder.Reset();
            FluentMetadataBuilder.ForAssemblyOfType<Person>();
        }
    }
}