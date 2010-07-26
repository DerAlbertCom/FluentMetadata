using System;
using System.Collections.Generic;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    public class MetaData
    {
        private readonly List<IRule> rules;

        public MetaData()
        {
//            ShowDisplay = true;
   //         ShowEditor = true;
            rules = new List<IRule>();
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
            foreach (var rule in metaData.Rules)
            {
                AddRule(rule);
            }
        }

        public bool? Required { get; set; }
        public Type ContainerType { get; set; }
        public Type ModelType { get; set; }
        public int? StringLength { get; set; }
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public bool? Readonly { get; set; }
        public bool? ShowDisplay { get; set; }
        public bool? ShowEditor { get; set; }

        public string TemplateHint { get; set; }

        public string NullDisplayText { get; set; }

        public string DisplayName { get; set; }

        public string DisplayFormat { get; set; }
        public string EditorFormat { get; set; }

        public string Description { get; set; }

        public string Watermark { get; set; }

        public bool? HideSurroundingHtml { get; set; }

        public bool? Hidden { get; set; }

        public string DataTypeName { get; set; }

        public IEnumerable<IRule> Rules
        {
            get { return rules; }
        }

        public void AddRule(IRule rule)
        {
            rules.Add(rule);
        }
    }
}