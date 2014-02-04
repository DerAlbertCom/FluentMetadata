using System;

namespace FluentMetadata
{
    public interface IProperty<T, TResult>
    {
        IProperty<T, TResult> AssertThat(Func<TResult, bool> assertFunc, string errorMessageFormat);
        IProperty<T, TResult> Length(int maxLength);
        IProperty<T, TResult> Length(int minLength, int? maxLength);
        IProperty<T, TResult> UIHint(string templateHint);
        IProperty<T, TResult> Description(string description);
        IProperty<T, TResult> Range(IComparable minimum, IComparable maximum);
        IEditorProperty<T, TResult> Editor { get; }
        IDisplayProperty<T, TResult> Display { get; }
        IAsProperty<T, TResult> As { get; }
        IIsProperty<T, TResult> Is { get; }
        IShouldProperty<T, TResult> Should { get; }
    }
}