using System.Drawing;
using BenchmarkDotNet.Running;
using Functional_CSharp;
using Functional_CSharp.Extensions;
using Functional_CSharp_Benchmarks;

//Just some testing stuff.

var list = new List<Option<int>>()
{
    Option<int>.Some(1),
    Option<int>.Some(2),
    Option<int>.Some(3),
    Option<int>.Some(4),
    Option<int>.Some(5),
    Option<int>.Some(6),
    Option<int>.Some(7),
    Option<int>.Some(8),
    Option<int>.Some(9),
    Option<int>.Some(10)
};

var initialState = Option<string>.Some("");

Option<string> Add(string x, int y) => Option<string>.Some(x + y);

var numbersToString = list.Fold(initialState, (s, option) => s.Combine(Add, option).Bind(x=>x + " || "));

Console.WriteLine(numbersToString.UnwrapOr("Failed"));




//var numbersToString = list.Fold("", (s, option) => s + option.Unwrap());


//var evenNumbers = list.Map(e => e.Filter(i => i % 2 == 0)).FindAll(e => e.IsSome);
//evenNumbers.ForEach(e=> Console.WriteLine(e.Unwrap()));

//var helloWorld = hello.Bind(x => world.Bind(y => x + y));

//Console.WriteLine(helloWorld.Unwrap());


//BenchmarkRunner.Run<Benchmark>();
