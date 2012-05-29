using System;

namespace FluentMetadata.Specs.SampleClasses
{
    public abstract class DomainObject
    {
        protected DomainObject()
        {

        }
        public virtual void Initialize()
        {
            Created = DateTime.UtcNow;
            Updated = Created;
        }
        public int Id { get; private set; }

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }


        protected void Modified()
        {
            Updated = DateTime.UtcNow;
        }

        private bool Equals(DomainObject other)
        {
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (!(obj is DomainObject))
            {
                return false;
            }
            return Equals((DomainObject)obj);
        }

        public override int GetHashCode()
        {
            return string.Format("{0}({1})", GetType().Name, Id).GetHashCode();
        }

        public static bool operator ==(DomainObject left, DomainObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DomainObject left, DomainObject right)
        {
            return !Equals(left, right);
        }
    }
}