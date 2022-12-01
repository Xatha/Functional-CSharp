namespace Functional_CSharp;

public readonly struct Option
{
    public static Option<T> None<T>() => new(false);
    public static Option<T> None<T>(T type) => new(false);
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
    
    //public static Option<T> None = new(false);

    //public static Option<T> Some<T>(T value) => new(value);
    
    //But I feel this is the most proper and ergonomic.
    public Option<U> Bind<U>(Func<T, U> func) => IsSome ? Option.Some(func(_value)) : Option.None<U>();
    
    //Makes chaining of lambda arguments easier.
    public Option<T> Bind(Func<T, Option<T>> func) => IsSome ? func(_value) : Option.None<T>();

    public Option<T> Filter(Func<T, bool> predicate) => IsSome && predicate(_value) ? this : Option.None<T>();

    public T Unwrap() => IsSome ? _value : throw new InvalidOperationException("Unwrapping None");
    public T UnwrapOr(T fallBackValue) => IsSome ? _value : fallBackValue;

    //Applies a function between two option types.

    //Combine two different options with static method.
    public Option<T> Combine<U>(Option<U> other, Func<T, U, Option<T>> combiner)
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
}