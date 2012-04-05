using System;
using System.Collections.Generic;
using System.Linq;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata : MetadataTestBase
    {
        readonly List<Type> builtMetadata = FluentMetadataBuilder.BuiltMetadataDefininitions;
        readonly IEnumerable<IRule> someViewModelRules;

        public When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata()
        {
            someViewModelRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel)).Rules;
        }

        [Fact]
        public void Dependent_metadata_may_be_built_before_its_dependency()
        {
            Assert.True(
                builtMetadata.IndexOf(typeof(SomeViewModelMetadata)) <
                builtMetadata.IndexOf(typeof(SomeDomainModelMetadata)));
        }

        [Fact]
        public void Dependent_metadata_is_built_again_after_its_dependencies_because_it_copies_metadata_from_them()
        {
            Assert.Equal(2, builtMetadata.Count(t => t == typeof(SomeViewModelMetadata)));
            Assert.True(
                builtMetadata.LastIndexOf(typeof(SomeDomainModelMetadata)) <
                builtMetadata.LastIndexOf(typeof(SomeViewModelMetadata)));
        }

        [Fact]
        public void Open_generic_metadata_is_built_before_non_generic_metadata()
        {
            Assert.True(
                builtMetadata.IndexOf(typeof(SomeDomainBaseTypeMetadata<>)) <
                builtMetadata.IndexOf(typeof(SomeDomainModelMetadata)));
        }

        [Fact]
        public void Dependent_metadata_is_built_again_if_dependency_is_built_again()
        {
            Assert.Equal(2, builtMetadata.Count(t => t == typeof(SomeOtherViewModelMetadata)));
            Assert.True(
                builtMetadata.LastIndexOf(typeof(SomeViewModelMetadata)) <
                builtMetadata.LastIndexOf(typeof(SomeOtherViewModelMetadata)));
        }

        [Fact]
        public void It_does_not_duplicate_generic_class_rules()
        {
            Assert.Equal(1, someViewModelRules.OfType<GenericClassRule<SomeViewModel>>().Count());
        }

        #region System under test

        #region dependent metadata is defined before its dependency

        class SomeDomainModel : SomeDomainBaseType { }
        class SomeViewModel { }
        class SomeViewModelMetadata : ClassMetadata<SomeViewModel>
        {
            public SomeViewModelMetadata()
            {
                CopyMetadataFrom<SomeDomainModel>();
                Class
                    .AssertThat(
                        svm => false,
                        string.Empty);
            }
        }
        class SomeDomainModelMetadata : SomeDomainBaseTypeMetadata<SomeDomainModel> { }

        #endregion

        #region open generic metadata is defined after non generic metadata

        class SomeDomainBaseType { }
        class SomeDomainBaseTypeMetadata<T> : ClassMetadata<T> where T : SomeDomainBaseType { }

        #endregion

        #region metadata depentent on incorrectly build metadata

        class SomeOtherViewModel { }
        class SomeOtherViewModelMetadata : ClassMetadata<SomeOtherViewModel>
        {
            public SomeOtherViewModelMetadata()
            {
                CopyMetadataFrom<SomeViewModel>();
            }
        }

        #endregion

        #endregion
    }
}