namespace FluentMetadata.MVC.Specs
{
    public class SampleModelMetaData : ClassMetadata<SampleModel>
    {
        public SampleModelMetaData()
        {
            Property(s => s.VornameRequired).Is.Required();
        }
    }
}