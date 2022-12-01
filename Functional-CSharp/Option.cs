namespace Functional_CSharp;

public readonly struct Option
{
    public static Option<T> None<T>() => new(false);
    public static Option<T> Some<T>(T value) => new(value);
}

public readonly struct Option<T>
{
    private readonly T _value;

    public bool IsSome { get; }

    internal Option(T value)
    {
        _value = value;
        IsSome = true;
    }
    
    internal Option(bool hasValue)
    {
        _value = default!;
        IsSome = hasValue;
    }

    //But I feel this is the most proper and ergonomic.
    public Option<TU> Bind<TU>(Func<T, TU> func) => IsSome ? Option.Some(func(_value)) : Option.None<TU>();
    
    //Makes chaining of lambda arguments easier.
    public Option<T> Bind(Func<T, Option<T>> func) => IsSome ? func(_value) : Option.None<T>();

    public Option<T> Filter(Func<T, bool> predicate) => IsSome && predicate(_value) ? this : Option.None<T>();

    public T Unwrap() => IsSome ? _value : throw new InvalidOperationException("Unwrapping None");
    public T UnwrapOr(T fallBackValue) => IsSome ? _value : fallBackValue;
    public Option<T> Or(Option<T> other) => IsSome ? this : other;
    
    public Option<T> Combine<TU>(Option<TU> other, Func<T, TU, Option<T>> combiner)
    {
        if (!IsSome || !other.IsSome) return Option.None<T>();
        return combiner(Unwrap(), other.Unwrap());
    }

    public static bool operator ==(Option<T> first, Option<T> second)
    {
        if (!first.IsSome || !second.IsSome) return false;
        return EqualityComparer<T>.Default.Equals(first.Unwrap(), second.Unwrap());
    }

    public static bool operator !=(Option<T> first, Option<T> second)
    {
        return !(first == second);
    }
    
    public bool Equals(Option<T> other)
    {
        return EqualityComparer<T>.Default.Equals(_value, other._value) && IsSome == other.IsSome;
    }

    public override bool Equals(object obj)
    {
        return obj is Option<T> other && Equals(other);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(_value, IsSome);
    }
}