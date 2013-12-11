using System;

namespace FluentMetadata.Builder
{
    internal class DisplayBuilder<T> : IDisplayClass<T>
    {
        private readonly IClassBuilder<T> classBuilder;

        public DisplayBuilder(IClassBuilder<T> classBuilder)
        {
            this.classBuilder = classBuilder;
        }

        public IClassBuilder<T> Name(string displayName)
        {
            classBuilder.Metadata.DisplayNameFunc = () => displayName;
            return classBuilder;
        }

        public IClassBuilder<T> Name(Func<string> displayNameFunc)
        {
            classBuilder.Metadata.DisplayNameFunc = displayNameFunc;
            return classBuilder;
        }

        public IClassBuilder<T> Format(string displayFormat)
        {
            classBuilder.Metadata.DisplayFormatFunc = () => displayFormat;
            return classBuilder;
        }

        public IClassBuilder<T> Format(Func<string> displayFormatFunc)
        {
            classBuilder.Metadata.DisplayFormatFunc = displayFormatFunc;
            return classBuilder;
        }
    }

    internal class DisplayBuilder<T, TResult> : IDisplayProperty<T, TResult>
    {
        private readonly PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder;

        public DisplayBuilder(PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> NullText(string nullDisplayText)
        {
            propertyMetaDataBuilder.Metadata.NullDisplayTextFunc = () => nullDisplayText;
            return propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> NullText(Func<string> nullDisplayTextFunc)
        {
            propertyMetaDataBuilder.Metadata.NullDisplayTextFunc = nullDisplayTextFunc;
            return propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> Name(string displayName)
        {
            propertyMetaDataBuilder.Metadata.DisplayNameFunc = () => displayName;
            return propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> Name(Func<string> displayNameFunc)
        {
            propertyMetaDataBuilder.Metadata.DisplayNameFunc = displayNameFunc;
            return propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> Format(string displayFormat)
        {
            propertyMetaDataBuilder.Metadata.DisplayFormatFunc = () => displayFormat;
            return propertyMetaDataBuilder;
        }

        public IProperty<T, TResult> Format(Func<string> displayFormatFunc)
        {
            propertyMetaDataBuilder.Metadata.DisplayFormatFunc = displayFormatFunc;
            return propertyMetaDataBuilder;
        }
    }
}