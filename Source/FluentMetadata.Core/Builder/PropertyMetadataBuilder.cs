using System;
using System.Linq.Expressions;
using FluentMetadata.Rules;

namespace FluentMetadata.Builder
{
    abstract class PropertyMetadataBuilder
    {
        private readonly Metadata metadata;

        protected PropertyMetadataBuilder()
            : this(new Metadata())
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

    class PropertyMetadataBuilder<T, TResult> : PropertyMetadataBuilder, IProperty<T, TResult>
    {
        public PropertyMetadataBuilder(Metadata metadata)
            : base(metadata)
        {
        }

        public PropertyMetadataBuilder(Expression<Func<T, TResult>> expression)
        {
            Metadata.ContainerType = typeof(T);
            Metadata.ModelName = ExpressionHelper.GetPropertyName(expression);
            Metadata.ModelType = ExpressionHelper.GetPropertyType(expression);
        }

        public IProperty<T, TResult> AssertThat(Func<TResult, bool> assertFunc, string errorMessageFormat)
        {
            AssertThat(assertFunc, () => errorMessageFormat);
            return this;
        }

        public IProperty<T, TResult> AssertThat(Func<TResult, bool> assertFunc, Func<string> errorMessageFormatFunc)
        {
            Metadata.AddRule(new GenericRule<TResult>(assertFunc, errorMessageFormatFunc));
            return this;
        }

        public IProperty<T, TResult> Length(int maxLength)
        {
            Metadata.AddRule(new StringLengthRule(maxLength));
            return this;
        }

        public IProperty<T, TResult> Length(int minLength, int? maxLength)
        {
            Metadata.AddRule(new StringLengthRule(minLength, maxLength));
            return this;
        }

        public IProperty<T, TResult> UIHint(string templateHint)
        {
            Metadata.TemplateHint = templateHint;
            return this;
        }

        public IProperty<T, TResult> Description(string description)
        {
            Metadata.DescriptionFunc = () => description;
            return this;
        }

        public IProperty<T, TResult> Description(Func<string> descriptionFunc)
        {
            Metadata.DescriptionFunc = descriptionFunc;
            return this;
        }

        public IEditorProperty<T, TResult> Editor
        {
            get { return new EditorBuilder<T, TResult>(this); }
        }

        public IDisplayProperty<T, TResult> Display
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

        public IProperty<T, TResult> Range(IComparable minimum, IComparable maximum)
        {
            Metadata.AddRule(new RangeRule(minimum, maximum));
            return this;
        }
    }
}