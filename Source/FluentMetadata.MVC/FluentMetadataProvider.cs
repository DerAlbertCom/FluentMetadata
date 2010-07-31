using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using FluentMetadata.Builder;

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
                                                  ITypeMetadataBuilder builder, Func<object> modelAccessor)
        {
            var metaData = builder.MetaDataFor(propertyInfo.Name);
            if (metaData != null)
            {
                return CreateModelMetaData(metaData, modelAccessor);
            }
            return null;
        }

        private ModelMetadata CreateModelMetaData(Metadata metadata,
                                                  Func<object> modelAccessor)
        {
            var modelMetaData = new FluentModelMetadata(metadata, this, metadata.ContainerType, modelAccessor,
                                                        metadata.ModelType, metadata.ModelName);
            if (metadata.DisplayName!=null)
            {
                modelMetaData.DisplayName = metadata.DisplayName;
            }
            if (metadata.ShowDisplay.HasValue)
            {
                modelMetaData.ShowForDisplay = metadata.ShowDisplay.Value;
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

        private static string GetDataTypeName(Metadata metadata)
        {
            if (metadata.Hidden.HasValue && metadata.Hidden.Value)
            {
                return "HiddenInput";
            }
            return metadata.DataTypeName;
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            if (FluentMetadataBuilder.HasTypeBuilder(modelType))
            {
                var builder = FluentMetadataBuilder.GetTypeBuilder(modelType);
                return new FluentModelMetadata(builder.Metadata, this, null, modelAccessor, modelType, "");
            }
            return fallback.GetMetadataForType(modelAccessor, modelType);
        }
    }
}