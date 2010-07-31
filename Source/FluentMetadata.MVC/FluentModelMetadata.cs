using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentMetadata.Rules;

namespace FluentMetadata.MVC
{
    public class FluentModelMetadata : ModelMetadata
    {
        private readonly Metadata metadata;
        
        public FluentModelMetadata(Metadata metadata , ModelMetadataProvider provider, Func<object> modelAccessor) : base(provider, metadata.ContainerType, modelAccessor, metadata.ModelType, metadata.ModelName)
        {
            this.metadata = metadata;
        }

        public override IEnumerable<ModelValidator> GetValidators(ControllerContext context)
        {
            foreach (var rule in metadata.Rules)
            {
                if (rule is IClassRule)
                {
                    yield return new ClassRuleModelValidator((IClassRule) rule, this, context);
                }
                else
                {
                    yield return new RuleModelValidator(rule, this, context);
                }
            } 
        }
    }
}