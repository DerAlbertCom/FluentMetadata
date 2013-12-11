using System;
using System.Linq;
using FluentMetadata.Specs.Builder;
using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    public class MetadataSetup
    {
        internal Exception Exception { get; private set; }

        public MetadataSetup()
        {
            FluentMetadataBuilder.Reset();
            try
            {
                FluentMetadataBuilder.BuildMetadataDefinitions(
                    typeof(Person).Assembly.GetTypes()
                        .Where(t => t.Is<IClassMetadata>())
                        .Except(When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata.GetUnbuildableMetadataDefinitions())
                        .Except(When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata_that_does_not_apply.GetUnbuildableMetadataDefinitions())
                        .Except(When_FluentMetadataBuilder_builds_copying_metadata_with_circular_references.GetUnbuildableMetadataDefinitions())
                        .Except(When_FluentMetadataBuilder_builds_metadata_copying_from_non_existing_metadata.GetUnbuildableMetadataDefinitions()));
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }
    }
}