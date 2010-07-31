using System.Collections;
using System.Collections.Generic;

namespace FluentMetadata
{
    public class PropertiesMetadata : IEnumerable<Metadata>
    {
        private readonly Dictionary<string, Metadata> metadatas = new Dictionary<string, Metadata>();

        internal void Add(Metadata metadata)
        {
            metadatas[metadata.ModelName] = metadata;
        }

        internal bool Contains(string propertyName)
        {
            return metadatas.ContainsKey(propertyName);
        }

        public IEnumerator<Metadata> GetEnumerator()
        {
            return metadatas.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int Count
        {
            get { return metadatas.Count; }
        }

        public Metadata this[string propertyName]
        {
            get { return metadatas[propertyName]; }
        }
    }
}