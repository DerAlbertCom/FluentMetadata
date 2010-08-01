namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class TagMetadata : ClassMetadata<Tag>
    {
        public TagMetadata()
        {
            Property(p => p.TagName).Length(MetadataInfo.NameLength).Is.Required().Display.Name("Tag");
        }
    }
}