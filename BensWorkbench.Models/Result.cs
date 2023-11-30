namespace BensWorkbench.Models;

/// <summary>
/// Class which encapsulates function results as either a success type <typeparamref name="T"/>, 
/// or some error type which extends Exception <typeparamref name="E"/>
/// </summary>
/// <typeparam name="T">Type for object which represents success</typeparam>
public class Result<T> : IEquatable<T>
{
    private Exception? ErrorObject { get; }
    private T? ValueInternal { get; }

    public Result(T obj)
    {
        ValueInternal = obj;
    }

    public Result(Exception exception)
    {
        ErrorObject = exception;
    }

    public T Unwrap()
    {
        if (ErrorObject is not null)
            throw ErrorObject;


        return ValueInternal ??
          throw (ErrorObject ?? new Exception("Attempted to Unwrap an unused Result"));
    }

    public bool IsErr()
    {
        return ErrorObject is not null;
    }

    public bool IsErr<F>() where F : Exception
    {
        if (ErrorObject is null)
            return false;

        return typeof(F).Equals(ErrorObject!.GetType());
    }

    public bool IsOK()
    {
        return ErrorObject is null && ValueInternal is not null;
    }

    public static implicit operator Result<T>(T obj) => new(obj);
    public static implicit operator Result<T>(Exception exception) => new(exception);
    public static implicit operator T(Result<T> rslt) => rslt.Unwrap();

    public static bool operator ==(Result<T> obj1, T obj2)
    {
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Result<T> obj1, T obj2)
    {
        return obj1.Equals(obj2);
    }

    public static bool operator ==(Result<T> obj1, Exception obj2)
    {
        try
        {
            obj1.Unwrap();
            return false;
        }
        catch (Exception exception)
        {
            return exception.Message == obj2.Message;
        }
    }

    public static bool operator !=(Result<T> obj1, Exception obj2)
    {
        try
        {
            obj1.Unwrap();
            return true;
        }
        catch (Exception exception)
        {
            return exception.Message != obj2.Message; ;
        }
    }

    public bool Equals(T? other)
    {
        return other?.Equals(ValueInternal) ?? false;
    }
}
