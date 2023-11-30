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
            var errRslt when errRslt.IsErr<Exception>() => false,
            var okRslt when okRslt.IsOK() => true,
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
            var errRslt when errRslt.IsErr<Exception>() => true,
            _ => false
        };

        Assert.IsTrue(result);
    }

    public Result<bool> IsEligible(bool value)
    {
        if (value)
            return true;

        return new Exception("Invalid");
    }
}