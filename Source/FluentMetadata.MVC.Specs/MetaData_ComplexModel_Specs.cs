using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FluentMetadata.MVC.Specs
{
    public abstract class ConcernOfComplexModel : ConcernOfComparingMetadata
    {
        protected ComplexModel model;

        protected override void EstablishContext()
        {
            model = new ComplexModel()
                        {
                            FirstName = "Albert",
                            LastName = "Weinert",
                            Age = 39,
                            Sex = 'm',
                            Amount = 815.4711m
                        };
        }

        protected void CreatePropertyMetadata(string propertyName)
        {
            Fluent = Sut.GetMetadataForProperty(() => model, model.GetType(), propertyName);
            Expected = OriginalProvider.GetMetadataForProperty(() => model, model.GetType(), propertyName);
        }
    }

    [Concern(typeof (FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_the_Type_ComplexModel : ConcernOfComplexModel
    {
        public override void CreateMetadata()
        {
            Fluent = Sut.GetMetadataForType(() => model, model.GetType());
            Expected = OriginalProvider.GetMetadataForType(() => model, model.GetType());
        }
    }

    [Concern(typeof (FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_FirstName : ConcernOfComplexModel
    {
        public override void CreateMetadata()
        {
            CreatePropertyMetadata("FirstName");
        }
    }

    [Concern(typeof (FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_LastName : ConcernOfComplexModel
    {
        public override void CreateMetadata()
        {
            CreatePropertyMetadata("LastName");
        }
    }

    [Concern(typeof (FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Sex : ConcernOfComplexModel
    {
        public override void CreateMetadata()
        {
            CreatePropertyMetadata("Sex");
        }
    }

    [Concern(typeof (FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_ComplexModel_Property_Amount : ConcernOfComplexModel
    {
        public override void CreateMetadata()
        {
            CreatePropertyMetadata("Amount");
        }
    }
}