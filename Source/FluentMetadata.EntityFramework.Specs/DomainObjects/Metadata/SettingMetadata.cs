namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class SettingMetadata : DomainObjectMetadata<Setting>
    {
        public SettingMetadata()
        {
            Property(p => p.Name).Length(MetadataInfo.NameLength).Is.Required();
            Property(p => p.Value).Length(1024).Is.Not.Required();
            Property(p => p.WebSite).Is.Required();
        }
    }
}