namespace FluentMetadata
{
    public static class TypeMetadataBuilderExtensions
    {
        public static void CopyMetadataFrom<T, TBaseType>(this TypeMetadataBuilder<T> typeBuilder)
        {
            var nameBuilder = new PropertyNameMetadataBuilder(typeof (TBaseType));

            foreach (var namedMetaData in nameBuilder.NamedMetaData)
            {
                var propertyInfo = typeof (T).GetProperty(namedMetaData.PropertyName);
                if (propertyInfo != null)
                {
                    typeBuilder.MapProperty(typeof (T), propertyInfo.Name, namedMetaData.MetaData);
                }
            }
        }
    }
}