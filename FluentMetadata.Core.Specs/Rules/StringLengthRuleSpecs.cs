using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Rules
{
    public class When_the_maximal_StringLength_is_50 : InstanceContextSpecification<StringLengthRule>
    {
        protected override StringLengthRule CreateSut()
        {
            return new StringLengthRule(50);
        }

        protected override void Because()
        {
        }

        [Observation]
        public void Should_Not_Valid_with_a_string_with_length_100()
        {
            var badLength = new string('x', 100);
            Sut.IsValid(badLength).ShouldBeFalse();
        }

        [Observation]
        public void Should_Valid_with_a_string_with_length_50()
        {
            var badLength = new string('x', 50);
            Sut.IsValid(badLength).ShouldBeTrue();
        }

        [Observation]
        public void Should_Valid_with_a_string_with_length_25()
        {
            var badLength = new string('x', 25);
            Sut.IsValid(badLength).ShouldBeTrue();
        }

        [Observation]
        public void Should_Not_Valid_with_a_string_with_length_51()
        {
            var badLength = new string('x', 51);
            Sut.IsValid(badLength).ShouldBeFalse();
        }

        [Observation]
        public void Should_Valid_with_a_string_with_length_49()
        {
            var badLength = new string('x', 49);
            Sut.IsValid(badLength).ShouldBeTrue();
        }

        [Observation]
        public void Should_Valid_with_a_string_with_length_0()
        {
            var badLength = string.Empty;
            Sut.IsValid(badLength).ShouldBeTrue();
        }

        [Observation]
        public void Should_Valid_with_a_string_is_NULL()
        {
            string  badLength = null;
            Sut.IsValid(badLength).ShouldBeTrue();
        }

    }
}