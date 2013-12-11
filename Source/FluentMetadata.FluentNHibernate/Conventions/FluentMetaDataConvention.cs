using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace FluentMetadata.FluentNHibernate.Conventions
{
    public class FluentMetadataConvention : IPropertyConvention, IReferenceConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            var meta = QueryFluentMetadata.FindMetadataFor(instance.EntityType, instance.Property.Name);
            if (meta != null)
            {
                if (meta.Required.HasValue && meta.Required.Value)
                {
                    instance.Not.Nullable();
                }

                var maxLength = meta.GetMaximumLength();
                if (maxLength.HasValue && maxLength.Value > 0)
                {
                    instance.Length(maxLength.Value);
                }
            }
        }

        public void Apply(IManyToOneInstance instance)
        {
            var meta = QueryFluentMetadata.FindMetadataFor(instance.EntityType, instance.Property.Name);
            if (meta != null && meta.Required.HasValue && meta.Required.Value)
            {
                instance.Not.Nullable();
            }
        }
    }
}