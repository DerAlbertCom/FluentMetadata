using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class BaseClass_Tests : MetadataTestBase
    {
        readonly Metadata id;

        public BaseClass_Tests()
        {
            id = QueryFluentMetadata.GetMetadataFor(typeof(BaseClass), "Id");
        }

        [Fact]
        public void Id_Required_is_True()
        {
            id.Required.Value.ShouldBeTrue();
        }
    }

    public class DerivedClass_Tests : MetadataTestBase
    {
        readonly Metadata id, title;

        public DerivedClass_Tests()
        {
            id = QueryFluentMetadata.GetMetadataFor(typeof(DerivedClass), "Id");
            title = QueryFluentMetadata.GetMetadataFor(typeof(DerivedClass), "Title");
        }

        [Fact]
        public void Title_Required_is_true()
        {
            title.Required.Value.ShouldBeTrue();
        }

        [Fact]
        public void Id_Required_is_True()
        {
            id.Required.Value.ShouldBeTrue();
        }
    }

    public class DerivedDerivedClass_Tests : MetadataTestBase
    {
        readonly Metadata id, title;

        public DerivedDerivedClass_Tests()
        {
            id = QueryFluentMetadata.GetMetadataFor(typeof(DerivedDerivedClass), "Id");
            title = QueryFluentMetadata.GetMetadataFor(typeof(DerivedDerivedClass), "Title");
        }

        [Fact]
        public void Title_Required_is_true()
        {
            title.Required.Value.ShouldBeTrue();
        }

        [Fact]
        public void Id_Required_is_True()
        {
            id.Required.Value.ShouldBeTrue();
        }
    }
}