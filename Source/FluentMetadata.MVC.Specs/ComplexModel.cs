using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FluentMetadata.MVC.Specs
{
    public class ComplexModelMetadata : ClassMetadata<ComplexModel>
    {
        public ComplexModelMetadata()
        {
            Class.Display.Name("Komplex");
            Property(e => e.FirstName).Display.Name("Vorname").Is.Not.ConvertEmptyStringToNull();
        }
    }
    [DisplayName("Komplex")]
    public class ComplexModel
    {
        [DisplayName("Vorname")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal Amount { get; set; }
        public char Sex { get; set; }
    }
}