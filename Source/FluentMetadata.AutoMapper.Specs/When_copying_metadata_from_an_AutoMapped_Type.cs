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
            destinationRenamedMetadata;

        public When_copying_metadata_from_an_AutoMapped_Type()
        {
            Mapper.CreateMap<Source, Destination>()
                .ForMember(destination => destination.Renamed, o => o.MapFrom(source => source.Named));
            Mapper.AssertConfigurationIsValid();

            FluentMetadataBuilder.ForAssemblyOfType<Source>();

            var query = new QueryFluentMetadata();
            destinationMetadata = query.GetMetadataFor(typeof(Destination));
            destinationMyPropertyMetadata = destinationMetadata.Properties
                .Single(m => m.ModelName == "MyProperty");
            destinationRenamedMetadata = destinationMetadata.Properties
                .Single(m => m.ModelName == "Renamed");
        }

        [Fact]
        public void a_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal("pockÃ¤nsdfsdf", destinationMyPropertyMetadata.GetDisplayName());
        }

        [Fact]
        public void the_destination_type_should_have_metadata_from_the_source_type_it_is_mapped_to()
        {
            Assert.Equal("rzjsfghgafsdfh", destinationMetadata.GetDisplayName());
        }

        //TODO [on AutoMapper update] check if AutoMapper makes projected source property accessible
        [Fact(Skip = "unsupported until AutoMapper makes projected source property accessible")]
        public void a_projected_destination_property_should_have_metadata_from_the_source_property_it_is_mapped_to()
        {
            Assert.Equal("adföoiulkanhsda", destinationRenamedMetadata.Description);
        }

        public void Dispose()
        {
            FluentMetadataBuilder.Reset();
            Mapper.Reset();
        }
    }
}