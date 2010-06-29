using System;
using Xunit;

namespace FluentMetadata.MVC.Specs
{
    [Concern(typeof(SampleModel),"Validation of SampleData")]
    public abstract class ConcernOfValidationSampleData : InstanceContextSpecification<DummyController>
        , IUseFixture<FluentMetadataFixture>
    {
        protected readonly SampleModel Model = new SampleModel();

        protected override DummyController CreateSut()
        {
            return new DummyController();
        }

        public void SetFixture(FluentMetadataFixture data)
        {
        }
    }

    public class When_vorname_is_required_and_not_set : ConcernOfValidationSampleData
    {
        protected override void Because()
        {
            Sut.ValidateModel(Model);
        }

        [Observation]
        public void Should_one_error_vorname()
        {
            Sut.ModelState["VornameRequired"].Errors.Count.ShouldBeEqualTo(1);
        }
    }

    public class When_vorname_is_required_and_is_set: ConcernOfValidationSampleData
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Model.VornameRequired = "Albert";
        }
        protected override void Because()
        {
            Sut.ValidateModel(Model);
        }

        [Observation]
        public void Should_no_error_on_vorname()
        {
            Sut.ModelState["VornameRequired"].ShouldBeNull();
        }
    }
}