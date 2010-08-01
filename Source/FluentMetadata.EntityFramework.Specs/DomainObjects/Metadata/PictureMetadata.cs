namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class PictureMetadata : ContentMetadata<Picture>
    {
        public PictureMetadata()
        {
            Property(p => p.OriginalFilename).Length(256).Is.Required();
            Property(p => p.Position).Is.Required();
        }
    }
}