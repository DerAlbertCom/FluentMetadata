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
            classBuilder.Metadata.DisplayFormat = displayFormat;
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
            propertyMetaDataBuilder.Metadata.NullDisplayText = nullDisplayText;
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
            propertyMetaDataBuilder.Metadata.DisplayFormat = displayFormat;
            return propertyMetaDataBuilder;
        }
    }
}