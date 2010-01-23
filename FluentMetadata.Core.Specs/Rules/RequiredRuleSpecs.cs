using System;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Rules
{
    [Concern(typeof (RequiredRule))]
    public class RequiredRuleSpecs : InstanceContextSpecification<RequiredRule>
    {
        protected override void Because()
        {
        }

        protected override RequiredRule CreateSut()
        {
            return new RequiredRule();
        }

        [Observation]
        public void NULL_is_not_Valid()
        {
            Sut.IsValid(null).ShouldBeFalse();
        }

        [Observation]
        public void EmptyString_is_not_Valid()
        {
            Sut.IsValid("").ShouldBeFalse();
        }

        [Observation]
        public void AnObject_is_Valid()
        {
            Sut.IsValid(DateTime.Now).ShouldBeTrue();
        }

        [Observation]
        public void AString_is_Valid()
        {
            Sut.IsValid("hallo").ShouldBeTrue();
        }

        [Observation]
        public void Number_99_is_Valid()
        {
            Sut.IsValid(99).ShouldBeTrue();
        }

        [Observation]
        public void Number_99_1_is_Valid()
        {
            Sut.IsValid(99.1).ShouldBeTrue();
        }
    }
}