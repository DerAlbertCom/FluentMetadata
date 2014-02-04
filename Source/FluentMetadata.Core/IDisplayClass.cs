using System;

namespace FluentMetadata
{
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
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        IClassBuilder<T> Name(Func<string> displayNameFunc);

        IClassBuilder<T> Format(string displayFormat);
    }
}