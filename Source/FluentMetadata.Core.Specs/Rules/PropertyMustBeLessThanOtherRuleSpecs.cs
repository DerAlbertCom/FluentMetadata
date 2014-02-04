using System;
using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Rules
{
    public class If_a_date_should_be_before_another : InstanceContextSpecification<PropertyMustBeLessThanOtherRule<Reminder>>
    {
        protected override PropertyMustBeLessThanOtherRule<Reminder> CreateSut()
        {
            return new PropertyMustBeLessThanOtherRule<Reminder>(x => x.AlertDate, x => x.EventDate);
        }

        protected override void Because()
        {
        }

        [Observation]
        public void It_is_valid_if_it_is_earlier_than_the_other()
        {
            var model = new Reminder { EventDate = DateTime.Now.AddDays(1) };
            model.AlertDate = model.EventDate.AddHours(-2);
            Sut.IsValid(model).ShouldBeTrue();
        }

        [Observation]
        public void It_is_invalid_if_it_is_equal_to_the_other()
        {
            var model = new Reminder { EventDate = DateTime.Now.AddDays(1) };
            model.AlertDate = model.EventDate;
            Sut.IsValid(model).ShouldBeFalse();
        }

        [Observation]
        public void It_is_invalid_if_it_is_later_than_the_other()
        {
            var model = new Reminder { EventDate = DateTime.Now.AddDays(1) };
            model.AlertDate = model.EventDate.AddHours(2);
            Sut.IsValid(model).ShouldBeFalse();
        }
    }

    public class If_an_int_property_should_be_less_than_another : InstanceContextSpecification<PropertyMustBeLessThanOtherRule<Reminder>>
    {
        protected override PropertyMustBeLessThanOtherRule<Reminder> CreateSut()
        {
            return new PropertyMustBeLessThanOtherRule<Reminder>(x => x.AlertDayOfWeek, x => x.EventDayOfWeek);
        }

        protected override void Because()
        {
        }

        [Observation]
        public void It_is_valid_if_it_is_less_than_the_other()
        {
            var model = new Reminder { EventDayOfWeek = 5, AlertDayOfWeek = 3 };
            Sut.IsValid(model).ShouldBeTrue();
        }
    }

    public class Reminder
    {
        public DateTime AlertDate { get; set; }
        public DateTime EventDate { get; set; }
        public int AlertDayOfWeek { get; set; }
        public int EventDayOfWeek { get; set; }
    }
}
