using FluentMetadata.Specs.SampleClasses;
using Xunit;

namespace FluentMetadata.Specs
{
    public class BaseClass_Tests : MetadataTestBase
    {
        private Metadata id;

        public BaseClass_Tests()
        {
            var query = new QueryFluentMetadata();
            id = query.GetMetadataFor(typeof(BaseClass), "Id");
        }

        [Fact]
        public void Id_Required_is_True()
        {
            id.Required.Value.ShouldBeTrue();
        }
    }


    public class DerivedClass_Tests : MetadataTestBase
    {
        private Metadata id;
        private Metadata title;

        public DerivedClass_Tests()
        {
            var query = new QueryFluentMetadata();
            id = query.GetMetadataFor(typeof (DerivedClass), "Id");
            title = query.GetMetadataFor(typeof(DerivedClass), "Title");
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
        private Metadata id;
        private Metadata title;

        public DerivedDerivedClass_Tests()
        {
            var query = new QueryFluentMetadata();
            id = query.GetMetadataFor(typeof(DerivedDerivedClass), "Id");
            title = query.GetMetadataFor(typeof(DerivedDerivedClass), "Title");
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