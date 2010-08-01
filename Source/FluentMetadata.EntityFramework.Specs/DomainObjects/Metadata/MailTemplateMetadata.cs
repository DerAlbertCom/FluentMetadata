namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class MailTemplateMetadata : DomainObjectMetadata<MailTemplate>
    {
        public MailTemplateMetadata()
        {
            Property(x => x.Name).Is.Required().Length(MetadataInfo.NameLength)
                .Display.Name("Name");
            Property(x => x.Subject).Is.Required().Length(MetadataInfo.TitleLength)
                .Display.Name("Betreff");
            Property(x => x.Body).Length(MetadataInfo.MaxLength).Is.Required()
                .Display.Name("Text");
            Property(x => x.WebSite).Is.Required();
        }
    }
}