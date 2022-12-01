using System.Runtime.InteropServices;

namespace Functional_CSharp.Extensions;

public static class List
{
    public static List<TU> Map<T, TU>(this List<T> source, Func<T, TU> mapper)
    {
        var result = new List<TU>(source.Count);
        var listSpan = CollectionsMarshal.AsSpan(source);
        
        for (var i = 0; i < listSpan.Length; i++)
        {
            result.Add(mapper(listSpan[i]));
        }

        return result;
    }
    
    public static TState Fold<T, TState>(this List<T> source, TState initialState, Func<TState, T, TState> folder)
    {
        var listSpan = CollectionsMarshal.AsSpan(source);
        var result = initialState;
        
        for (var i = 0; i < listSpan.Length; i++)
        {
            result = folder(result, listSpan[i]);
        }

        return result;
    }
}