using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    public class FluentModelMetadata : ModelMetadata
    {
        private readonly MetaData metaData;
        

        public FluentModelMetadata(MetaData metaData , ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName) : base(provider, containerType, modelAccessor, modelType, propertyName)
        {
            this.metaData = metaData;
        }

        public override IEnumerable<ModelValidator> GetValidators(ControllerContext context)
        {
            foreach (var rule in metaData.Rules)
            {
                yield return new RuleModelValidator(rule, this, context);
            } 
        }
    }
}