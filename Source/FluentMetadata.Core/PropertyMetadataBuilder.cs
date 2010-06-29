using System;
using System.Linq.Expressions;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    public abstract class PropertyMetadataBuilder
    {
        private readonly MetaData metaData;

        protected PropertyMetadataBuilder() : this(new MetaData())
        {
        }

        protected PropertyMetadataBuilder(MetaData metaData)
        {
            this.metaData = metaData;
        }

        public MetaData MetaData
        {
            get { return metaData; }
        }
    }

    public class PropertyMetadataBuilder<T,TResult> : PropertyMetadataBuilder, IProperty<T,TResult>
    {
        public PropertyMetadataBuilder(Expression<Func<T, TResult>> expression)
        {
            MetaData.ContainerType = typeof (T);
            MetaData.PropertyName = ExpressionHelper.GetPropertyName(expression);
            MetaData.ModelType = ExpressionHelper.GetPropertyType(expression);
        }

        public PropertyMetadataBuilder(MetaData metaData) : base(metaData)
        {
        }

        public PropertyMetadataBuilder(string propertyName)
        {
            MetaData.ContainerType = null;
            MetaData.PropertyName = propertyName;
            MetaData.ModelType = typeof (T);
        }


        public IProperty<T,TResult> Length(int length)
        {
            MetaData.StringLength = length;
            MetaData.AddRule(new StringLengthRule(length));
            return this;
        }

        public IProperty<T,TResult> TemplateHint(string templateHint)
        {
            MetaData.TemplateHint = templateHint;
            return this;
        }

        public IProperty<T,TResult> Description(string description)
        {
            MetaData.Description = description;
            return this;
        }

        public IEditorProperty<T,TResult> Editor
        {
            get { return new EditorBuilder<T, TResult>(this); }
        }

        public IDisplayProperty<T,TResult> Display
        {
            get { return new DisplayBuilder<T, TResult>(this); }
        }

        public IAsProperty<T, TResult> As
        {
            get { return new AsBuilder<T, TResult>(this); }
        }

        public IIsProperty<T, TResult> Is
        {
            get { return new IsBuilder<T, TResult>(this); }
        }

        public IShouldProperty<T, TResult> Should
        {
            get { return new ShouldBuilder<T, TResult>(this); }
        }
    }
}