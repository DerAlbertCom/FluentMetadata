namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class Tag : DomainObject, IWebSiteEntity
    {
        private Tag()
        {
        }

        public WebSite WebSite { get; private set; }
        public string TagName { get; private set; }

        public void SetName(string name)
        {
            TagName = name;
        }

        public void SetWebSite(WebSite webSite)
        {
            WebSite = webSite;
        }
    }
}