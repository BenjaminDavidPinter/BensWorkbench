public class MissingFileDataException : Exception
{
    public OSFile FileToWrite { get; set; }

    public MissingFileDataException(OSFile attemptedFile)
    {
        FileToWrite = attemptedFile;
    }
}