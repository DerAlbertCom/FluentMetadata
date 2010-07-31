namespace FluentMetadata.Specs.SampleClasses.MetaData
{
    public class PersonMetadata : ClassMetadata<Person>
    {
        public PersonMetadata()
        {
            Property(p => p.FirstName).Is.Required();
            Class.Display.Name("Benutzer");
            Class.Display.Format("{0} der Benutzer");
        }
    }
}