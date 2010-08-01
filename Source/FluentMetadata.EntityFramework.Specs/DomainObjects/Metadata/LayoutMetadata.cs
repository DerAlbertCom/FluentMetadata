namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class LayoutMetadata : ContentMetadata<Layout>
    {
        public LayoutMetadata()
        {
            Property(e => e.Format).Length(MetadataInfo.MaxLength).Display.Name("Format");
        }
    }
}