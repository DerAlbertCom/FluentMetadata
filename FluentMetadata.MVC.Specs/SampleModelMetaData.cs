namespace FluentMetadata.MVC.Specs
{
    public class SampleModelMetaData : ClassMetadata<SampleModel>
    {
        public SampleModelMetaData()
        {
            The(s => s.VornameRequired).Is.Required();
        }
    }
}