namespace FluentMetadata
{
    public static class PropertyExtensions
    {
        public static IProperty WithMaxSize(this IProperty property)
        {
            return property.Length(2^31);
        }
    }
}