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
}