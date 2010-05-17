namespace FluentMetadata
{
    public static class PropertyExtensions
    {
        public static IProperty<T, TResult> WithMaxSize<T, TResult>(this IProperty<T, TResult> property)
        {
            return property.Length(2^31);
        }
    }
}