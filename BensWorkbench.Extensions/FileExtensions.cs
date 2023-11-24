using BensWorkbench.Models;

namespace BensWorkbench.Extensions;

public static class FileExtensions
{
    public static Result<string, Exception> ReadToBase64(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return new FileNotFoundException($"{filePath}");

            return Convert.ToBase64String(File.ReadAllBytes(filePath));
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Result<int, Exception> WriteBase64AsFile(string base64, string filePath, string fileName)
    {
        try
        {
            if (!Directory.Exists(filePath))
                return new FileNotFoundException($"{filePath}");

            var totalBytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(Path.Combine(filePath, fileName), totalBytes);

            return totalBytes.Count();
        }
        catch (Exception e)
        {
            return e;
        }
    }
}