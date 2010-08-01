using System.Data.Entity.ModelConfiguration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class PrimitivePropertyConfigurationAdapter : ConfigurationAdapter<PrimitivePropertyConfiguration>
    {
        public PrimitivePropertyConfigurationAdapter() : base(new PropertyConfigurationAdapter())
        {
        }

        protected override void ConvertToConfiguration(Metadata data)
        {
        }
    }
}