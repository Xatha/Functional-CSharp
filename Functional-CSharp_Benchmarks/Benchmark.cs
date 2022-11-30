using BenchmarkDotNet.Attributes;
using Functional_CSharp;

namespace Functional_CSharp_Benchmarks;

[MemoryDiagnoser]
public class Benchmark
{
    [Params(1000, 10000, 100000)] public int Iterations;
    
    [Benchmark]
    public void UnwrapOption()
    {
        var option = Option<int>.Some(1).Unwrap();
    }
    
    static int SumRecursive(int n, int acc) => n == 0 ? acc : SumRecursive(n - 1, acc + n);

    [Benchmark]
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

    [Benchmark]
    public void SumOption() 
    {
        var sum = Option<int>.Some(0).Bind(acc => SumRecursive(Iterations, acc));
    }
    
    [Benchmark]
    public void SumNone() 
    {
        var sum = Option<int>.None.Bind(acc => SumRecursive(Iterations, acc));
    }
    
}