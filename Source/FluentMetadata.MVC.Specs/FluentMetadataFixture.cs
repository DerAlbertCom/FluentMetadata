using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    public class FluentMetadataFixture
    {
        public FluentMetadataFixture()
        {
            FluentMetadataBuilder.Reset();
            FluentMetadataBuilder.ForAssemblyOfType<FluentMetadataFixture>();
            ModelMetadataProviders.Current = new FluentMetadataProvider();
        }
    }
}