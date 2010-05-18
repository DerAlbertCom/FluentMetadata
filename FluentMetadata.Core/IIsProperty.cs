using System;
using System.Linq.Expressions;

namespace FluentMetadata
{
    public interface IIsProperty<T, TResult> : IIsNotProperty<T, TResult>
    {
        IIsNotProperty<T, TResult> Not { get; }
    }

    public interface IIsNotProperty<T,TResult>
    {
        IProperty<T,TResult> Required();
        IProperty<T,TResult> ReadOnly();
    }
}