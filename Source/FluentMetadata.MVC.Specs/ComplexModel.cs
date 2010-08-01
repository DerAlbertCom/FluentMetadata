using System.ComponentModel;

namespace FluentMetadata.MVC.Specs
{
    public class ComplexModelMetadata:ClassMetadata<ComplexModel>
    {
        public ComplexModelMetadata()
        {
            Class.Display.Name("Komplex");
            Property(e => e.FirstName).Display.Name("Vorname");
        }
    }
    [DisplayName("Komplex")]
    public class ComplexModel
    {
        [DisplayName("Vorname")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal Amount { get; set; }
        public char Sex { get; set; }
    }
}