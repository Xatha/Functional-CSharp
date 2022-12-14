using BenchmarkDotNet.Attributes;
using Functional_CSharp;
using Functional_CSharp.Extensions;

namespace Functional_CSharp_Benchmarks;

[MemoryDiagnoser]
public class Benchmark
{
    [Params(1000)] public int Iterations;
    
    //[Benchmark]
    public void UnwrapOption()
    {
        var option = Option.Some(1).Unwrap();
    }
    
    static int SumRecursive(int n, int acc) => n == 0 ? acc : SumRecursive(n - 1, acc + n);
    
    //[Benchmark]
    public void SumIterative()
    {
        var sum = 0;
        for (var i = 0; i < Iterations; i++)
        {
            sum += i;
        }
    }
    
    [Benchmark]
    public void SumRecursive()
    {
        var sum = SumRecursive(Iterations, 0);
    }
    
    
    //[Benchmark]
    public void SumOption() 
    {
        var sum = Option.Some(0).Bind(acc => SumRecursive(Iterations, acc));
    }
    
    //[Benchmark]
    public void SumNone() 
    {
        var sum = Option.None<int>().Bind(acc => SumRecursive(Iterations, acc));
    }
    
}