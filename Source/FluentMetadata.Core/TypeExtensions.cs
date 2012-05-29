using System;

namespace FluentMetadata
{
    static class TypeExtensions
    {
        internal static object CreateGenericInstance(this Type genericBaseType, Type firstType, Type secondType, params object[] constructorArgs)
        {
            return Activator.CreateInstance(
                genericBaseType.MakeGenericType(firstType, secondType),
                constructorArgs);
        }

        internal static object CreateGenericInstance(this Type genericBaseType, Type type, params object[] constructorArgs)
        {
            return Activator.CreateInstance(
                genericBaseType.MakeGenericType(type),
                constructorArgs);
        }

        internal static bool Is(this Type type, Type otherType)
        {
            return otherType.IsAssignableFrom(type);
        }

        internal static bool Is<TOther>(this Type type)
        {
            return type.Is(typeof(TOther));
        }
    }
}