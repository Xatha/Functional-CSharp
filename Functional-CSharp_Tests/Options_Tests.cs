using System.Reflection.Metadata;
using Functional_CSharp;

namespace Functional_CSharp_Tests;

[TestFixture]
public class Options_Tests
{
    [Test]
    public void Option_With_Value_Is_Some()
    {
        var option = Option.Some(1);
        Assert.That(option.IsSome, Is.True);
    }
    
    [Test]
    public void Option_With_No_Value_Is_None()
    {
        var option = Option.None<int>();
        Assert.That(option.IsSome, Is.False);
    }
    
    [Test]
    public void Option_With_Value_Unwraps_Value()
    {
        var option = Option.Some(1);
        Assert.That(option.Unwrap(), Is.EqualTo(1));
    }
    
    [Test]
    public void Option_With_No_Value_Throws_Exception()
    {
        var option = Option.None<int>();
        Assert.Throws<InvalidOperationException>(() => option.Unwrap());
    }

    [Test]
    public void Option_With_Value_Binds_Function_Returns_Value()
    {
        var option = Option.Some(1);
        var result = option.Bind(x => x + 1);
        Assert.That(result.Unwrap(), Is.EqualTo(2));
    }
    
    [Test]
    public void Option_With_No_Value_Binds_Function_Returns_None()
    {
        var option = Option.None<int>();
        var result = option.Bind(x => x + 1);
        Assert.Throws<InvalidOperationException>(() => result.Unwrap());
    }
        
    [Test]
    public void Option_With_Value_Filters_Correctly()
    {
        var option = Option.Some(5).Filter(x => x > 4);

        Assert.That(option.Unwrap(), Is.EqualTo(5));
    }
    
    [Test]
    public void Option_With_No_Value_Filters_Correctly()
    {
        var option = Option.None<int>().Filter(x => x > 4);
        Assert.Throws<InvalidOperationException>(() => option.Unwrap());
    }
    
    [Test]
    public void Option_With_Value_UnwrapOr_Returns_Value()
    {
        var option = Option.Some(5);
        Assert.That(option.UnwrapOr(10), Is.EqualTo(5));
    }
    
    [Test]
    public void Option_With_No_Value_UnwrapOr_Returns_Fallback()
    {
        var option = Option.None<int>();
        Assert.That(option.UnwrapOr(10), Is.EqualTo(10));
    }
    
    [Test]
    public void Option_With_Value_Or_Returns_Option()
    {
        var optionFallback = Option.Some(10);
        var option = Option.Some(5);
        Assert.That(option.Or(optionFallback).Unwrap(), Is.EqualTo(5));
    }
    
    [Test]
    public void Option_With_No_Value_Or_Returns_OptionFallBack()
    {
        var optionFallback = Option.Some(10);
        var option = Option.None<int>();
        Assert.That(option.Or(optionFallback).Unwrap(), Is.EqualTo(10));
    }
    
    [Test]
    public void Options_With_Values_Combine()
    {
        var firstOption = Option.Some(10);
        var secondOption = Option.Some(20);
        var result = firstOption.Combine(secondOption, (x, y) => Option.Some(x + y));
        Assert.That(result.Unwrap(), Is.EqualTo(30));
    }
    
    [Test] 
    public void Options_With_No_Values_Return_None()
    {
        var firstOption = Option.None<int>();
        var secondOption = Option.None<int>();
        var result = firstOption.Combine(secondOption, (x, y) => Option.Some(x + y));
        Assert.That(result.IsSome, Is.False);
    }
    
    [Test] 
    public void Options_With_Same_Value_Equals_True()
    {
        var firstOption = Option.Some(10);
        var secondOption = Option.Some(10);
        Assert.That(firstOption == secondOption, Is.True);
        Assert.That(firstOption != secondOption, Is.False);
    }
    
    [Test] 
    public void Options_With_Not_Same_Value_Equals_False()
    {
        var firstOption = Option.Some(10);
        var secondOption = Option.Some(15);
        Assert.That(firstOption == secondOption, Is.False);
        Assert.That(firstOption != secondOption, Is.True);
    }


}