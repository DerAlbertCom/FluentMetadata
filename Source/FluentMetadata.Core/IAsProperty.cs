using System.ComponentModel.DataAnnotations;

namespace FluentMetadata
{
    /// <summary>
    /// Sets the DataType of a model property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IAsProperty<T, TResult>
    {
        /// <summary>
        /// Sets the DataType to email address.
        /// </summary>
        /// <returns></returns>
        IProperty<T, TResult> EmailAddress();

        /// <summary>
        /// Sets the DataType to URL.
        /// </summary>
        /// <returns></returns>
        IProperty<T, TResult> Url();

        /// <summary>
        /// Sets the DataType to HTML.
        /// </summary>
        /// <returns></returns>
        IProperty<T, TResult> Html();

        /// <summary>
        /// Sets the DataType to text.
        /// </summary>
        /// <returns></returns>
        IProperty<T, TResult> Text();

        /// <summary>
        /// Sets the DataType to multiline text.
        /// </summary>
        /// <returns></returns>
        IProperty<T, TResult> MultilineText();

        /// <summary>
        /// Sets the DataType to password.
        /// </summary>
        /// <returns></returns>
        IProperty<T, TResult> Password();

        /// <summary>
        /// Sets the DataType to a custom value.
        /// </summary>
        /// <param name="dataTypeName">Name of the data type.</param>
        /// <returns></returns>
        IProperty<T, TResult> Custom(string dataTypeName);

        /// <summary>
        /// Sets the DataType to a custom DataType.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns></returns>
        IProperty<T, TResult> Custom(DataType dataType);
    }
}