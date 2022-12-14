using Functional_CSharp;
using Functional_CSharp.Extensions;
using static Functional_CSharp.Option;

//Just some testing stuff.

var list = new List<Option<int>>()
{
    Some(1),
    Some(2),
    Some(3),
    Some(4),
    Some(5),
    Some(6),
    Some(7),
    Some(8),
    Some(9),
    Some(10)
};

Option<string> Add(string x, int y) => Some(x + y);

list = list.Map(x => x.Filter(e => e % 2 != 1)).FindAll(x=> x.IsSome);
list.ForEach(x => Console.WriteLine(x.UnwrapOr(-1)));


var numbersToString = list.Fold(Some(""), (s, option) => 
    s.Combine(option, Add).Bind(x=> x + " "));

Console.WriteLine(numbersToString.UnwrapOr("Failed"));




//var numbersToString = list.Fold("", (s, option) => s + option.Unwrap());


//var evenNumbers = list.Map(e => e.Filter(i => i % 2 == 0)).FindAll(e => e.IsSome);
//evenNumbers.ForEach(e=> Console.WriteLine(e.Unwrap()));

//var helloWorld = hello.Bind(x => world.Bind(y => x + y));

//Console.WriteLine(helloWorld.Unwrap());


//BenchmarkRunner.Run<Benchmark>();
