using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentMetadata
{
    /// <summary>Refeclective extensions for <see cref="Type"/>.</summary>
    public static class TypeExtensions
    {
        internal static object CreateGenericInstance(this Type genericBaseType, Type firstType, Type secondType, params object[] constructorArgs)
            => Activator.CreateInstance(genericBaseType.MakeGenericType(firstType, secondType), constructorArgs);

        internal static object CreateGenericInstance(this Type genericBaseType, Type type, params object[] constructorArgs)
            => Activator.CreateInstance(genericBaseType.MakeGenericType(type), constructorArgs);

        internal static bool Is(this Type type, Type otherType) => otherType.IsAssignableFrom(type);
        internal static bool Is<TOther>(this Type type) => type.Is(typeof(TOther));

        /// <summary>
        /// Gets the one hiding property from <paramref name="sameNameProperties"/>
        /// which may include properties declared on a base class
        /// but hidden by another property using the <c>new</c> keyword.
        /// </summary>
        public static PropertyInfo GetHiding(this IEnumerable<PropertyInfo> sameNameProperties)
        {
            var declaringTypes = sameNameProperties.Select(p => p.DeclaringType).ToArray();

            return sameNameProperties
                .OrderBy(p => declaringTypes.Count(d => p.DeclaringType.Is(d)))
                .Last();
        }
    }

    internal static class ObjectExtensions
    {
        internal static bool Is<T>(this object obj) => obj.GetType().Is<T>();
    }
}