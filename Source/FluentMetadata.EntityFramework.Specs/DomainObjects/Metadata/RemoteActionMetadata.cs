namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class RemoteActionMetadata : ClassMetadata<RemoteAction>
    {
        public RemoteActionMetadata()
        {
            Property(x => x.ObjectId).Is.Required();
            Property(x => x.Action).Is.Required().Length(MetadataInfo.TitleLength);
            Property(x => x.ConfirmationKey).Is.Required();
            Property(x => x.Expiration).Is.Required();
        }
    }
}