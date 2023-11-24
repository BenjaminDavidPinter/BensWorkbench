namespace BensWorkbench.Models.Tests;

public class ResultsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ValidResult()
    {
        var result = IsEligible(true) switch
        {
            var okRslt when okRslt.IsOK() => true,
            var errRslt when errRslt.IsErr() => false,
            _ => false
        };

        Assert.IsTrue(result);
    }

    [Test]
    public void InvalidResult()
    {
        var result = IsEligible(false) switch
        {
            var okRslt when okRslt.IsOK() => false,
            var errRslt when errRslt.IsErr() => true,
            _ => false
        };

        Assert.IsTrue(result);
    }

    public Result<bool, Exception> IsEligible(bool value)
    {
        if (value)
            return true;

        return new Exception("Invalid");
    }
}