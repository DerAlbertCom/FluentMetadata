using System;
using System.Linq.Expressions;
using FluentMetadata.Rules;

namespace FluentMetadata.Builder
{
    internal abstract class PropertyMetadataBuilder
    {
        private readonly Metadata metadata;

        protected PropertyMetadataBuilder() : this(new Metadata())
        {
        }

        protected PropertyMetadataBuilder(Metadata metadata)
        {
            this.metadata = metadata;
        }

        public Metadata Metadata
        {
            get { return metadata; }
        }
    }

    internal class PropertyMetadataBuilder<T,TResult> : PropertyMetadataBuilder, IProperty<T,TResult>
    {
        public PropertyMetadataBuilder(Expression<Func<T, TResult>> expression)
        {
            Metadata.ContainerType = typeof (T);
            Metadata.ModelName = ExpressionHelper.GetPropertyName(expression);
            Metadata.ModelType = ExpressionHelper.GetPropertyType(expression);
        }

        public PropertyMetadataBuilder(Metadata metadata) : base(metadata)
        {
        }

        public PropertyMetadataBuilder(string propertyName)
        {
            Metadata.ContainerType = null;
            Metadata.ModelName = propertyName;
            Metadata.ModelType = typeof (T);
        }


        public IProperty<T,TResult> Length(int length)
        {
            Metadata.StringLength = length;
            Metadata.AddRule(new StringLengthRule(length));
            return this;
        }

        public IProperty<T,TResult> UIHint(string templateHint)
        {
            Metadata.TemplateHint = templateHint;
            return this;
        }

        public IProperty<T,TResult> Description(string description)
        {
            Metadata.Description = description;
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