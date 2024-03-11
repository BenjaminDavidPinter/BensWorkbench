namespace BensWorkbench.Services.Output;

public static class ColorConsole
{
    public static void WriteLine(string text,
    ConsoleColor foregroundColor = ConsoleColor.Gray,
    ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        var oldForegroundColor = Console.ForegroundColor;
        var oldBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;

        Console.WriteLine(text);

        Console.ForegroundColor = oldForegroundColor;
        Console.BackgroundColor = oldBackgroundColor;
    }

    public static void Write(string text,
    ConsoleColor foregroundColor = ConsoleColor.Gray,
    ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        var oldForegroundColor = Console.ForegroundColor;
        var oldBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;

        Console.Write(text);

        Console.ForegroundColor = oldForegroundColor;
        Console.BackgroundColor = oldBackgroundColor;
    }
}