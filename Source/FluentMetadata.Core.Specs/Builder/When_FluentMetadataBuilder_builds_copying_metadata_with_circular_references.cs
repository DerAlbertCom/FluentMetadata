using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class When_FluentMetadataBuilder_builds_copying_metadata_with_circular_references
    {
        Exception exception;

        public When_FluentMetadataBuilder_builds_copying_metadata_with_circular_references()
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
            var type = typeof(When_FluentMetadataBuilder_builds_copying_metadata_with_circular_references);
            return type.Assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(type.FullName) &&
                    t.Is<IClassMetadata>());
        }

        [Fact]
        public void It_throws_a_CircularRefenceException()
        {
            Assert.IsType<MetadataDefinitionSorter.CircularRefenceException>(exception);
        }

        [Fact]
        public void The_CircularRefenceException_contains_the_full_name_of_each_type_building_the_circular_reference()
        {
            GetUnbuildableMetadataDefinitions()
                .ToList()
                .ForEach(t => Assert.Contains(t.FullName, exception.Message));
        }

        #region metadata with circular references

        class SomeType { }
        class SomeOtherType { }
        class SomeThirdType { }
        class SomeTypeMetadata : ClassMetadata<SomeType>
        {
            public SomeTypeMetadata()
            {
                CopyMetadataFrom<SomeOtherType>();
            }
        }
        class SomeOtherTypeMetadata : ClassMetadata<SomeOtherType>
        {
            public SomeOtherTypeMetadata()
            {
                CopyMetadataFrom<SomeThirdType>();
            }
        }
        class SomeThirdTypeMetadata : ClassMetadata<SomeThirdType>
        {
            public SomeThirdTypeMetadata()
            {
                CopyMetadataFrom<SomeType>();
            }
        }

        #endregion
    }
}