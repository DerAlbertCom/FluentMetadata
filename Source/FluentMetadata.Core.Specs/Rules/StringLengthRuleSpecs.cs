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
            string badLength = null;
            Sut.IsValid(badLength).ShouldBeTrue();
        }
    }

    public class When_the_minimal_StringLength_is_8_and_Maximal_StringLength_is_null : InstanceContextSpecification<StringLengthRule>
    {
        protected override StringLengthRule CreateSut()
        {
            return new StringLengthRule(8, null);
        }

        protected override void Because()
        {
        }

        [Observation]
        public void Should_be_invalid_with_a_null_string()
        {
            Sut.IsValid(null).ShouldBeFalse();
        }

        [Observation]
        public void Should_be_invalid_with_a_string_with_length_7()
        {
            var value = new string('a', 7);
            Sut.IsValid(value).ShouldBeFalse();
        }

        [Observation]
        public void Should_be_valid_with_a_string_with_length_8()
        {
            var value = new string('a', 8);
            Sut.IsValid(value).ShouldBeTrue();
        }

        [Observation]
        public void Should_be_valid_with_a_string_with_length_4001()
        {
            var value = new string('a', 4001);
            Sut.IsValid(value).ShouldBeTrue();
        }
    }

    public class When_the_minimal_StringLength_is_5_and_Maximal_StringLength_is_250 : InstanceContextSpecification<StringLengthRule>
    {
        protected override StringLengthRule CreateSut()
        {
            return new StringLengthRule(5, 250);
        }

        protected override void Because()
        {
        }

        [Observation]
        public void Should_be_invalid_with_a_string_with_length_4()
        {
            var value = new string('a', 4);
            Sut.IsValid(value).ShouldBeFalse();
        }

        [Observation]
        public void Should_be_valid_with_a_string_with_length_5()
        {
            var value = new string('a', 5);
            Sut.IsValid(value).ShouldBeTrue();
        }

        [Observation]
        public void Should_be_valid_with_a_string_with_length_249()
        {
            var value = new string('a', 249);
            Sut.IsValid(value).ShouldBeTrue();
        }

        [Observation]
        public void Should_be_valid_with_a_string_with_length_250()
        {
            var value = new string('a', 249);
            Sut.IsValid(value).ShouldBeTrue();
        }

        [Observation]
        public void Should_be_invalid_with_a_string_with_length_251()
        {
            var value = new string('a', 251);
            Sut.IsValid(value).ShouldBeFalse();
        }
    }
}