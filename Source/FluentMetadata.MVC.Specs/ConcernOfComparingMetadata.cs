using System;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace FluentMetadata.MVC.Specs
{
    public abstract class ConcernOfComparingMetadata : InstanceContextSpecification<FluentMetadataProvider>,
                                                       IUseFixture<FluentMetadataFixture>
    {
        protected readonly ModelMetadataProvider OriginalProvider = new DataAnnotationsModelMetadataProvider();

        protected ModelMetadata Fluent;
        protected ModelMetadata Expected;
        private Exception exception;

        protected override void Because()
        {
            CreateMetadata();
        }

        public void SetFixture(FluentMetadataFixture data)
        {
            exception = data.Exception;
        }

        public abstract void CreateMetadata();

        [Observation]
        public void MetadataSetupDoesNotThrowAnException()
        {
            Assert.Null(exception);
        }

        [Observation]
        public void Equals_ModelMetadata_Properties_Count()
        {
            Console.WriteLine(Expected.Properties.Count());
            Assert.Equal(Expected.Properties.Count(), Fluent.Properties.Count());
        }

        [Observation]
        public void Equals_PropertyName()
        {
            Console.WriteLine(Expected.PropertyName);
            Assert.Equal(Expected.PropertyName, Fluent.PropertyName);
        }

        [Observation]
        public void Equals_DisplayName()
        {
            Console.WriteLine(Expected.DisplayName);
            Assert.Equal(Expected.DisplayName, Fluent.DisplayName);
        }

        [Observation]
        public void Equals_Model()
        {
            Console.WriteLine(Expected.Model);
            Assert.Equal(Expected.Model, Fluent.Model);
        }

        [Observation]
        public void Equals_ContainerType()
        {
            Console.WriteLine(Expected.ContainerType);
            Assert.Equal(Expected.ContainerType, Fluent.ContainerType);
        }

        [Observation]
        public void Equals_ConvertEmptyStringToNull()
        {
            Console.WriteLine(Expected.ConvertEmptyStringToNull);
            Assert.Equal(Expected.ConvertEmptyStringToNull, Fluent.ConvertEmptyStringToNull);
        }

        [Observation]
        public void Equals_DataTypeName()
        {
            Console.WriteLine(Expected.DataTypeName);
            Assert.Equal(Expected.DataTypeName, Fluent.DataTypeName);
        }

        [Observation]
        public void Equals_DispalyFormatString()
        {
            Console.WriteLine(Expected.DisplayFormatString);
            Assert.Equal(Expected.DisplayFormatString, Fluent.DisplayFormatString);
        }

        [Observation]
        public void Equals_Description()
        {
            Console.WriteLine(Expected.Description);
            Assert.Equal(Expected.Description, Fluent.Description);
        }

        [Observation]
        public void Equals_EditFormatString()
        {
            Console.WriteLine(Expected.EditFormatString);
            Assert.Equal(Expected.EditFormatString, Fluent.EditFormatString);
        }

        [Observation]
        public void Equals_HideSurroundingHtml()
        {
            Console.WriteLine(Expected.HideSurroundingHtml);
            Assert.Equal(Expected.HideSurroundingHtml, Fluent.HideSurroundingHtml);
        }

        [Observation]
        public void Equals_IsReadOnly()
        {
            Console.WriteLine(Expected.IsReadOnly);
            Assert.Equal(Expected.IsReadOnly, Fluent.IsReadOnly);
        }

        [Observation]
        public void Equals_IsRequired()
        {
            Console.WriteLine(Expected.IsRequired);
            Assert.Equal(Expected.IsRequired, Fluent.IsRequired);
        }

        [Observation]
        public void Equals_ModelType()
        {
            Console.WriteLine(Expected.ModelType);
            Assert.Equal(Expected.ModelType, Fluent.ModelType);
        }

        [Observation]
        public void Equals_NullDisplayText()
        {
            Console.WriteLine(Expected.NullDisplayText);
            Assert.Equal(Expected.NullDisplayText, Fluent.NullDisplayText);
        }

        [Observation]
        public void Equals_RequestValidationEnabled()
        {
            Console.WriteLine(Expected.RequestValidationEnabled);
            Assert.Equal(Expected.RequestValidationEnabled, Fluent.RequestValidationEnabled);
        }

        [Observation]
        public void Equals_ShowForDisplay()
        {
            Console.WriteLine(Expected.ShowForDisplay);
            Assert.Equal(Expected.ShowForDisplay, Fluent.ShowForDisplay);
        }

        [Observation]
        public void Equals_ShowForEdit()
        {
            Console.WriteLine(Expected.ShowForEdit);
            Assert.Equal(Expected.ShowForEdit, Fluent.ShowForEdit);
        }

        [Observation]
        public void Equals_TemplateHint()
        {
            Console.WriteLine(Expected.TemplateHint);
            Assert.Equal(Expected.TemplateHint, Fluent.TemplateHint);
        }

        [Observation]
        public void Equals_Watermark()
        {
            Console.WriteLine(Expected.Watermark);
            Assert.Equal(Expected.Watermark, Fluent.Watermark);
        }

        [Observation]
        public void DataAnnotationsModelValidatorProviderAppliesRequiredValidators()
        {
            var controllerContext = new ControllerContext();
            var dataAnnotationsModelValidatorProvider = new DataAnnotationsModelValidatorProvider();

            var expectedValidatorCount = dataAnnotationsModelValidatorProvider
                .GetValidators(Expected, controllerContext)
                .Count(v => v.IsRequired);

            if (Expected.IsRequired)
            {
                Assert.Equal(1, expectedValidatorCount);
            }
            else
            {
                Assert.InRange(expectedValidatorCount, 0, 1);
            }

            Assert.Equal(
                expectedValidatorCount,
                dataAnnotationsModelValidatorProvider
                    .GetValidators(Fluent, controllerContext)
                    .Count(v => v.IsRequired));
        }

        [Observation]
        public void StringLengthValidatorsMatch()
        {
            var controllerContext = new ControllerContext();

            var expectedValidatorCount = new DataAnnotationsModelValidatorProvider()
                .GetValidators(Expected, controllerContext)
                .OfType<StringLengthAttributeAdapter>()
                .Count();

            Assert.InRange(expectedValidatorCount, 0, 1);

            Assert.Equal(
                expectedValidatorCount,
                new FluentValidationProvider()
                    .GetValidators(Fluent, controllerContext)
                    .OfType<RuleModelValidator>()
                    .SelectMany(rmv => rmv.GetClientValidationRules())
                    .OfType<ModelClientValidationStringLengthRule>()
                    .Count());
        }

        [Observation]
        public void RangeValidatorsMatch()
        {
            var controllerContext = new ControllerContext();
            var expectedValidatorCount = new DataAnnotationsModelValidatorProvider()
                .GetValidators(Expected, controllerContext)
                .OfType<RangeAttributeAdapter>()
                .Count();
            Assert.InRange(expectedValidatorCount, 0, 1);
            Assert.Equal(
                expectedValidatorCount,
                new FluentValidationProvider()
                    .GetValidators(Fluent, controllerContext)
                    .OfType<RuleModelValidator>()
                    .SelectMany(rmv => rmv.GetClientValidationRules())
                    .OfType<ModelClientValidationRangeRule>()
                    .Count());
        }

        [Observation]
        public void RegularExpressionValidatorsMatch()
        {
            var controllerContext = new ControllerContext();
            var expectedValidatorCount = new DataAnnotationsModelValidatorProvider()
                .GetValidators(Expected, controllerContext)
                .OfType<RegularExpressionAttributeAdapter>()
                .Count();
            Assert.InRange(expectedValidatorCount, 0, 1);
            Assert.Equal(
                expectedValidatorCount,
                new FluentValidationProvider()
                    .GetValidators(Fluent, controllerContext)
                    .OfType<RuleModelValidator>()
                    .SelectMany(rmv => rmv.GetClientValidationRules())
                    .OfType<ModelClientValidationRegexRule>()
                    .Count());
        }
    }
}