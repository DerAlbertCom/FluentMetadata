using System;

namespace FluentMetadata
{
    /// <summary>
    /// Sets metadata relevant for displaying a model.
    /// </summary>
    /// <typeparam name="T">The type of the model.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IDisplayProperty<T, TResult>
    {
        /// <summary>
        /// Sets the null text of the property.
        /// Use this for static, i.e. culture invariant null texts.
        /// </summary>
        /// <param name="nullDisplayText">The null display text.</param>
        /// <returns></returns>
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
        /// <param name="displayNameFunc">The display name function.</param>
        /// <returns></returns>
        IProperty<T, TResult> Name(Func<string> displayNameFunc);

        /// <summary>
        /// Sets the display format of the property.
        /// Use this for static, i.e. culture invariant display formats.
        /// </summary>
        /// <param name="displayFormat">The display format.</param>
        /// <returns></returns>
        IProperty<T, TResult> Format(string displayFormat);
    }
}