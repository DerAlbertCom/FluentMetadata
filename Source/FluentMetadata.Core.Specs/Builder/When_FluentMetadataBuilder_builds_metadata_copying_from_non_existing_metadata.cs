using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class When_FluentMetadataBuilder_builds_metadata_copying_from_non_existing_metadata
    {
        readonly Exception exception;

        public When_FluentMetadataBuilder_builds_metadata_copying_from_non_existing_metadata()
        {
            FluentMetadataBuilder.Reset();

            try
            {
                FluentMetadataBuilder.BuildMetadataDefinitions(
                    GetUnbuildableMetadataDefinitions());
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        internal static IEnumerable<Type> GetUnbuildableMetadataDefinitions()
        {
            var type = typeof(When_FluentMetadataBuilder_builds_metadata_copying_from_non_existing_metadata);
            return type.Assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(type.FullName) &&
                    t.Is<IClassMetadata>());
        }

        [Fact]
        public void It_throws_a_NoMetadataDefinedException()
        {
            Assert.IsType<MetadataDefinitionSorter.NoMetadataDefinedException>(exception);
        }

        [Fact]
        public void The_NoMetadataDefinedException_contains_the_full_name_of_the_metadata_type_that_is_unbuildable()
        {
            Assert.Contains(typeof(SomeTypeMetadata).FullName, exception.Message);
        }

        [Fact]
        public void The_NoMetadataDefinedException_contains_the_full_name_of_the_model_type_whose_metadata_cannot_be_found()
        {
            Assert.Contains(typeof(SomeOtherType).FullName, exception.Message);
        }

        #region metadata copying from non-existing

        class SomeType { }
        class SomeOtherType { }

        class SomeTypeMetadata : ClassMetadata<SomeType>
        {
            public SomeTypeMetadata()
            {
                CopyMetadataFrom<SomeOtherType>();
            }
        }

        #endregion
    }
}