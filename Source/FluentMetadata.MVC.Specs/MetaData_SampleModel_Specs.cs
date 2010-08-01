using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FluentMetadata.MVC.Specs
{
    [Concern(typeof (FluentMetadataProvider))]
    public class When_getting_the_first_Property_Metadata_of_SampleData : ConcernOfComparingMetadata
    {
        private SampleModel model;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            model = new SampleModel {VornameRequired = "Albert"};
        }
        public override void CreateMetadata()
        {Fluent = Sut.GetMetadataForProperties(model, model.GetType()).Single();
            
            Expected = OriginalProvider.GetMetadataForProperties(model, model.GetType()).Single();
        }
    }

    [Concern(typeof(FluentMetadataProvider))]
    public class When_getting_the_Metadata_of_the_Type_SampleData : ConcernOfComparingMetadata
    {
        private SampleModel model;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            model = new SampleModel { VornameRequired = "Albert" };
        }
        public override void CreateMetadata()
        {
            Fluent = Sut.GetMetadataForType(()=>model, model.GetType());
            Expected = Sut.GetMetadataForType(() => model, model.GetType());
        }
    }


}