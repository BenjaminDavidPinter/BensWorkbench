[TestFixture]
public class FileExtensionTests
{
    [SetUp]
    public void Setup()
    {
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
}