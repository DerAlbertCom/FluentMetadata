using System;

namespace FluentMetadata
{
    public class MetaData
    {
        public MetaData()
        {
            ShowDisplay = true;
            ShowEditor = true;
        }

        public MetaData(MetaData metaData, Type containerType) : this()
        {
            Required = metaData.Required;
            ContainerType = containerType;
            ModelType = metaData.ModelType;
            StringLength = metaData.StringLength;
            PropertyName = metaData.PropertyName;
            ErrorMessage = metaData.ErrorMessage;
            DataTypeName = metaData.DataTypeName;
            Readonly = metaData.Readonly;
            ShowDisplay = metaData.ShowDisplay;
            ShowEditor = metaData.ShowEditor;
            TemplateHint = metaData.TemplateHint;
            NullDisplayText = metaData.NullDisplayText;
            DisplayName = metaData.DisplayName;
        }

        public bool Required { get; set; }
        public Type ContainerType { get; set; }
        public Type ModelType { get; set; }
        public int StringLength { get; set; }
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public bool Readonly { get; set; }
        public bool ShowDisplay { get; set; }
        public bool ShowEditor { get; set; }

        public string TemplateHint { get; set; }

        public string NullDisplayText { get; set; }

        public string DisplayName { get; set; }

        public string DisplayFormat { get; set; }
        public string EditorFormat { get; set; }

        public string Description { get; set; }

        public string Watermark { get; set; }

        public bool HideSurroundingHtml { get; set; }

        public bool Hidden { get; set; }

        public string DataTypeName { get; set; }
    }
}