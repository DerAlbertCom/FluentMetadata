using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    internal static class MetadataMapper
    {
        public static void CopyMetadata(Metadata source, ModelMetadata destination)
        {
            destination.ConvertEmptyStringToNull = source.ConvertEmptyStringToNull;
            destination.DataTypeName = source.DataTypeName;
            destination.Description = source.GetDescription();
            destination.DisplayFormatString = source.GetDisplayFormat();
            destination.DisplayName = source.GetDisplayName();
            destination.EditFormatString = source.GetEditorFormat();

            if (source.HideSurroundingHtml.HasValue)
            {
                destination.HideSurroundingHtml = source.HideSurroundingHtml.Value;
            }

            destination.IsReadOnly = source.ReadOnly;

            if (source.Required.HasValue)
            {
                destination.IsRequired = source.Required.Value;
            }

            destination.NullDisplayText = source.GetNullDisplayText();
            destination.RequestValidationEnabled = source.RequestValidationEnabled;
            destination.ShowForDisplay = source.ShowDisplay;
            destination.ShowForEdit = source.ShowEditor;
            destination.TemplateHint = GetTemplateHint(source);
            destination.Watermark = source.GetWatermark();
        }

        private static string GetTemplateHint(Metadata metadata)
        {
            if (metadata.Hidden.HasValue && metadata.Hidden.Value)
            {
                return "HiddenInput";
            }

            return metadata.TemplateHint;
        }
    }
}