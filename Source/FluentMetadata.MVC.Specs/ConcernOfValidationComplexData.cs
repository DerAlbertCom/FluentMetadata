using Xunit;

namespace FluentMetadata.MVC.Specs
{
    [Concern(typeof(ComplexModel), "Validation of ComplexData")]
    public abstract class ConcernOfValidationComplexData : InstanceContextSpecification<DummyController>, IUseFixture<FluentMetadataFixture>
    {
        protected readonly ComplexModel Model = new ComplexModel();

        protected override DummyController CreateSut() => new DummyController();

        public void SetFixture(FluentMetadataFixture data) { }
    }

    public class When_FirstName_is_required_and_not_set : ConcernOfValidationComplexData
    {
        protected override void Because() => Sut.ValidateModel(Model);

        [Observation]
        public void Should_one_error_vorname() => Sut.ModelState["FirstName"].Errors.Count.ShouldBeEqualTo(1);
    }

    public class When_FirstName_is_required_and_is_set : ConcernOfValidationComplexData
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Model.FirstName = "Albert";
        }

        protected override void Because() => Sut.ValidateModel(Model);

        [Observation]
        public void Should_no_error_on_vorname() => Sut.ModelState["FirstName"].ShouldBeNull();
    }
}