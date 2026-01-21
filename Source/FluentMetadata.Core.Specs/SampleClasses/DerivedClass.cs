namespace FluentMetadata.Specs.SampleClasses
{
    public abstract class BaseClass
    {
        public int Id { get; set; }
    }

    public class DerivedClass : BaseClass
    {
        public string Title { get; set; }
    }

    public class DerivedDerivedClass : DerivedClass
    {
        public string Text { get; set; }
    }

    public class BaseClassMetadata<T> : ClassMetadata<T> where T : BaseClass
    {
        protected BaseClassMetadata()
        {
            Property(e => e.Id).Is.Required();
        }
    }

    public class DerivedClassMetadata<T> : BaseClassMetadata<T> where T : DerivedClass
    {
        protected DerivedClassMetadata()
        {
            Property(e => e.Title).Is.Required();
        }
    }

    public class DerviceDerviceClassMetadata : DerivedClassMetadata<DerivedDerivedClass>
    {
        public DerviceDerviceClassMetadata()
        {
            Property(e => e.Text).Is.ReadOnly();
        }
    }
}