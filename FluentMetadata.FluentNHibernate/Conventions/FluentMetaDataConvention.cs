using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace FluentMetadata.FluentNHibernate.Conventions
{
    public class FluentMetaDataConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            var builder = FluentMetadataBuilder.GetTypeBuilder(instance.EntityType);
            if (builder == null)
            {
                return;
            }
            var meta = builder.MetaDataFor(instance.Property.Name);
            if (meta == null)
            {
                return;
            }
            ApplyRequired(meta.Required, instance);
            ApplyStringLength(meta.StringLength, instance);
        }

        private void ApplyStringLength(int stringLength, IPropertyInstance target)
        {
            if (stringLength > 0)
            {
                target.Length(stringLength);
            }
        }

        private void ApplyRequired(bool required, IPropertyInstance target)
        {
            if (required)
            {
                target.Not.Nullable();
            }
        }
    }
}