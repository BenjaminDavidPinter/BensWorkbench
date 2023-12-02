using BensWorkbench.Models;

public class OSFile
{
    public static Result<OSFile> FromBase64String(string Base64)
    {
        try
        {
            return new OSFile()
            {
                FileData = Convert.FromBase64String(Base64)
            };
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Result<OSFile> FromBytes(string Base64)
    {
        try
        {
            return new OSFile()
            {
                FileData = Convert.FromBase64String(Base64)
            };
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Result<OSFile> FromDiskToBase64(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return new FileNotFoundException($"{filePath}");

            return new OSFile()
            {
                FileData = File.ReadAllBytes(filePath),
                FileLocation = filePath
            };
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public Result<OSFile> Save(string filePath, string fileName)
    {
        if (!Directory.Exists(filePath))
            return new FileNotFoundException($"{filePath}");

        if (FileData is null && Base64 is null)
            return new MissingFileDataException(this);

        if (Base64 is not null)
        {
            return WriteFileFromBase64(filePath, fileName);
        }
        else if (FileData is not null)
        {
            return WriteFileFromBytes(filePath, fileName);
        }

        return new SystemException("Something went wrong while writing the file");
    }

    private Result<OSFile> WriteFileFromBase64(string filePath, string fileName)
    {
        var fileBytes = Convert.FromBase64String(Base64);
        var path = Path.Combine(filePath, fileName);

        File.WriteAllBytes(Path.Combine(filePath, fileName), fileBytes);

        var returnFile = new OSFile()
        {
            FileLocation = path,
            FileData = fileBytes
        };

        return returnFile;
    }

    private Result<OSFile> WriteFileFromBytes(string filePath, string fileName)
    {
        var path = Path.Combine(filePath, fileName);

        //This is only called after a guard on 'FileData', so I'm telling the compiler it's okay
        File.WriteAllBytes(Path.Combine(filePath, fileName), FileData!);

        var returnFile = new OSFile()
        {
            FileLocation = path,
            FileData = FileData
        };

        return returnFile;
    }

    public string? FileLocation { get; set; }
    public byte[]? FileData { get; set; }
    public int FileSize => FileData?.Count() ?? 0;

    //TODO: This will currently throw an exception when accessed and FileData is not set. Rework this to 
    //      be safer.
    public string Base64 => Convert.ToBase64String(FileData ?? Array.Empty<byte>());
}