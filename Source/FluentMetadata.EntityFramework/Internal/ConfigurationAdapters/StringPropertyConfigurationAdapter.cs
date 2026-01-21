using System.Data.Entity.ModelConfiguration.Configuration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class StringPropertyConfigurationAdapter : ConfigurationAdapter<StringPropertyConfiguration>
    {
        public StringPropertyConfigurationAdapter()
            : base(new OptionalPrimitivePropertyConfigurationAdapter())
        {
        }

        protected override void ConvertToConfiguration(Metadata data)
        {
            var maxLength = data.GetMaximumLength();
            if (!maxLength.HasValue)
            {
                return;
            }
            Configuration.HasMaxLength(maxLength);
        }
    }
}