using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace FluentMetadata.MVC.Specs
{
    public abstract class ConcernOfComplexModel : ConcernOfComparingMetadata
    {
        protected ComplexModel model;

        protected override void EstablishContext() => model = new ComplexModel()
        {
            FirstName = "Albert",
            LastName = "Weinert",
            Age = 39,
            Sex = 'm',
            Amount = 815.4711m
        };

        protected void CreatePropertyMetadata(string propertyName)
        {
            Fluent = Sut.GetMetadataForProperty(() => model, model.GetType(), propertyName);
            Expected = OriginalProvider.GetMetadataForProperty(() => model, model.GetType(), propertyName);
        }
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_the_Type_ComplexModel : ConcernOfComplexModel
    {
        private ModelValidator[] validators;

        public override void CreateMetadata()
        {
            model.FirstName = "Robert'); DROP ";
            model.LastName = "TABLE Students; --";
            Fluent = Sut.GetMetadataForType(() => model, model.GetType());
            Expected = OriginalProvider.GetMetadataForType(() => model, model.GetType());

            validators = new FluentValidationProvider()
                .GetValidators(Fluent, new ControllerContext())
                .ToArray();
        }

        [Observation]
        public void A_validator_is_returned_for_the_generic_rule() => Assert.Equal(1, validators.Length);

        [Observation]
        public void The_validator_is_of_type_ClassRuleModelValidator() => Assert.IsType<ClassRuleModelValidator>(validators[0]);

        [Observation]
        public void The_validator_returns_1_ModelValidationResult() => Assert.Equal(1, validators[0].Validate(model).Count());

        [Observation]
        public void The_error_message_of_the_ModelValidationResult_equals_the_message_specified_in_the_rule() => Assert.Equal(
            "'LastName' and 'Vorname' do not match.",
            validators[0].Validate(model).ToArray()[0].Message);

        [Observation]
        public void Getting_metadata_for_all_properties_does_not_throw_an_exception()
        {
            Exception error = null;
            IEnumerable<ModelMetadata> properties = null;

            try { properties = Sut.GetMetadataForProperties(model, model.GetType()); }
            catch (Exception ex) { error = ex; }

            Assert.Null(error);
            Assert.Equal(OriginalProvider.GetMetadataForProperties(model, model.GetType()).Count(), properties.Count());
        }
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Id : ConcernOfComplexModel
    {
        public override void CreateMetadata() => CreatePropertyMetadata("Id");
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_FirstName : ConcernOfComplexModel
    {
        public override void CreateMetadata() => CreatePropertyMetadata("FirstName");
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_LastName : ConcernOfComplexModel
    {
        public override void CreateMetadata() => CreatePropertyMetadata("LastName");
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Sex : ConcernOfComplexModel
    {
        private ModelValidator[] validators;

        public override void CreateMetadata()
        {
            CreatePropertyMetadata("Sex");

            validators = new FluentValidationProvider()
                .GetValidators(Fluent, new ControllerContext())
                .ToArray();
        }

        [Observation]
        public void A_validator_is_returned_for_the_generic_rule() => Assert.Equal(1, validators.Length);

        [Observation]
        public void The_validator_is_of_type_RuleModelValidator() => Assert.IsType<RuleModelValidator>(validators[0]);

        [Observation]
        public void The_validator_returns_1_ModelValidationResult() => Assert.Equal(1, validators[0].Validate(model).Count());

        [Observation]
        public void The_error_message_of_the_ModelValidationResult_says_value_cannot_be_male() => Assert.Equal(
            "'Sex' cannot be male since this is a ComplexModel.",
            validators[0].Validate(model).ToArray()[0].Message);
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Amount : ConcernOfComplexModel
    {
        public override void CreateMetadata() => CreatePropertyMetadata("Amount");
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Age : ConcernOfComplexModel
    {
        public override void CreateMetadata() => CreatePropertyMetadata("Age");
    }
}