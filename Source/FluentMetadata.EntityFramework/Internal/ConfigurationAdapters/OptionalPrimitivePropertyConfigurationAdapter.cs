using System.Data.Entity.ModelConfiguration.Configuration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class OptionalPrimitivePropertyConfigurationAdapter :
        ConfigurationAdapter<PrimitivePropertyConfiguration>
    {
        public OptionalPrimitivePropertyConfigurationAdapter()
            : base(new PrimitivePropertyConfigurationAdapter())
        {
        }

        protected override void ConvertToConfiguration(Metadata data)
        {
            if (!data.Required.HasValue)
                return;
            if (data.Required.Value)
                Configuration.IsRequired();
            else
                Configuration.IsOptional();
        }
    }
}