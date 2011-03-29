using System;
using System.Collections.Generic;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    public class Metadata
    {
        private readonly List<IRule> rules;
        private readonly PropertiesMetadata properties;

        public Metadata()
        {
            ConvertEmptyStringToNull = true;
            ShowDisplay = true;
            ShowEditor = true;
            rules = new List<IRule>();
            properties = new PropertiesMetadata();
        }

        public Metadata(Metadata metadata, Type containerType)
            : this()
        {
            ContainerType = containerType;
            ModelName = metadata.ModelName;
            ModelType = metadata.ModelType;
            CopyMetaDataFrom(metadata);
        }

        internal void CopyMetaDataFrom(Metadata metadata)
        {
            Required = metadata.Required;
            StringLength = metadata.StringLength;
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

        #region properties corresponding to System.Web.Mvc.ModelMetadata

        // Summary:
        //     Gets a dictionary that contains additional metadata about the model.
        //
        // Returns:
        //     A dictionary that contains additional metadata about the model.
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual Dictionary<string, object> AdditionalValues { get; }

        // ~ System.Web.Mvc.ModelMetadata.ContainerType
        /// <summary>
        /// Gets or sets the type of the container for the model.
        /// </summary>
        /// <value>
        /// The type of the container for the model.
        /// </value>
        public Type ContainerType { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether empty strings that are posted
        /// back in forms should be converted to null.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if empty strings that are posted back in forms should be
        /// 	converted to null; otherwise, <c>false</c>. The default value is <c>true</c>
        /// </value>
        public bool ConvertEmptyStringToNull { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.DataTypeName
        /// <summary>
        /// Gets or sets meta information about the data type.
        /// </summary>
        /// <value>
        /// Meta information about the data type.
        /// </value>
        public string DataTypeName { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.Description
        /// <summary>
        /// Gets or sets the description of the model.
        /// </summary>
        /// <value>
        /// The description of the model. The default value is null.
        /// </value>
        public string Description { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.DisplayFormatString
        /// <summary>
        /// Gets or sets the display format string for the model.
        /// </summary>
        /// <value>
        /// The display format string for the model.
        /// </value>
        public string DisplayFormat { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.DisplayName
        /// <summary>
        /// Gets or sets the display name of the model.
        /// </summary>
        /// <value>
        /// The display name of the model.
        /// </value>
        public string DisplayName { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.EditFormatString
        /// <summary>
        /// Gets or sets the edit format string of the model.
        /// </summary>
        /// <value>
        /// The edit format string of the model.
        /// </value>
        public string EditorFormat { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.HideSurroundingHtml
        /// <summary>
        /// Gets or sets a value that indicates whether the model object should be rendered
        /// using associated HTML elements.
        /// </summary>
        /// <value>
        /// <c>true</c> if the associated HTML elements that contains the model object should
        /// be included with the object; otherwise, <c>false</c>.
        /// </value>
        public bool? HideSurroundingHtml { get; set; }

        //
        // Summary:
        //     Gets or sets a value that indicates whether the model is a complex type.
        //
        // Returns:
        //     A value that indicates whether the model is considered a complex type by
        //     the MVC framework.
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual bool IsComplexType { get; }

        //
        // Summary:
        //     Gets a value that indicates whether the type is nullable.
        //
        // Returns:
        //     true if the type is nullable; otherwise, false.
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public bool IsNullableValueType { get; }

        // ~ System.Web.Mvc.ModelMetadata.IsReadOnly
        /// <summary>
        /// Gets or sets a value that indicates whether the model is read-only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model is read-only; otherwise, <c>false</c>.
        /// </value>
        public bool Readonly { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.IsRequired
        /// <summary>
        /// Gets or sets a value that indicates whether the model is required.
        /// </summary>
        /// <value>
        /// <c>true</c> if the model is required; otherwise, <c>false</c>.
        /// </value>
        public bool? Required { get; set; }

        //
        // Summary:
        //     Gets the value of the model.
        //
        // Returns:
        //     The value of the model. For more information about System.Web.Mvc.ModelMetadata,
        //     see the entry ASP.NET MVC 2 Templates, Part 2: ModelMetadata on Brad Wilson's
        //     blog
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public object Model { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.ModelType
        /// <summary>
        /// Gets or sets the type of the model.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        public Type ModelType { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.NullDisplayText
        /// <summary>
        /// Gets or sets the string to display for null values.
        /// </summary>
        /// <value>
        /// The string to display for null values.
        /// </value>
        public string NullDisplayText { get; set; }

        //
        // Summary:
        //     Gets or sets a value that represents order of the current metadata.
        //
        // Returns:
        //     The order value of the current metadata.
        // TODO MVC3 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual int Order { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.Properties
        /// <summary>
        /// Gets a collection of model metadata objects that describe the properties
        /// of the model.
        /// </summary>
        public PropertiesMetadata Properties { get { return properties; } }

        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        /// <value>
        /// The name of the model.
        /// </value>
        public string ModelName { get; set; }

        //
        // Summary:
        //     Gets or sets a value that indicates whether request validation is enabled.
        //
        // Returns:
        //     true if request validation is enabled; otherwise, false.
        // TODO MVC3 public virtual bool RequestValidationEnabled { get; set; }

        //
        // Summary:
        //     Gets or sets a short display name.
        //
        // Returns:
        //     The short display name.
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual string ShortDisplayName { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.ShowForDisplay
        /// <summary>
        /// Gets or sets a value that indicates whether the property should be displayed in read-only views such as list and detail views.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model should be displayed in read-only views; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDisplay { get; set; }

        // ~ System.Web.Mvc.ModelMetadata.ShowForEdit
        /// <summary>
        /// Gets or sets a value that indicates whether the model should be displayed in editable views.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model should be displayed in editable views; otherwise, <c>false</c>.
        /// </value>
        public bool ShowEditor { get; set; }

        //
        // Summary:
        //     Gets or sets the simple display string for the model.
        //
        // Returns:
        //     The simple display string for the model.
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual string SimpleDisplayText { get; set; }

        /// <summary>
        /// Gets or sets a hint that suggests what template to use for this model.
        /// </summary>
        /// <value>
        /// A hint that suggests what template to use for this model.
        /// </value>
        public string TemplateHint { get; set; }

        /// <summary>
        /// Gets or sets a value that can be used as a watermark.
        /// </summary>
        /// <value>
        /// The watermark.
        /// </value>
        public string Watermark { get; set; }

        #endregion

        // TODO add StringLengthRule to rules automatically on set
        public int? StringLength { get; set; }
        public string ErrorMessage { get; set; }
        public bool? Hidden { get; set; }

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