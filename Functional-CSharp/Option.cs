namespace Functional_CSharp;

public readonly struct Option<T>
{
    private readonly T _value;

    public bool IsSome { get; }

    private Option(T value)
    {
        _value = value;
        IsSome = true;
    }
    
    private Option(bool hasValue)
    {
        _value = default!;
        IsSome = hasValue;
    }
    
    public static Option<T> None = new(false);

    public static Option<T> Some(T value) => new(value);

    public Option<T> Bind(Func<T, T> func) => IsSome ? Some(func(_value)) : None;
    
    public T Unwrap() => IsSome ? _value : throw new InvalidOperationException("Unwrapping None");
}