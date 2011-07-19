using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace FluentMetadata.FluentNHibernate.Conventions
{
    public class FluentMetaDataConvention : IPropertyConvention
    {
        private readonly QueryFluentMetadata query = new QueryFluentMetadata();

        public void Apply(IPropertyInstance instance)
        {
            var meta = query.FindMetadataFor(instance.EntityType, instance.Property.Name);
            if (meta != null)
            {
                if (meta.Required.HasValue)
                {
                    ApplyRequired(meta.Required.Value, instance);
                }

                var maxLength = meta.GetMaximumLength();
                if (maxLength.HasValue)
                {
                    ApplyStringLength(maxLength.Value, instance);
                }
            }
        }

        private static void ApplyStringLength(int stringLength, IPropertyInstance target)
        {
            if (stringLength > 0)
            {
                target.Length(stringLength);
            }
        }

        private static void ApplyRequired(bool required, IPropertyInstance target)
        {
            if (required)
            {
                target.Not.Nullable();
            }
        }
    }
}