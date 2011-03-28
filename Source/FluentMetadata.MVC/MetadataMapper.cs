using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    internal static class MetadataMapper
    {
        public static void CopyMetadata(Metadata source, ModelMetadata destination)
        {
            destination.DisplayName = source.DisplayName;
            destination.ShowForDisplay = source.ShowDisplay;
            destination.DataTypeName = GetDataTypeName(source);
            destination.ConvertEmptyStringToNull = source.ConvertEmptyStringToNull;
        }

        private static string GetDataTypeName(Metadata metadata)
        {
            if (metadata.Hidden.HasValue && metadata.Hidden.Value)
            {
                return "HiddenInput";
            }
            return metadata.DataTypeName;
        }


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
    }
}