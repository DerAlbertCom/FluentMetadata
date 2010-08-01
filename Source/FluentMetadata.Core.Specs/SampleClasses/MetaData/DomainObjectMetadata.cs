namespace FluentMetadata.Specs.SampleClasses.MetaData
{
    public class DomainObjectMetadata<T> : ClassMetadata<T> where T : DomainObject
    {
        protected DomainObjectMetadata()
        {
            Property(x => x.Id).Should.HiddenInput().Should.Not.ShowInEditor().Should.Not.ShowInDisplay().Is.ReadOnly();
            Property(x => x.Created).Is.Required().Should.Not.ShowInEditor().Display.Name("Angelegt").Is.ReadOnly();
            Property(x => x.Updated).Is.Required().Should.Not.ShowInEditor().Display.Name("Bearbeitet").Is.ReadOnly();
        }
    }
}