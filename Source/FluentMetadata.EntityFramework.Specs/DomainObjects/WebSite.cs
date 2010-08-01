namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class WebSite : DomainObject
    {
        private WebSite()
        {
        }

        public string Hostname { get; private set; }

        public string Name { get; private set; }

        public void SetName(string name)
        {
            Name = name;
            Modified();
        }

        public void SetHostname(string hostname)
        {
            Hostname = hostname;
            Modified();
        }

        public bool MatchUrl(string host)
        {
            return host.Contains(Hostname);
        }
    }
}