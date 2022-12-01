namespace Functional_CSharp.Extensions;

public static class Generic
{
    public static Option<T> ToOption<T>(this T value) => Option<T>.Some(value);
}