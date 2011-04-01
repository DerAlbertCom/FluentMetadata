namespace FluentMetadata.AutoMapper.Specs
{
    class Source
    {
        public string MyProperty { get; set; }
    }

    class Destination
    {
        public string MyProperty { get; set; }
    }

    class SourceMetaData : ClassMetadata<Source>
    {
        public SourceMetaData()
        {
            Class.Display.Name("rzjsfghgafsdfh");
            Property(x => x.MyProperty).Display.Name("pockänsdfsdf");
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