using System;
using System.Linq;
using AutoMapper;
using Xunit;

namespace FluentMetadata.AutoMapper.Specs
{
    public class When_copying_metadata_from_an_AutoMapped_Type : IDisposable
    {
        Metadata destinationMetadata,
            destinationMyPropertyMetadata,
            destinationRenamedMetadata,
            destinationNestedFurtherNestedIdMetadata,
            destinationIntPropertyMetadata;

        public When_copying_metadata_from_an_AutoMapped_Type()
        {
            Mapper.CreateMap<Source, Destination>()
                .ForMember(d => d.Renamed, o => o.MapFrom(s => s.Named))
                .ForMember(d => d.IntProperty, o => o.ResolveUsing<FakeResolver>().FromMember(s => s.StringField));
            Mapper.AssertConfigurationIsValid();

            FluentMetadataBuilder.ForAssemblyOfType<Source>();

            var query = new QueryFluentMetadata();
            destinationMetadata = query.GetMetadataFor(typeof(Destination));
            destinationMyPropertyMetadata = destinationMetadata.Properties
                .Single(m => m.ModelName == "MyProperty");
            destinationRenamedMetadata = destinationMetadata.Properties
                .Single(m => m.ModelName == "Renamed");
            destinationNestedFurtherNestedIdMetadata = destinationMetadata.Properties
                .Single(m => m.ModelName == "NestedFurtherNestedId");
            destinationIntPropertyMetadata = destinationMetadata.Properties
                .Single(m => m.ModelName == "IntProperty");
        }

        [Fact]
        public void a_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal("pockänsdfsdf", destinationMyPropertyMetadata.GetDisplayName());
        }

        [Fact]
        public void the_destination_type_should_have_metadata_from_the_source_type_it_is_mapped_to()
        {
            Assert.Equal("rzjsfghgafsdfh", destinationMetadata.GetDisplayName());
        }

        [Fact]
        public void a_projected_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal("adföoiulkanhsda", destinationRenamedMetadata.Description);
        }

        [Fact]
        public void a_flattened_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal(true, destinationNestedFurtherNestedIdMetadata.Required);
        }

        [Fact]
        public void a_destination_property_resolved_from_a_source_property_should_have_metadata_from_the_source_property()
        {
            Assert.Equal("üoicvnqwnb", destinationIntPropertyMetadata.TemplateHint);
        }

        public void Dispose()
        {
            FluentMetadataBuilder.Reset();
            Mapper.Reset();
        }
    }
}