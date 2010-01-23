using System;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public class PropertyMetadataBuilder : IProperty
    {
        private readonly MetaData metaData;

        protected PropertyMetadataBuilder()
        {
            metaData = new MetaData();
        }

        public PropertyMetadataBuilder(MetaData metaData)
        {
            this.metaData = metaData;
        }

        public MetaData MetaData
        {
            get { return metaData; }
        }

        public IProperty Length(int length)
        {
            metaData.StringLength = length;
            return this;
        }

        public IProperty TemplateHint(string templateHint)
        {
            metaData.TemplateHint = templateHint;
            return this;
        }

        public IProperty Description(string description)
        {
            metaData.Description = description;
            return this;
        }

        public IEditorProperty Editor
        {
            get { return new EditorBuilder(this); }
        }

        public IDisplayProperty Display
        {
            get { return new DisplayBuilder(this); }
        }

        public IAsProperty As
        {
            get { return new AsBuilder(this); }
        }

        public IIsProperty Is
        {
            get { return new IsBuilder(this); }
        }

        public IShouldProperty Should
        {
            get { return new ShouldBuilder(this); }
        }
    }

    public class PropertyMetaDataBuilder<T> : PropertyMetadataBuilder
    {
        public PropertyMetaDataBuilder(Expression<Func<T, object>> expression)
        {
            MetaData.ContainerType = typeof (T);
            MetaData.PropertyName = ExpressionHelper.GetPropertyName(expression);
            MetaData.ModelType = ExpressionHelper.GetPropertyType(expression);
        }
    }
}