namespace FluentMetadata.AutoMapper.Specs
{
    class Source
    {
        public string MyProperty { get; set; }
        public int Named { get; set; }
        public Nested Nested { get; set; }
    }

    class Nested
    {
        public FurtherNested FurtherNested { get; set; }
    }

    class FurtherNested
    {
        public int Id { get; set; }
    }

    class Destination
    {
        public string MyProperty { get; set; }
        public int Renamed { get; set; }
        public int NestedFurtherNestedId { get; set; }
    }

    class SourceMetaData : ClassMetadata<Source>
    {
        public SourceMetaData()
        {
            Class.Display.Name("rzjsfghgafsdfh");
            Property(x => x.MyProperty).Display.Name("pockänsdfsdf");
            Property(x => x.Named).Description("adföoiulkanhsda");
        }
    }

    class FurtherNestedMetaData : ClassMetadata<FurtherNested>
    {
        public FurtherNestedMetaData()
        {
            Property(x => x.Id).Is.Required();
        }
    }

    class TargetMetaData : ClassMetadata<Destination>
    {
        public TargetMetaData()
        {
            this.CopyAutoMappedMetadataFrom(typeof(Source));
        }
    }
}