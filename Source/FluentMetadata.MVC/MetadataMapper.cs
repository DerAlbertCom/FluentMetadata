using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    internal static class MetadataMapper
    {
        public static void CopyMetadata(Metadata source, ModelMetadata destination)
        {
            //TODO map missing Metadata properties
            destination.ConvertEmptyStringToNull = source.ConvertEmptyStringToNull;
            destination.DataTypeName = source.DataTypeName;
            //TODO [MVC3] destination.Description = source.Description;
            destination.DisplayFormatString = source.DisplayFormat;
            destination.DisplayName = source.GetDisplayName();
            destination.EditFormatString = source.EditorFormat;
            if (source.HideSurroundingHtml.HasValue)
            {
                destination.HideSurroundingHtml = source.HideSurroundingHtml.Value;
            }
            destination.IsReadOnly = source.Readonly;
            if (source.Required.HasValue)
            {
                destination.IsRequired = source.Required.Value;
            }
            destination.NullDisplayText = source.NullDisplayText;
            destination.ShowForDisplay = source.ShowDisplay;
            destination.ShowForEdit = source.ShowEditor;
            destination.TemplateHint = GetTemplateHint(source);
            //TODO [MVC3] destination.Watermark = source.Watermark;
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