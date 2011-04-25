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
            Property(e => e.Age).As.Custom("Years");
            Property(e => e.Amount)
                .Display.Format("{0:c}")
                .Editor.Format("{0:c}");
        }
    }
    [DisplayName("Komplex")]
    public class ComplexModel
    {
        [DisplayName("Vorname")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType("Years")]
        public int Age { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        public char Sex { get; set; }
    }
}