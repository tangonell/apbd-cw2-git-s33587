namespace APBD02.Result;

// Rust-like Result type

public abstract record Result<T, E>
{
    public sealed record Ok(T Value) : Result<T, E>;
    public sealed record Err(E Error) : Result<T, E>;

    public static implicit operator Result<T, E>(T value) => new Ok(value);
    public static implicit operator Result<T, E>(E error) => new Err(error);

    public T Expect(string msg) => this switch
    {
        Ok ok => ok.Value,
        Err err => throw new InvalidOperationException($"{msg}: {err.Error}"),
        _ => throw new InvalidOperationException("Unreachable")
    };
    
    public T Unwrap() => this switch
    {
        Ok ok => ok.Value,
        Err err => throw new InvalidOperationException($"called `Result::unwrap() on an `Err` value`: {err.Error}"),
        _ => throw new InvalidOperationException("Unreachable")
    };
    
    public T UnwrapOr(T defaultValue) => this switch
    {
        Ok ok => ok.Value,
        Err _ => defaultValue,
        _ => throw new InvalidOperationException("Unreachable")
    };
}

public abstract record Result<E>
{
    public sealed record Ok : Result<E>;
    public sealed record Err(E Error) : Result<E>;
    
    public static implicit operator Result<E>(E error) => new Err(error);
}