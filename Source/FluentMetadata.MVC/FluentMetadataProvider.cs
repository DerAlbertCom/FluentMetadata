using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    public class FluentMetadataProvider : ModelMetadataProvider
    {
        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            var typeBuilder = FluentMetadataBuilder.GetTypeBuilder(containerType);
            var propertyInfos = containerType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                yield return CreateModelMetaData(containerType, propertyInfo, typeBuilder, null);
            }
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType,
                                                             string propertyName)
        {
            var propertyInfo = containerType.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                return null;
            }
            var typeBuilder = FluentMetadataBuilder.GetTypeBuilder(containerType);
            return CreateModelMetaData(containerType, propertyInfo, typeBuilder, modelAccessor);
        }

        private ModelMetadata CreateModelMetaData(Type containerType, PropertyInfo propertyInfo,
                                                  TypeMetadataBuilder builder, Func<object> modelAccessor)
        {
            var metaData = builder.MetaDataFor(propertyInfo.Name);
            if (metaData != null)
            {
                return CreateModelMetaData(metaData, modelAccessor);
            }
            return new ModelMetadata(this, containerType, modelAccessor, propertyInfo.PropertyType,
                                     propertyInfo.Name);
        }

        private ModelMetadata CreateModelMetaData(MetaData metaData,
                                                  Func<object> modelAccessor)
        {
            return new FluentModelMetadata(metaData,this, metaData.ContainerType, modelAccessor, metaData.ModelType,
                                     metaData.PropertyName)
                       {
                           DisplayName = metaData.DisplayName,
                           ShowForDisplay = metaData.ShowDisplay,
                           ShowForEdit = metaData.ShowEditor,
                           TemplateHint = metaData.TemplateHint,
                           IsReadOnly = metaData.Readonly,
                           DataTypeName = GetDataTypeName(metaData),
                           NullDisplayText = metaData.NullDisplayText,
                           DisplayFormatString = metaData.DisplayFormat,
                           EditFormatString = metaData.EditorFormat,
                           Description = metaData.Description,
                           IsRequired = metaData.Required,
                           Watermark = metaData.Watermark,
                           HideSurroundingHtml = metaData.HideSurroundingHtml
                       };
        }

        private static string GetDataTypeName(MetaData metaData)
        {
            if (metaData.Hidden)
            {
                return "HiddenInput";
            }
            return metaData.DataTypeName;
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            var builder = FluentMetadataBuilder.GetTypeBuilder(modelType);
            return new FluentModelMetadata(builder.MetaData, this, null, modelAccessor, modelType, "");
        }
    }
}