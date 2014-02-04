using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Rules
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class When_two_properties_should_be_equal : InstanceContextSpecification<PropertyMustMatchRule<ChangePasswordModel>>
    {
        protected override PropertyMustMatchRule<ChangePasswordModel> CreateSut()
        {
            return new PropertyMustMatchRule<ChangePasswordModel>(x => x.NewPassword, x => x.ConfirmPassword);
        }

        protected override void Because()
        {
        }

        [Observation]
        public void Should_be_valid_if_properties_match()
        {
            var model = new ChangePasswordModel { NewPassword = "asdf", ConfirmPassword = "asdf" };
            Sut.IsValid(model).ShouldBeTrue();
        }

        [Observation]
        public void Should_be_invalid_if_properties_do_not_match()
        {
            var model = new ChangePasswordModel { NewPassword = "qwer", ConfirmPassword = "asdf" };
            Sut.IsValid(model).ShouldBeFalse();
        }
    }
}