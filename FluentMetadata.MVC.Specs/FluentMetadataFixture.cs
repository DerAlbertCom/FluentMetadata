using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    public class FluentMetadataFixture
    {
        private static readonly ModelMetadataProvider Metadata = new FluentMetadataProvider();

        public FluentMetadataFixture()
        {
            FluentMetadataBuilder.Reset();
            FluentMetadataBuilder.ForAssemblyOfType<FluentMetadataFixture>();
 
            ModelMetadataProviders.Current = Metadata;
        }
    }
}