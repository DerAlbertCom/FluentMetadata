namespace FluentMetadata
{
    /// <summary>
    /// Sets metadata relevant for editing a model.
    /// </summary>
    /// <typeparam name="T">The type of the model.</typeparam>
    /// <typeparam name="TResult">The type of the property.</typeparam>
    public interface IEditorProperty<T, TResult>
    {
        /// <summary>
        /// Sets the error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        IProperty<T, TResult> ErrorMessage(string errorMessage);

        /// <summary>
        /// Sets the editor format of the property.
        /// Use this for static, i.e. culture invariant editor formats.
        /// </summary>
        /// <param name="editorFormat">The editor format.</param>
        /// <returns></returns>
        IProperty<T, TResult> Format(string editorFormat);
        
        /// <summary>
        /// Sets the watermark of the property.
        /// Use this for static, i.e. culture invariant watermarks.
        /// </summary>
        /// <param name="watermark">The watermark.</param>
        /// <returns></returns>
        IProperty<T, TResult> Watermark(string watermark);
    }
}