using System.Data.Entity.ModelConfiguration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class StringPropertyConfigurationAdapter : ConfigurationAdapter<StringPropertyConfiguration>
    {
        public StringPropertyConfigurationAdapter() : base(new OptionalPrimitivePropertyConfigurationAdapter())
        {
        }

        protected override void ConvertToConfiguration(Metadata data)
        {
            if (data.StringLength > 0)
            {
                Configuration.MaxLength = data.StringLength;
            }
        }
    }
}