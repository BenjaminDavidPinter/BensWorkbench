using System.Text;
using BensWorkbench.Models;

public class OSFile
{
    /// <summary>
    /// Load an OSFile from a base64 string
    /// </summary>
    /// <param name="Base64">File content as Base64</param>
    /// <returns></returns>
    public static Result<OSFile> FromBase64String(string Base64)
    {
        try
        {
            return new OSFile()
            {
                FileData = Convert.FromBase64String(Base64),
                Base64 = Base64
            };
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Result<OSFile> FromBytes(byte[] fileData)
    {
        try
        {
            return new OSFile()
            {
                FileData = fileData,
                Base64 = Convert.ToBase64String(fileData)
            };
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Result<OSFile> FromDisk(string filePath)
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
            return WriteFileFromBase64Internal(filePath, fileName);
        }
        else if (FileData is not null)
        {
            return WriteFileFromBytesInternal(filePath, fileName);
        }

        return new SystemException("Something went wrong while writing the file");
    }

    public static Result<OSFile> Save(string filePath, string fileName, string fileContents)
    {
        return WriteFileFromBytesExternal(filePath, fileName, Encoding.ASCII.GetBytes(fileContents));
    }

    public static Result<OSFile> SaveBase64(string filePath, string fileName, string base64)
    {
        return WriteFileFromBytesExternal(filePath, fileName, Encoding.ASCII.GetBytes(base64));
    }

    private Result<OSFile> WriteFileFromBase64Internal(string filePath, string fileName)
    {
        if (string.IsNullOrEmpty(Base64)) return new InvalidDataException("Base64 is null");
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

    private Result<OSFile> WriteFileFromBase64External(string filePath, string fileName, string base64)
    {
        return WriteFileFromBytesExternal(filePath, fileName, Convert.FromBase64String(base64));
    }

    private Result<OSFile> WriteFileFromBytesInternal(string filePath, string fileName)
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

    private static Result<OSFile> WriteFileFromBytesExternal(string filePath, string fileName, byte[] data)
    {
        var path = Path.Combine(filePath, fileName);

        //This is only called after a guard on 'FileData', so I'm telling the compiler it's okay
        File.WriteAllBytes(Path.Combine(filePath, fileName), data!);

        var returnFile = new OSFile()
        {
            FileLocation = path,
            FileData = data
        };

        return returnFile;
    }

    public string? FileLocation { get; set; }
    public byte[]? FileData { get; set; }
    public int FileSize => FileData?.Count() ?? 0;
    public string? Base64 { get; set; }
}