using System;
using System.Globalization;

namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class Setting : DomainObject, IWebSiteEntity
    {
        private Setting()
        {
        }

        public virtual WebSite WebSite { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        public void Initialize(string name, WebSite webSite)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (webSite == null)
            {
                throw new ArgumentNullException("webSite");
            }
            Name = name;
            WebSite = webSite;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}