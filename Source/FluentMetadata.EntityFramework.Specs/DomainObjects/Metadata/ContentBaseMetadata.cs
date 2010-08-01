namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public abstract class ContentBaseMetadata<T> : ContentMetadata<T> where T : ContentBase
    {
        protected ContentBaseMetadata()
        {
            Property(e => e.Content).Length(MetadataInfo.MaxLength).Display.Name("Text").Is.Required();
            Property(e => e.ContentType).Length(32).Display.Name("ContentTyp").Is.Required();
        }
    }
}