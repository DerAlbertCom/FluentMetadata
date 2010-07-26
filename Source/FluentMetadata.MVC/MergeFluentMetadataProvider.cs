using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    public class MergeFluentMetadataProvider : DataAnnotationsModelMetadataProvider
    {

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metaData = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (metaData != null)
            {
                MergeMetaData(metaData, metaData.ContainerType, metaData.PropertyName);
            }
            return metaData;
        }

        private void MergeMetaData(ModelMetadata modelMetaData, Type containerType, string propertyName)
        {
            if (containerType==null || propertyName==null || !FluentMetadataBuilder.HasTypeBuilder(containerType))
            {
                return;
            }
            var metaData = FluentMetadataBuilder.GetTypeBuilder(containerType).MetaDataFor(propertyName);
            if (metaData==null)
                return;
            if (metaData.Required.HasValue)
                modelMetaData.IsRequired = metaData.Required.Value;
            if (metaData.DataTypeName != null)
                modelMetaData.DataTypeName = metaData.DataTypeName;
            if (metaData.Readonly.HasValue)
                modelMetaData.IsReadOnly = metaData.Readonly.Value;
            if (metaData.ShowDisplay.HasValue)
                modelMetaData.ShowForDisplay = metaData.ShowDisplay.Value;
            if (metaData.ShowEditor.HasValue)
                modelMetaData.ShowForEdit = metaData.ShowEditor.Value;
            if (metaData.TemplateHint != null)
                modelMetaData.TemplateHint = metaData.TemplateHint;
            if (metaData.NullDisplayText != null)
                modelMetaData.NullDisplayText = metaData.NullDisplayText;
            if (metaData.DisplayName != null)
                modelMetaData.DisplayName = metaData.DisplayName;


            //            modelMetaData. StringLength = metaData.StringLength;
            //            modelMetaData.ErrorMessage = metaData.ErrorMessage;
        }
    }
}