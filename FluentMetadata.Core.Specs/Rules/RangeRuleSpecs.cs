using System;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Rules
{
    [Concern(typeof (RangeRule))]
    public abstract class ConcernOfRangeRule : InstanceContextSpecification<RangeRule>
    {
        protected override void Because()
        {
        }
    }

    public class When_The_Range_Is_Between_100_and_200 : ConcernOfRangeRule
    {
        protected override RangeRule CreateSut()
        {
            return new RangeRule(100, 200);
        }

        [Observation]
        public void Should_150_is_in_range()
        {
            Sut.IsValid(150).ShouldBeTrue();
        }

        [Observation]
        public void Should_100_is_in_range()
        {
            Sut.IsValid(100).ShouldBeTrue();
        }

        [Observation]
        public void Should_200_is_in_range()
        {
            Sut.IsValid(200).ShouldBeTrue();
        }

        [Observation]
        public void Should_201_is_out_of_range()
        {
            Sut.IsValid(201).ShouldBeFalse();
        }

        [Observation]
        public void Should_250_is_out_of_range()
        {
            Sut.IsValid(250).ShouldBeFalse();
        }

        [Observation]
        public void Should_50_is_out_of_range()
        {
            Sut.IsValid(50).ShouldBeFalse();
        }

        [Observation]
        public void Should_99_is_out_of_range()
        {
            Sut.IsValid(99).ShouldBeFalse();
        }
    }

    public class When_The_Range_Is_Between_100_0_and_200_0 : ConcernOfRangeRule
    {
        protected override RangeRule CreateSut()
        {
            return new RangeRule(100.0, 200.0);
        }

        [Observation]
        public void Should_150_0_is_in_range()
        {
            Sut.IsValid(150.0).ShouldBeTrue();
        }

        [Observation]
        public void Should_100_0_is_in_range()
        {
            Sut.IsValid(100.0).ShouldBeTrue();
        }

        [Observation]
        public void Should_200_0_is_in_range()
        {
            Sut.IsValid(200.0).ShouldBeTrue();
        }

        [Observation]
        public void Should_250_0_is_out_of_range()
        {
            Sut.IsValid(250.0).ShouldBeFalse();
        }

        [Observation]
        public void Should_50_0_is_out_of_range()
        {
            Sut.IsValid(50.0).ShouldBeFalse();
        }

        [Observation]
        public void Should_99_9_is_out_of_range()
        {
            Sut.IsValid(99.9).ShouldBeFalse();
        }

        [Observation]
        public void Should_200_1_is_out_of_range()
        {
            Sut.IsValid(200.1).ShouldBeFalse();
        }
    }

    public class When_The_Range_Is_Between_1_1_2010_and_5_5_2010 : ConcernOfRangeRule
    {
        protected override RangeRule CreateSut()
        {
            return new RangeRule(new DateTime(2010, 1, 1), new DateTime(2010, 5, 5));
        }

        [Observation]
        public void Should_2_2_2010_is_in_range()
        {
            Sut.IsValid(new DateTime(2010, 2, 2)).ShouldBeTrue();
        }

        [Observation]
        public void Should_1_1_2010_is_in_range()
        {
            Sut.IsValid(new DateTime(2010, 1, 1)).ShouldBeTrue();
        }

        [Observation]
        public void Should_5_5_2010_is_in_range()
        {
            Sut.IsValid(new DateTime(2010, 5, 5)).ShouldBeTrue();
        }

        [Observation]
        public void Should_6_6_2011_0_is_out_of_range()
        {
            Sut.IsValid(new DateTime(2011, 6, 6)).ShouldBeFalse();
        }

        [Observation]
        public void Should_1_1_2009_is_out_of_range()
        {
            Sut.IsValid(new DateTime(2009, 1, 1)).ShouldBeFalse();
        }

        [Observation]
        public void Should_31_12_2009_is_out_of_range()
        {
            Sut.IsValid(new DateTime(2009, 12, 31)).ShouldBeFalse();
        }

        [Observation]
        public void Should_6_5_2010_is_out_of_range()
        {
            Sut.IsValid(new DateTime(2010, 5, 6)).ShouldBeFalse();
        }
    }
}