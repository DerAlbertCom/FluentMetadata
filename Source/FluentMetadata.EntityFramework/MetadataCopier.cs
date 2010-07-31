using System.Data.Entity.ModelConfiguration;

namespace FluentMetadata.EntityFramework
{
    public class MetadataCopier
    {
        public  void CopyMetadata(PropertyConfiguration configuration, Metadata data)
        {
            if (configuration is StringPropertyConfiguration)
            {
                SetMetadata((StringPropertyConfiguration)configuration, data);
                return;
            }
            if (configuration is BinaryPropertyConfiguration)
            {
                SetMetadata((BinaryPropertyConfiguration)configuration, data);
                return;
            }
            if (configuration is OptionalPrimitivePropertyConfiguration)
            {
                SetMetadata((OptionalPrimitivePropertyConfiguration)configuration, data);
                return;
            }
            if (configuration is DecimalPropertyConfiguration)
            {
                SetMetadata((DecimalPropertyConfiguration)configuration, data);
                return;
            }
            if (configuration is DateTimePropertyConfiguration)
            {
                SetMetadata((DateTimePropertyConfiguration)configuration, data);
                return;
            }
            if (configuration is PrimitivePropertyConfiguration)
            {
                SetMetadata((PrimitivePropertyConfiguration)configuration, data);
                return;
            }
        }

        private void SetMetadata(PropertyConfiguration configuration, Metadata data)
        {
        }

        private void SetMetadata(StringPropertyConfiguration result, Metadata data)
        {
            if (FluentMetadataBuilder.HasTypeBuilder(data.ContainerType))
            {
                var metaData = FluentMetadataBuilder.GetTypeBuilder(data.ContainerType);
                var propertyMetadata = metaData.MetaDataFor(data.ModelName);
                if (propertyMetadata != null)
                {
                    if (propertyMetadata.StringLength > 0)
                    {
                        result.MaxLength = propertyMetadata.StringLength;
                    }
                }
            }
            SetMetadata((OptionalPrimitivePropertyConfiguration)result, data);
        }

        private void SetMetadata(PrimitivePropertyConfiguration configuration, Metadata data)
        {
            SetMetadata((PropertyConfiguration)configuration, data);
        }

        private void SetMetadata(DateTimePropertyConfiguration configuration, Metadata data)
        {
            SetMetadata((PrimitivePropertyConfiguration)configuration, data);
        }

        private void SetMetadata(DecimalPropertyConfiguration configuration, Metadata data)
        {
            SetMetadata((PrimitivePropertyConfiguration)configuration, data);
        }

        private void SetMetadata(OptionalPrimitivePropertyConfiguration configuration, Metadata data)
        {
            configuration.Optional = !data.Required;
            SetMetadata((PrimitivePropertyConfiguration)configuration, data);
        }

        private void SetMetadata(BinaryPropertyConfiguration configuration, Metadata data)
        {
            SetMetadata((OptionalPrimitivePropertyConfiguration)configuration, data);
        }

        
    }
}