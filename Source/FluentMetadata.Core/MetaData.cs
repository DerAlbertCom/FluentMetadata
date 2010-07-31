using System;
using System.Collections.Generic;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    public class Metadata
    {
        private readonly List<IRule> rules;

        public Metadata()
        {
//            ShowDisplay = true;
   //         ShowEditor = true;
            rules = new List<IRule>();
        }

        public Metadata(Metadata metadata, Type containerType) : this()
        {
            Required = metadata.Required;
            ContainerType = containerType;
            ModelType = metadata.ModelType;
            StringLength = metadata.StringLength;
            ModelName = metadata.ModelName;
            ErrorMessage = metadata.ErrorMessage;
            DataTypeName = metadata.DataTypeName;
            Readonly = metadata.Readonly;
            ShowDisplay = metadata.ShowDisplay;
            ShowEditor = metadata.ShowEditor;
            TemplateHint = metadata.TemplateHint;
            NullDisplayText = metadata.NullDisplayText;
            DisplayName = metadata.DisplayName;
            foreach (var rule in metadata.Rules)
            {
                AddRule(rule);
            }
        }

        public bool? Required { get; set; }
        public Type ContainerType { get; set; }
        public Type ModelType { get; set; }
        public int? StringLength { get; set; }
        public string ModelName { get; set; }
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