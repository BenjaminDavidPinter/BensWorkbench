[TestFixture]
public class OSFileTests
{
    private OSFile testFile = new();

    [SetUp]
    public void Setup()
    {
        //Setup a test file to work with
        File.WriteAllText("TestFile.txt", "Hellow World!");
    }

    [Test]
    public void TestInvalidRead()
    {
        var result = OSFile.FromDiskToBase64("Bunk filepath") switch
        {
            var errRslt when errRslt.IsErr<NotImplementedException>() => false,
            var errRslt when errRslt.IsErr<FileNotFoundException>() => true,
            var okRslt when okRslt.IsOK() => false,
            _ => false
        };

        Assert.IsTrue(result);
    }

    [Test]
    public void TestInvalidRead_DiffSyntax()
    {
        var result = OSFile.FromDiskToBase64("Bunk filepath");

        Assert.IsTrue(result.IsErr<FileNotFoundException>());
    }

    [Test]
    public void TestValidRead()
    {
        var result = OSFile.FromDiskToBase64("TestFile.txt") switch
        {
            var errRslt when errRslt.IsErr<Exception>() => false,
            var okRslt when okRslt.IsOK() => true,
            _ => false
        };

        Assert.IsTrue(result);
    }

    [Test]
    public void TestWrite_Full()
    {
        File.WriteAllText("1.txt", "Test");
        var result = OSFile.FromDiskToBase64("1.txt").Unwrap();
        _ = result.Save("./", "2.txt").Unwrap();
        var result3 = OSFile.FromDiskToBase64("2.txt").Unwrap();

        Assert.That(result3.Base64, Is.EqualTo(result.Base64));
    }
}
