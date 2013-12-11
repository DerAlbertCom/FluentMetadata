namespace FluentMetadata.Specs.SampleClasses.MetaData
{
    public class PersonMetadata : ClassMetadata<Person>
    {
        public PersonMetadata()
        {
            Property(p => p.FirstName)
                .Is.Required();
            Class
                .Display.Name("Benutzer")
                .Display.Format(() => "{0} der Benutzer")
                .Property(p => p.FirstName).ShouldEqual(p => p.LastName);
        }
    }
}