using System;
using System.Collections.Generic;
using System.Linq;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata_that_does_not_apply
    {
        readonly List<Type> builtMetadata = FluentMetadataBuilder.BuiltMetadataDefininitions;
        readonly IEnumerable<IRule> someViewModelRules, someViewModelMyPropertyRules;
        readonly Exception exception;

        public When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata_that_does_not_apply()
        {
            FluentMetadataBuilder.Reset();

            try
            {
                FluentMetadataBuilder.BuildMetadataDefinitions(GetUnbuildableMetadataDefinitions());
                someViewModelRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel)).Rules;
                someViewModelMyPropertyRules = QueryFluentMetadata.GetMetadataFor(typeof(SomeViewModel), "MyProperty").Rules;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        internal static IEnumerable<Type> GetUnbuildableMetadataDefinitions()
        {
            var type = typeof(When_FluentMetadataBuilder_builds_metadata_copying_from_other_metadata_that_does_not_apply);
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
        public void It_does_not_copy_generic_class_rules()
        {
            Assert.Equal(0, someViewModelRules.OfType<GenericClassRule<SomeDomainModel>>().Count());
        }

        [Fact]
        public void It_does_not_copy_PropertyMustBeLessThanOtherRules()
        {
            Assert.Equal(0, someViewModelRules.OfType<PropertyMustBeLessThanOtherRule<SomeDomainModel>>().Count());
        }

        [Fact]
        public void It_does_not_copy_PropertyMustMatchRules()
        {
            Assert.Equal(0, someViewModelRules.OfType<PropertyMustMatchRule<SomeDomainModel>>().Count());
        }

        [Fact]
        public void It_does_copy_RequiredRules()
        {
            Assert.Equal(1, someViewModelMyPropertyRules.OfType<RequiredRule>().Count());
        }

        [Fact]
        public void It_does_not_copy_PropertyMustMatchRegexRules()
        {
            Assert.Equal(0, someViewModelMyPropertyRules.OfType<PropertyMustMatchRegexRule>().Count());
        }

        [Fact]
        public void It_does_not_copy_RangeRules()
        {
            Assert.Equal(0, someViewModelMyPropertyRules.OfType<RangeRule>().Count());
        }

        [Fact]
        public void It_does_not_copy_StringLengthRules()
        {
            Assert.Equal(0, someViewModelMyPropertyRules.OfType<StringLengthRule>().Count());
        }

        [Fact]
        public void It_does_not_copy_generic_property_rules()
        {
            Assert.Equal(0, someViewModelMyPropertyRules.OfType<GenericRule<int>>().Count());
        }

        [Fact]
        public void It_does_not_copy_ClassRuleValidatingAPropertyWrapper()
        {
            Assert.Equal(0, someViewModelMyPropertyRules.OfType<ClassRuleValidatingAPropertyWrapper>().Count());
        }

        #region System under test

        class SomeDomainModel
        {
            public int MyProperty { get; set; }
            public int MyProperty2 { get; set; }
        }
        class SomeViewModel
        {
            public string MyProperty { get; set; }
        }
        class SomeDomainModelMetadata : ClassMetadata<SomeDomainModel>
        {
            public SomeDomainModelMetadata()
            {
                Class
                    .AssertThat(
                        svm => false,
                        string.Empty)
                    .ComparableProperty(svm => svm.MyProperty)
                        .ShouldBeLessThan(svm => svm.MyProperty2)
                    .Property(svm => svm.MyProperty)
                        .ShouldEqual(svm => svm.MyProperty2);
                Property(svm => svm.MyProperty)
                    .Is.Required()
                    .Should.MatchRegex("here be some regex")
                    .Range(0, 1)
                    .Length(1, 1)
                    .AssertThat(
                        v => v > 100,
                        string.Empty);
            }
        }
        class SomeViewModelMetadata : ClassMetadata<SomeViewModel>
        {
            public SomeViewModelMetadata()
            {
                CopyMetadataFrom<SomeDomainModel>();
            }
        }

        #endregion
    }
}