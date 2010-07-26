using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    public class FluentMetadataProvider : ModelMetadataProvider
    {
        private readonly ModelMetadataProvider fallback;

        public FluentMetadataProvider(ModelMetadataProvider fallback)
        {
            this.fallback = fallback;
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            if (!FluentMetadataBuilder.HasTypeBuilder(containerType))
            {
                foreach (var metadata in fallback.GetMetadataForProperties(container, containerType))
                {
                    yield return metadata;
                }
            }
            else
            {
                var typeBuilder = FluentMetadataBuilder.GetTypeBuilder(containerType);
                var propertyInfos = containerType.GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    ModelMetadata modelMetadata = CreateModelMetaData(propertyInfo, typeBuilder, () => container);
                    if (modelMetadata == null)
                    {
                        modelMetadata = GetMetadataForProperty(() => container, containerType, propertyInfo.Name);
                    }
                    yield return modelMetadata;
                }
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
            ModelMetadata metadataForProperty = null;
            if (FluentMetadataBuilder.HasTypeBuilder(containerType))
            {
                var typeBuilder = FluentMetadataBuilder.GetTypeBuilder(containerType);
                metadataForProperty = CreateModelMetaData(propertyInfo, typeBuilder, modelAccessor);
            }
            return
                metadataForProperty
                ??
                fallback.GetMetadataForProperty(modelAccessor, containerType, propertyName);
        }

        private ModelMetadata CreateModelMetaData(PropertyInfo propertyInfo,
                                                  TypeMetadataBuilder builder, Func<object> modelAccessor)
        {
            var metaData = builder.MetaDataFor(propertyInfo.Name);
            if (metaData != null)
            {
                return CreateModelMetaData(metaData, modelAccessor);
            }
            return null;
        }

        private ModelMetadata CreateModelMetaData(MetaData metaData,
                                                  Func<object> modelAccessor)
        {
            var modelMetaData = new FluentModelMetadata(metaData, this, metaData.ContainerType, modelAccessor,
                                                        metaData.ModelType, metaData.PropertyName);
            if (metaData.DisplayName!=null)
            {
                modelMetaData.DisplayName = metaData.DisplayName;
            }
            if (metaData.ShowDisplay.HasValue)
            {
                modelMetaData.ShowForDisplay = metaData.ShowDisplay.Value;
            }
            return modelMetaData;
            //return new FluentModelMetadata(metaData, this, metaData.ContainerType, modelAccessor, metaData.ModelType,
            //                               metaData.PropertyName)
            //           {

            //               DisplayName = metaData.DisplayName,
            //               ShowForDisplay = metaData.ShowDisplay.Value,
            //               ShowForEdit = metaData.ShowEditor,
            //               TemplateHint = metaData.TemplateHint,
            //               IsReadOnly = metaData.Readonly,
            //               DataTypeName = GetDataTypeName(metaData),
            //               NullDisplayText = metaData.NullDisplayText,
            //               DisplayFormatString = metaData.DisplayFormat,
            //               EditFormatString = metaData.EditorFormat,
            //               Description = metaData.Description,
            //               IsRequired = metaData.Required,
            //               Watermark = metaData.Watermark,
            //               HideSurroundingHtml = metaData.HideSurroundingHtml
            //           };
        }

        private static string GetDataTypeName(MetaData metaData)
        {
            if (metaData.Hidden.HasValue && metaData.Hidden.Value)
            {
                return "HiddenInput";
            }
            return metaData.DataTypeName;
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            if (FluentMetadataBuilder.HasTypeBuilder(modelType))
            {
                var builder = FluentMetadataBuilder.GetTypeBuilder(modelType);
                return new FluentModelMetadata(builder.MetaData, this, null, modelAccessor, modelType, "");
            }
            return fallback.GetMetadataForType(modelAccessor, modelType);
        }
    }
}