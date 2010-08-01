namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class CommentMetadata : ContentMetadata<Comment>
    {
        public CommentMetadata()
        {
            Property(c => c.EMail).Length(MetadataInfo.EMailLength).Is.Not.Required().As.EmailAddress();
            Property(c => c.Text).Length(MetadataInfo.MaxLength).Is.Required().As.MultilineText();
        }
    }
}