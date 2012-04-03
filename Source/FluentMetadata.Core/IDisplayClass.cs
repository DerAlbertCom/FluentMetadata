using System;

namespace FluentMetadata
{
    /// <summary>
    /// Sets display metadata on the type.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public interface IDisplayClass<T>
    {
        /// <summary>
        /// Sets the display name of the class.
        /// Use this for static, i.e. culture invariant display names.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        IClassBuilder<T> Name(string displayName);

        /// <summary>
        /// Sets the display name of the class.
        /// Use this for dynamic, i.e. localized display names, e.g. resource strings.
        /// </summary>
        /// <param name="displayName">The display name funtion.</param>
        /// <returns></returns>
        IClassBuilder<T> Name(Func<string> displayNameFunc);

        /// <summary>
        /// Sets the display format of the class.
        /// Use this for dynamic, i.e. localized display formats, e.g. resource strings.
        /// </summary>
        /// <param name="displayFormat">The display format.</param>
        /// <returns></returns>
        IClassBuilder<T> Format(string displayFormat);
    }
}