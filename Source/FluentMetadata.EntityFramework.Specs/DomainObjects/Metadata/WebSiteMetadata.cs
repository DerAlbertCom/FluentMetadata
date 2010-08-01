namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class WebSiteMetadata : DomainObjectMetadata<WebSite>
    {
        public WebSiteMetadata()
        {
            Property(p => p.Name).Length(MetadataInfo.NameLength).Is.Required();
            Property(p => p.Hostname).Length(MetadataInfo.NameLength).Is.Required();
        }
    }
}