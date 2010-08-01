using System.Data.Entity.ModelConfiguration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class PropertyConfigurationAdapter : ConfigurationAdapter<PropertyConfiguration>
    {
        public PropertyConfigurationAdapter() : base(null)
        {
        }

        protected override void ConvertToConfiguration(Metadata data)
        {
        }
    }
}