using System;

namespace FluentMetadata
{
    public interface IDisplayProperty<T, TResult>
    {
        IProperty<T, TResult> NullText(string nullDisplayText);

        /// <summary>
        /// Sets the display name of the property.
        /// Use this for static, i.e. culture invariant display names.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        IProperty<T, TResult> Name(string displayName);

        /// <summary>
        /// Sets the display name of the property.
        /// Use this for dynamic, i.e. localized display names, e.g. resource strings.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        IProperty<T, TResult> Name(Func<string> displayNameFunc);

        IProperty<T, TResult> Format(string displayFormat);
    }
}