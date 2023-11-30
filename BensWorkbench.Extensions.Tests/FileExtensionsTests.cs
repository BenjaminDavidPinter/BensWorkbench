[TestFixture]
public class FileExtensionTests
{
    [SetUp]
    public void Setup()
    {
        File.WriteAllText("TestFile.txt", "Hellow World!");
    }

    [Test]
    public void TestInvalidRead()
    {
        var result = FileExtensions.ReadToBase64("Bunk filepath") switch
        {
            var errRslt when errRslt.IsErr<NotImplementedException>() => false,
            var errRslt when errRslt.IsErr<FileNotFoundException>() => true,
            var okRslt when okRslt.IsOK() => false,
            _ => false
        };

        Assert.IsTrue(result);
    }

    [Test]
    public void TestValidRead()
    {
        var result = FileExtensions.ReadToBase64("TestFile.txt") switch
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
        var result = FileExtensions.ReadToBase64("1.txt").Unwrap();
        var result2 = FileExtensions.WriteBase64AsFile(result, "./", "2.txt").Unwrap();
        var result3 = FileExtensions.ReadToBase64("2.txt").Unwrap();

        Assert.That(result3, Is.EqualTo(result));
    }
}
