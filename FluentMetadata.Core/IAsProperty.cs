namespace FluentMetadata
{
    public interface IAsProperty<T, TResult>
    {
        IProperty<T,TResult> EmailAddress();
        IProperty<T,TResult> Url();
        IProperty<T,TResult> Html();
        IProperty<T,TResult> Text();
        IProperty<T,TResult> MultilineText();
        IProperty<T,TResult> Password();
    }
}