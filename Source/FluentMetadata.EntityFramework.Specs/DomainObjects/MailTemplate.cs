namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class MailTemplate : DomainObject, IWebSiteEntity
    {
        private MailTemplate() {
        }

        public virtual WebSite WebSite { get; private set; }
        public string Name { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public void SetTemplate(string subject, string body) {
            Subject = subject;
            Body = body;
            Modified();
        }

        public void ChangeSite(WebSite site) {
            WebSite = site;
        }

    }
}