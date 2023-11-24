namespace BensWorkbench.Models;

public class Result<T, E> : IEquatable<T>
    where E : Exception
    where T : IEquatable<T>, new()
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
    
    public static implicit operator Result<T,E>(T obj) => new(obj);
    public static implicit operator Result<T,E>(E exception) => new(exception);
    
    public static bool operator ==(Result<T,E> obj1, T obj2)
    {
        return obj1.Equals(obj2);
    }
    
    public static bool operator !=(Result<T,E> obj1, T obj2)
    {
        return obj1.Equals(obj2);
    }
    
    public static bool operator ==(Result<T,E> obj1, E obj2)
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

    public static bool operator !=(Result<T,E> obj1, E obj2)
    {
        try
        {
            obj1.Unwrap();
            return true;
        }
        catch (E exception)
        {
            return exception.Message != obj2.Message;;
        }
    }
    
    public bool Equals(T? other)
    {
        return other?.Equals(ValueInternal ?? new()) ?? false;
    }
}
