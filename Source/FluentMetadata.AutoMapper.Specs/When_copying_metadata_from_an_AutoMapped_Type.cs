using System;
using System.Linq;
using AutoMapper;
using Xunit;

namespace FluentMetadata.AutoMapper.Specs
{
    public class When_copying_metadata_from_an_AutoMapped_Type : IDisposable
    {
        Metadata destinationMetadata,
            destinationMyPropertyMetadata;

        public When_copying_metadata_from_an_AutoMapped_Type()
        {
            Mapper.CreateMap<Source, Destination>();
            Mapper.AssertConfigurationIsValid();

            FluentMetadataBuilder.ForAssemblyOfType<Source>();

            var query = new QueryFluentMetadata();
            destinationMetadata = query.GetMetadataFor(typeof(Destination));
            destinationMyPropertyMetadata = destinationMetadata.Properties
                .Single(m => m.ModelName == "MyProperty");
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

        public void Dispose()
        {
            FluentMetadataBuilder.Reset();
            Mapper.Reset();
        }
    }
}