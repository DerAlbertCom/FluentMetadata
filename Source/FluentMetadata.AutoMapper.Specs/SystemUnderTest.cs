using AutoMapper;

namespace FluentMetadata.AutoMapper.Specs
{
    internal class Source
    {
        public string MyProperty { get; set; }
        public int Named { get; set; }
        public Nested Nested { get; set; }
        public string StringField { get; set; }
    }

    internal class Nested
    {
        public FurtherNested FurtherNested { get; set; }
    }

    internal class FurtherNested
    {
        public int Id { get; set; }
    }

    internal class Destination
    {
        public string MyProperty { get; set; }
        public int Renamed { get; set; }
        public int NestedFurtherNestedId { get; set; }
        public int IntProperty { get; set; }
    }

    internal class FakeResolver : ValueResolver<string, int>
    {
        protected override int ResolveCore(string source)
        {
            return default(int);
        }
    }

    internal class SourceMetaData : ClassMetadata<Source>
    {
        public SourceMetaData()
        {
            Class.Display.Name("rzjsfghgafsdfh");
            Property(x => x.MyProperty).Display.Name("pockänsdfsdf");
            Property(x => x.Named).Description("adföoiulkanhsda");
            Property(x => x.StringField).UIHint("üoicvnqwnb");
        }
    }

    internal class FurtherNestedMetaData : ClassMetadata<FurtherNested>
    {
        public FurtherNestedMetaData()
        {
            Property(x => x.Id).Is.Required();
        }
    }

    internal class TargetMetaData : ClassMetadata<Destination>
    {
        public TargetMetaData()
        {
            this.CopyAutoMappedMetadataFrom(typeof(Source));
        }
    }
}