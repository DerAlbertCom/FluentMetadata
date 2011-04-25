using System.Web.Mvc;

namespace FluentMetadata.MVC
{
    internal static class MetadataMapper
    {
        public static void CopyMetadata(Metadata source, ModelMetadata destination)
        {
            //TODO map missing Metadata properties
            destination.ConvertEmptyStringToNull = source.ConvertEmptyStringToNull;
            destination.DataTypeName = GetDataTypeName(source);
            //TODO [MVC3] destination.Description = source.Description;
            destination.DisplayFormatString = source.DisplayFormat;
            destination.DisplayName = source.DisplayName;
            destination.EditFormatString = source.EditorFormat;
            //destination.ShowForDisplay = source.ShowDisplay;
        }

        private static string GetDataTypeName(Metadata metadata)
        {
            if (metadata.Hidden.HasValue && metadata.Hidden.Value)
            {
                return "HiddenInput";
            }
            return metadata.DataTypeName;
        }
    }
}