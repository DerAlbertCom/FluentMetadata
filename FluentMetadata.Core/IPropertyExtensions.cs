namespace FluentMetadata
{
    public static class IPropertyExtensions
    {
        public static IProperty WithMaxSize(this IProperty property)
        {
            return property.Length(2^31);
        }
    }
}