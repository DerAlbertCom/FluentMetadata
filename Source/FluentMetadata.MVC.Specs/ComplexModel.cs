using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    public class ComplexModelMetadata : ClassMetadata<ComplexModel>
    {
        public ComplexModelMetadata()
        {
            CopyMetadataFrom<ComplexDomainModel>();

            Class
                .Display.Name("Komplex")
                .AssertThat(
                    cm => !"Robert'); DROP TABLE Students; --".Equals( //http://xkcd.com/327/ :)
                        cm.FirstName + cm.LastName,
                        StringComparison.InvariantCultureIgnoreCase),
                    "Gotcha, little Bobby Tables! You'll never be '{0}'!");
            Property(e => e.Id)
                .Should.HiddenInput()
                .Is.ReadOnly()
                .Should.Not.ShowInDisplay()
                .Should.Not.ShowInEditor();
            Property(e => e.FirstName)
                .Display.Name("Vorname")
                .Description("Der Vorname des komplexen Models")
                .Editor.Watermark("hier Vornamen eintragen")
                .Is.Not.ConvertEmptyStringToNull()
                .Is.Required()
                .Is.RequestValidationEnabled();
            Property(e => e.LastName)
                .Display.NullText("No lastname set")
                .Should.MatchRegex("^[A-Z]'?[- a-zA-Z]+$")
                .Is.Not.RequestValidationEnabled()
                .Length(50);
            Property(e => e.Age)
                .As.Custom("Years")
                .UIHint("Spinner")
                .Range(0, 123);
            Property(e => e.Sex)
                .AssertThat(
                    sex => sex != 'm',
                    "'{0}' cannot be male since this is a ComplexModel.");
        }
    }

    [DisplayName("Komplex")]
    public class ComplexModelBase : ComplexDomainModel
    {
        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Vorname")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Description = "Der Vorname des komplexen Models", Prompt = "hier Vornamen eintragen")]
        [Required]
        public string FirstName { get; set; }

        [DisplayFormat(NullDisplayText = "No lastname set")]
        [RegularExpression("^[A-Z]'?[- a-zA-Z]+$")]
        [AllowHtml]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataType("Years")]
        [UIHint("Spinner")]
        [Range(0, 123)]
        public int Age { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public new decimal Amount { get; set; }

        public char Sex { get; set; }
        internal bool IsComplex => Sex != 'm';
    }

    public class ComplexModel : ComplexModelBase { }

    public class ComplexDomainModel
    {
        public double Amount { get; set; }

        private class Metadata : ClassMetadata<ComplexDomainModel>
        {
            public Metadata() =>
                Property(e => e.Amount)
                    .As.Custom(DataType.Currency)
                    .Display.Format(() => "{0:c}")
                    .Editor.Format(() => "{0:c}");
        }
    }
}