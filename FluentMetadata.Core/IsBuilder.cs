using System;
using System.Linq.Expressions;
using FluentMetadata.Rules;

namespace FluentMetadata
{
    public class IsBuilder<T, TResult> : IIsProperty<T, TResult>
    {
        private readonly PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder;
        private bool notted;

        private MetaData MetaData
        {
            get { return propertyMetaDataBuilder.MetaData; }
        }

        public IsBuilder(PropertyMetadataBuilder<T, TResult> propertyMetaDataBuilder)
        {
            this.propertyMetaDataBuilder = propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> Required()
        {
            MetaData.Required = !notted;
            notted = false;
            if (MetaData.Required)
            {
                MetaData.AddRule(new RequiredRule());
            }
            return propertyMetaDataBuilder;
        }

        public IProperty<T,TResult> ReadOnly()
        {
            MetaData.Readonly = !notted;
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