using System;
using System.Collections.Generic;
using System.Linq;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    public class Metadata
    {
        private readonly List<IRule> rules;

        public Metadata()
        {
            ConvertEmptyStringToNull = true;
            RequestValidationEnabled = true;
            ShowDisplay = true;
            ShowEditor = true;
            ModelType = typeof(object);
            rules = new List<IRule>();
            Properties = new PropertiesMetadataCollection();
        }

        public Metadata(Metadata metadata, Type containerType) : this()
        {
            ContainerType = containerType;
            ModelName = metadata.ModelName;
            ModelType = metadata.ModelType;
            CopyMetaDataFrom(metadata);
        }

        internal void CopyMetaDataFrom(Metadata metadata)
        {
            //TODO write tests for CopyMetaDataFrom: properties commented here have not associated tests yet
            ConvertEmptyStringToNull = metadata.ConvertEmptyStringToNull;
            DataTypeName = metadata.DataTypeName;
            DescriptionFunc = metadata.DescriptionFunc;
            DisplayFormatFunc = metadata.DisplayFormatFunc;
            DisplayNameFunc = metadata.DisplayNameFunc;
            EditorFormatFunc = metadata.EditorFormatFunc;
            HideSurroundingHtml = metadata.HideSurroundingHtml;
            ReadOnly = metadata.ReadOnly;
            required = metadata.Required;
            NullDisplayTextFunc = metadata.NullDisplayTextFunc;
            RequestValidationEnabled = metadata.RequestValidationEnabled;
            ShowDisplay = metadata.ShowDisplay;
            ShowEditor = metadata.ShowEditor;
            TemplateHint = metadata.TemplateHint;
            WatermarkFunc = metadata.WatermarkFunc;
            //ErrorMessage = metadata.ErrorMessage;
            Hidden = metadata.Hidden;

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

        /// <summary>
        /// Gets or sets the type of the container for the model.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.ContainerType"/>.
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
        /// <c>true</c> if empty strings that are posted back in forms should be
        /// converted to null; otherwise, <c>false</c>. The default value is <c>true</c>
        /// </value>
        public bool ConvertEmptyStringToNull { get; set; }

        /// <summary>
        /// Gets or sets meta information about the data type.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.DataTypeName"/>.
        /// </summary>
        /// <value>
        /// Meta information about the data type.
        /// </value>
        public string DataTypeName { get; set; }

        /// <summary>
        /// Gets or sets the description function of the model.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.Description"/>.
        /// </summary>
        /// <value>
        /// The description function of the model. The default value is null.
        /// </value>
        internal Func<string> DescriptionFunc { private get; set; }

        /// <summary>
        /// Gets or sets the display format function for the model.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.DisplayFormatString"/>.
        /// </summary>
        /// <value>
        /// The display format function for the model.
        /// </value>
        internal Func<string> DisplayFormatFunc { private get; set; }

        /// <summary>
        /// Gets or sets the GetDisplayName function of the model.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.DisplayName"/>.
        /// </summary>
        /// <value>
        /// The GetDisplayName function of the model.
        /// </value>
        internal Func<string> DisplayNameFunc { private get; set; }

        /// <summary>
        /// Gets or sets the editor format function of the model.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.EditFormatString"/>.
        /// </summary>
        /// <value>
        /// The editor format function of the model.
        /// </value>
        internal Func<string> EditorFormatFunc { private get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the model object should be rendered
        /// using associated HTML elements.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.HideSurroundingHtml"/>.
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
        // MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual bool IsComplexType { get; }

        //
        // Summary:
        //     Gets a value that indicates whether the type is nullable.
        //
        // Returns:
        //     true if the type is nullable; otherwise, false.
        // MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public bool IsNullableValueType { get; }

        /// <summary>
        /// Gets or sets a value that indicates whether the model is read-only.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.IsReadOnly"/>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model is read-only; otherwise, <c>false</c>.
        /// </value>
        internal bool ReadOnly { get; set; }

        private bool? required;
        /// <summary>
        /// Gets or sets a value that indicates whether the model is required.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.IsRequired"/>.
        /// </summary>
        /// <value>
        /// <c>true</c> if the model is required; otherwise, <c>false</c>.
        /// </value>
        public bool? Required
        {
            get => required;
            set
            {
                required = value;
                if (required.Value)
                {
                    AddRule(new RequiredRule());
                }
                else
                {
                    rules.RemoveAll(r => r.Is<RequiredRule>());
                }
            }
        }

        //
        // Summary:
        //     Gets the value of the model.
        //
        // Returns:
        //     The value of the model. For more information about System.Web.Mvc.ModelMetadata,
        //     see the entry ASP.NET MVC 2 Templates, Part 2: ModelMetadata on Brad Wilson's
        //     blog
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public object Model { get; set; }

        /// <summary>
        /// Gets or sets the type of the model.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.ModelType"/>.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        public Type ModelType { get; set; }

        /// <summary>
        /// Gets or sets the string to display for null values.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.NullDisplayText"/>.
        /// </summary>
        /// <value>
        /// The string to display for null values.
        /// </value>
        internal Func<string> NullDisplayTextFunc { private get; set; }

        //
        // Summary:
        //     Gets or sets a value that represents order of the current metadata.
        //
        // Returns:
        //     The order value of the current metadata.
        // TODO MVC3 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual int Order { get; set; }

        /// <summary>
        /// Gets a collection of model metadata objects that describe the properties
        /// of the model.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.Properties"/>.
        /// </summary>
        public PropertiesMetadataCollection Properties { get; }

        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        /// <value>
        /// The name of the model.
        /// </value>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether request validation is enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if request validation is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool RequestValidationEnabled { get; set; }

        //
        // Summary:
        //     Gets or sets a short display name.
        //
        // Returns:
        //     The short display name.
        // TODO MVC2 [in order to complete properties corresponding to System.Web.Mvc.ModelMetadata] public virtual string ShortDisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the property should be displayed in read-only views such as list and detail views.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.ShowForDisplay"/>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model should be displayed in read-only views; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDisplay { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the model should be displayed in editable views.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.ShowForEdit"/>.
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
        /// Gets or sets a function that can be used as a watermark.
        /// </summary>
        /// <value>
        /// The watermark function.
        /// </value>
        internal Func<string> WatermarkFunc { private get; set; }

        #endregion

        //TODO [DerAlbertCom] What kind of ErrorMessage is this? Write some XML docs and/or tests.
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a property or field value should be rendered as a hidden input element.
        /// Corresponds to <see cref="System.Web.Mvc.ModelMetadata.HiddenInputAttribute"/>.
        /// </summary>
        /// <value>
        /// <c>true</c> if the model should be rendered as a hidden input element; otherwise, <c>false</c>.
        /// </value>
        public bool? Hidden { get; set; }

        public IEnumerable<IRule> Rules => rules;

        internal void AddRule(IRule rule)
        {
            var classRule = rule as IClassRule;
            if (classRule != null &&
                ModelType.Is(classRule.ClassType) ||
                classRule == null &&
                ModelType.Is(rule.PropertyType))
            {
                rules.RemoveAll(r => r.Equals(rule));
                rules.Add(rule);
            }
        }

        public string GetDescription()
        {
            return DescriptionFunc == null ?
                null :
                DescriptionFunc();
        }

        internal string GetDisplayFormat()
        {
            return DisplayFormatFunc == null ?
                null :
                DisplayFormatFunc();
        }

        public string GetDisplayName()
        {
            return DisplayNameFunc == null ?
                null :
                DisplayNameFunc();
        }

        internal string GetEditorFormat()
        {
            return EditorFormatFunc == null ?
                null :
                EditorFormatFunc();
        }

        internal string GetNullDisplayText()
        {
            return NullDisplayTextFunc == null ?
                null :
                NullDisplayTextFunc();
        }

        internal string GetWatermark()
        {
            return WatermarkFunc == null ?
                null :
                WatermarkFunc();
        }

        public object GetRangeMinimum()
        {
            var rangeRule = GetLastRuleOfType<RangeRule>();
            return rangeRule == null ? null : rangeRule.Minimum;
        }

        public object GetRangeMaximum()
        {
            var rangeRule = GetLastRuleOfType<RangeRule>();
            return rangeRule == null ? null : rangeRule.Maximum;
        }

        public int? GetMaximumLength()
        {
            var lengthRule = GetLastRuleOfType<StringLengthRule>();
            if (lengthRule == null)
            {
                return null;
            }
            return lengthRule.Maximum;
        }

        public int? GetMinimumLength()
        {
            var lengthRule = GetLastRuleOfType<StringLengthRule>();
            if (lengthRule == null)
            {
                return null;
            }
            return lengthRule.Minimum;
        }

        private T GetLastRuleOfType<T>()
        {
            return Rules.OfType<T>().LastOrDefault();
        }
    }
}