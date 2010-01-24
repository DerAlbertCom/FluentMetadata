using FluentMetadata.Rules;

namespace FluentMetadata
{
    public class IsBuilder : IIsProperty
    {
        private readonly PropertyMetadataBuilder propertyMetaDataBuilder;
        private bool notted;

        private MetaData MetaData
        {
            get { return propertyMetaDataBuilder.MetaData; }
        }

        public IsBuilder(PropertyMetadataBuilder propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty Required()
        {
            MetaData.Required = !notted;
            notted = false;
            if (MetaData.Required)
            {
                MetaData.AddRule(new RequiredRule());
            }
            return propertyMetaDataBuilder;
        }

        public IProperty ReadOnly()
        {
            MetaData.Readonly = !notted;
            notted = false;
            return propertyMetaDataBuilder;
        }

        public IIsNotProperty Not
        {
            get
            {
                notted = true;
                return this;
            }
        }
    }
}