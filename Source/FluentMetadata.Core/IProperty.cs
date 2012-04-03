using System;

namespace FluentMetadata
{
    /// <summary>
    /// Sets metadata on a property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IProperty<T, TResult>
    {
        /// <summary>
        /// Creates a generic property rule (i.e. a rule that is evaluated in property context)
        /// asserting that the <paramref name="assertFunc"/> returns true.
        /// </summary>
        /// <param name="assertFunc">What to assert.</param>
        /// <param name="errorMessageFormat">The static error message format.
        /// Can contain a placeholder ({0}) for the class display name.</param>
        /// <returns></returns>
        IProperty<T, TResult> AssertThat(Func<TResult, bool> assertFunc, string errorMessageFormat);

        /// <summary>
        /// Sets text length restrictions on the property value.
        /// </summary>
        /// <param name="maxLength">The maximum text length.</param>
        /// <returns></returns>
        IProperty<T, TResult> Length(int maxLength);

        /// <summary>
        /// Sets text length restrictions on the property value.
        /// </summary>
        /// <param name="minLength">The minimum text length.</param>
        /// <param name="maxLength">The maximum text length.</param>
        /// <returns></returns>
        IProperty<T, TResult> Length(int minLength, int? maxLength);

        /// <summary>
        /// Sets the template hint used by Dynamic Data to decide which template to use for displaying or editing the property.
        /// </summary>
        /// <param name="templateHint">The template hint.</param>
        /// <returns></returns>
        IProperty<T, TResult> UIHint(string templateHint);

        /// <summary>
        /// Sets the description of the property.
        /// Use this for static, i.e. culture invariant descriptions.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        IProperty<T, TResult> Description(string description);

        /// <summary>
        /// Sets valid range constraints on the property value.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns></returns>
        IProperty<T, TResult> Range(IComparable minimum, IComparable maximum);

        /// <summary>
        /// Sets editor metadata.
        /// </summary>
        IEditorProperty<T, TResult> Editor { get; }

        /// <summary>
        /// Sets display metadata.
        /// </summary>
        IDisplayProperty<T, TResult> Display { get; }

        /// <summary>
        /// Sets the DataType.
        /// </summary>
        IAsProperty<T, TResult> As { get; }

        /// <summary>
        /// Sets various metadata flags.
        /// </summary>
        IIsProperty<T, TResult> Is { get; }

        /// <summary>
        /// Sets various metadata flags and creates rules.
        /// </summary>
        IShouldProperty<T, TResult> Should { get; }
    }
}