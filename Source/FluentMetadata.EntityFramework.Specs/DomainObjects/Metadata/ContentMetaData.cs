namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public abstract class ContentMetadata<T> : DomainObjectMetadata<T> where T : Content
    {
        protected ContentMetadata()
        {
            Property(c => c.Title).Is.Required().Length(MetadataInfo.TitleLength).Display.Name("Überschrift");
            Property(c => c.Abstract).Length(MetadataInfo.MaxLength).As.MultilineText().Display.Name("Zusammenfassung");
            Property(c => c.Author).Is.Required();
            Property(c => c.DisplayStart).Is.Required().Display.Name("Anzeigen ab");
            Property(c => c.DisplayEnd).Is.Required().Display.Name("Anzeigen bis");
        }
    }
}