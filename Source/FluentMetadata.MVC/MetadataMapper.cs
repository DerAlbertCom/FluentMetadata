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
            destination.DisplayName = source.DisplayName;
            //TODO [MVC3] destination.Description = source.Description;
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