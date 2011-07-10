using FluentMetadata.Rules;
using Xunit;

namespace FluentMetadata.Specs.Rules
{
    [Concern(typeof(PropertyMustMatchRegexRule))]
    public class When_property_value_should_match_a_regex : InstanceContextSpecification<PropertyMustMatchRegexRule>
    {
        protected override void Because()
        {
        }

        protected override PropertyMustMatchRegexRule CreateSut()
        {
            //from http://regexlib.com/REDetails.aspx?regexp_id=96
            const string validUri = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return new PropertyMustMatchRegexRule(validUri);
        }
        
        [Observation]
        public void A_null_value_is_valid() // because to check this is the responsibility of the RequiredRule
        {
            Sut.IsValid(null).ShouldBeTrue();
        }
    }
}