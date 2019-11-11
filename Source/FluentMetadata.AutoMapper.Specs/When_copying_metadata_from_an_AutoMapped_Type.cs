using AutoMapper;
using Xunit;

namespace FluentMetadata.AutoMapper.Specs
{
    public class When_copying_metadata_from_an_AutoMapped_Type
    {
        private readonly Metadata destinationMetadata;

        public When_copying_metadata_from_an_AutoMapped_Type()
        {
            FluentMetadataBuilder.Reset();
            Mapper.Reset();

            Mapper.CreateMap<Source, Destination>()
                .ForMember(d => d.Renamed, o => o.MapFrom(s => s.Named))
                .ForMember(d => d.IntProperty, o => o.ResolveUsing<FakeResolver>().FromMember(s => s.StringField));

            Mapper.AssertConfigurationIsValid();
            FluentMetadataBuilder.ForAssemblyOfType<Source>();

            destinationMetadata = QueryFluentMetadata.GetMetadataFor(typeof(Destination));
        }

        [Fact]
        public void a_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal("pockänsdfsdf", destinationMetadata.Properties[nameof(Destination.MyProperty)].GetDisplayName());
        }

        [Fact]
        public void the_destination_type_should_have_metadata_from_the_source_type_it_is_mapped_to()
        {
            Assert.Equal("rzjsfghgafsdfh", destinationMetadata.GetDisplayName());
        }

        [Fact]
        public void a_projected_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal("adföoiulkanhsda", destinationMetadata.Properties[nameof(Destination.Renamed)].GetDescription());
        }

        [Fact]
        public void a_flattened_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal(true, destinationMetadata.Properties[nameof(Destination.NestedFurtherNestedId)].Required);
        }

        [Fact]
        public void a_destination_property_resolved_from_a_source_property_should_have_metadata_from_the_source_property()
        {
            Assert.Equal("üoicvnqwnb", destinationMetadata.Properties[nameof(Destination.IntProperty)].TemplateHint);
        }
    }
}