using System.Data.Entity.ModelConfiguration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class OptionalPrimitivePropertyConfigurationAdapter :
        ConfigurationAdapter<OptionalPrimitivePropertyConfiguration>
    {
        public OptionalPrimitivePropertyConfigurationAdapter() : base(new PrimitivePropertyConfigurationAdapter())
        {
        }

        protected override void ConvertToConfiguration(Metadata data)
        {
            Configuration.Optional = !data.Required;
        }
    }
}