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
          var a when a == true => HandleSuccess(),
          var e when e == new Exception("Invalid") => HandleError(),
          var ex when ex == new Exception("Some other error") => HandleErrorDifferently(),
          _ => HandleDefaultCase()
        };
    }
    
    [Test]
    public void InvalidResult()
    {
        var operationResult = IsEligible(false);
        if(operationResult == new Exception("Invalid"))
        {
            Assert.Pass();
        }
        
        Assert.Fail();
    }
    
    public Result<bool, Exception> IsEligible(bool value)
    {
        if(value)
            return true;
        
        return new Exception("Invalid");
    }
    
    public bool HandleError() {return true;}
    public bool HandleErrorDifferently() {return true;}
    public bool HandleDefaultCase() {return true;}
    public bool HandleSuccess() {return true;}
}