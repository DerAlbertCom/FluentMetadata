using System;
using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    public class FluentMetadataFixture
    {
        internal Exception Exception { get; private set; }

        public FluentMetadataFixture()
        {
            FluentMetadataBuilder.Reset();

            try { FluentMetadataBuilder.ForAssemblyOfType<FluentMetadataFixture>(); }
            catch (Exception ex) { Exception = ex; }

            ModelMetadataProviders.Current = new FluentMetadataProvider();
        }
    }
}