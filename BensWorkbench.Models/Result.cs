using System.IO.IsolatedStorage;

namespace BensWorkbench.Models;

public class Result<T, E> : IEquatable<T>
    where E : Exception
{
    private E? ErrorObject { get; }
    private T? ValueInternal { get; }

    public Result(T obj)
    {
        ValueInternal = obj;
    }

    public Result(E exception)
    {
        ErrorObject = exception;
    }

    public T Unwrap()
    {
        if (ErrorObject is not null)
            throw ErrorObject;


        return ValueInternal ?? throw (ErrorObject ?? new Exception());
    }

    public bool IsErr()
    {
        return ErrorObject is not null;
    }

    public bool IsErr<F>() where F : Exception
    {
        return typeof(F).Equals(ErrorObject.GetType());
    }

    public bool IsOK()
    {
        return ErrorObject is null && ValueInternal is not null;
    }

    public static implicit operator Result<T, E>(T obj) => new(obj);
    public static implicit operator Result<T, E>(E exception) => new(exception);
    public static implicit operator T(Result<T, E> rslt) => rslt.Unwrap();

    public static bool operator ==(Result<T, E> obj1, T obj2)
    {
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Result<T, E> obj1, T obj2)
    {
        return obj1.Equals(obj2);
    }

    public static bool operator ==(Result<T, E> obj1, E obj2)
    {
        try
        {
            obj1.Unwrap();
            return false;
        }
        catch (E exception)
        {
            return exception.Message == obj2.Message;
        }
    }

    public static bool operator !=(Result<T, E> obj1, E obj2)
    {
        try
        {
            obj1.Unwrap();
            return true;
        }
        catch (E exception)
        {
            return exception.Message != obj2.Message; ;
        }
    }

    public bool Equals(T? other)
    {
        return other?.Equals(ValueInternal) ?? false;
    }
}