using System;

namespace FluentMetadata
{
    internal static class TypeExtensions
    {
        internal static object CreateGenericInstance(this Type genericBaseType, Type firstType, Type secondType, params object[] constructorArgs)
            => Activator.CreateInstance(genericBaseType.MakeGenericType(firstType, secondType), constructorArgs);

        internal static object CreateGenericInstance(this Type genericBaseType, Type type, params object[] constructorArgs)
            => Activator.CreateInstance(genericBaseType.MakeGenericType(type), constructorArgs);

        internal static bool Is(this Type type, Type otherType) => otherType.IsAssignableFrom(type);
        internal static bool Is<TOther>(this Type type) => type.Is(typeof(TOther));
    }

    internal static class ObjectExtensions
    {
        internal static bool Is<T>(this object obj) => obj.GetType().Is<T>();
    }
}