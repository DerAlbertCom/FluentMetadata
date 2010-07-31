using FluentMetadata.Rules;

namespace FluentMetadata.Builder
{
    internal class IsBuilder<T, TResult> : IIsProperty<T, TResult>
    {
        private readonly PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder;
        private bool notted;

        private Metadata Metadata
        {
            get { return propertyMetaDataBuilder.Metadata; }
        }

        public IsBuilder(PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Required()
        {
            Metadata.Required = !notted;
            notted = false;
            if (Metadata.Required)
            {
                Metadata.AddRule(new RequiredRule());
            }
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> ReadOnly()
        {
            Metadata.Readonly = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }


        public IIsNotProperty<T, TResult> Not
        {
            get
            {
                notted = true;
                return this;
            }
        }
    }
}