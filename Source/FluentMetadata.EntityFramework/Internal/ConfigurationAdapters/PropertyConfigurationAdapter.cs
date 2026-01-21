using System.Data.Entity.ModelConfiguration.Configuration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class PropertyConfigurationAdapter : ConfigurationAdapter<PrimitivePropertyConfiguration>
    {
        public PropertyConfigurationAdapter()
            : base(null)
        {
        }

        protected override void ConvertToConfiguration(Metadata data)
        {
        }
    }
}