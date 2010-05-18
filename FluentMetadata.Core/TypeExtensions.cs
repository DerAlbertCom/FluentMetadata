using System;

namespace FluentMetadata
{
    public static class TypeExtensions
    {
        public static object CreateGenericInstance(this Type genericBaseType, Type firstType, Type secondType, params object[] constructorArgs)
        {
            var genericType = genericBaseType.MakeGenericType(firstType,secondType);
            return Activator.CreateInstance(genericType, constructorArgs);
        }

        public static object CreateGenericInstance(this Type genericBaseType, Type type, params object[] constructorArgs)
        {
            var genericType = genericBaseType.MakeGenericType(type);
            return Activator.CreateInstance(genericType,constructorArgs);
        }
    }
}