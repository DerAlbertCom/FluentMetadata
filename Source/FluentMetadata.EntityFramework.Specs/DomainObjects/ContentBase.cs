namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public abstract class ContentBase : Content
    {
        public override void Initialize()
        {
            base.Initialize();
            ContentType = "wiki";
        }

        public Layout Layout { get; private set; }
        public string Content { get; private set; }
        public string ContentType { get; private set; }

        public bool IsWiki
        {
            get { return ContentType == "wiki"; }
        }
    }
}