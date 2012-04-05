using System;
using System.Collections.Generic;
using System.Linq;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata
    {
        readonly List<Type> builtMetadata = FluentMetadataBuilder.BuiltMetadataDefininitions;
        readonly IEnumerable<IRule> someViewModelRules, someViewModelMyPropertyRules, someViewModelMyStringPropertyRules;
        readonly Exception exception;

        public When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata()
        {
            FluentMetadataBuilder.Reset();
            var typesToBuildInLaterBatch = new[] { typeof(SomeTypeInAnotherAssemblyMetadata) };
            try
            {
                FluentMetadataBuilder.BuildMetadataDefinitions(
                    GetUnbuildableMetadataDefinitions()
                        .Except(typesToBuildInLaterBatch));
                FluentMetadataBuilder.BuildMetadataDefinitions(typesToBuildInLaterBatch);
                someViewModelRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel)).Rules;
                someViewModelMyPropertyRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel), "MyProperty").Rules;
                someViewModelMyStringPropertyRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel), "MyStringProperty").Rules;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        internal static IEnumerable<Type> GetUnbuildableMetadataDefinitions()
        {
            var type = typeof(When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata);
            return type.Assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(type.FullName) &&
                    t.Is<IClassMetadata>());
        }

        [Fact]
        public void It_does_not_throw_an_exception()
        {
            Assert.Null(exception);
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
        public void Dependent_metadata_built_in_a_later_batch_is_built_correctly()
        {
            Assert.Equal(1, builtMetadata.Count(t => t == typeof(SomeTypeInAnotherAssemblyMetadata)));
        }

        [Fact]
        public void It_does_not_duplicate_generic_class_rules()
        {
            Assert.Equal(1, someViewModelRules.OfType<GenericClassRule<SomeViewModel>>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_PropertyMustBeLessThanOtherRules()
        {
            Assert.Equal(1, someViewModelRules.OfType<PropertyMustBeLessThanOtherRule<SomeViewModel>>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_PropertyMustMatchRules()
        {
            Assert.Equal(1, someViewModelRules.OfType<PropertyMustMatchRule<SomeViewModel>>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_RequiredRules()
        {
            Assert.Equal(1, someViewModelMyPropertyRules.OfType<RequiredRule>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_PropertyMustMatchRegexRules()
        {
            Assert.Equal(1, someViewModelMyStringPropertyRules.OfType<PropertyMustMatchRegexRule>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_RangeRules()
        {
            Assert.Equal(1, someViewModelMyPropertyRules.OfType<RangeRule>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_StringLengthRules()
        {
            Assert.Equal(1, someViewModelMyStringPropertyRules.OfType<StringLengthRule>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_generic_property_rules()
        {
            Assert.Equal(1, someViewModelMyPropertyRules.OfType<GenericRule<int>>().Count());
        }

        [Fact]
        public void It_does_not_duplicate_ClassRuleValidatingAPropertyWrapper()
        {
            Assert.Equal(1, someViewModelMyPropertyRules.OfType<ClassRuleValidatingAPropertyWrapper>().Count());
        }

        #region System under test

        #region dependent metadata is defined before its dependency

        class SomeDomainModel : SomeDomainBaseType { }
        class SomeViewModel
        {
            public int MyProperty { get; set; }
            public int MyProperty2 { get; set; }
            public string MyStringProperty { get; set; }
        }
        class SomeViewModelMetadata : ClassMetadata<SomeViewModel>
        {
            public SomeViewModelMetadata()
            {
                CopyMetadataFrom<SomeDomainModel>();
                Class
                    .AssertThat(
                        svm => false,
                        string.Empty)
                    .ComparableProperty(svm => svm.MyProperty)
                        .ShouldBeLessThan(svm => svm.MyProperty2)
                    .Property(svm => svm.MyProperty)
                       .ShouldEqual(svm => svm.MyStringProperty);
                Property(svm => svm.MyProperty)
                    .Is.Required()
                    .Range(0, 1)
                    .AssertThat(
                        v => v > 100,
                        string.Empty);
                Property(svm => svm.MyStringProperty)
                    .Should.MatchRegex("here be some regex")
                    .Length(1, 1);
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

        #region metadata built in a later batch

        class SomeTypeInAnotherAssembly { }
        class SomeTypeInAnotherAssemblyMetadata : ClassMetadata<SomeTypeInAnotherAssembly>
        {
            public SomeTypeInAnotherAssemblyMetadata()
            {
                CopyMetadataFrom<SomeDomainModel>();
            }
        }

        #endregion

        #endregion
    }
}