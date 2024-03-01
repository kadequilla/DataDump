namespace DemoDataDump;

public static class Utils
{
    public static void Println(ConsoleColor consoleColor, dynamic text)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Println(ConsoleColor bgColor, ConsoleColor fgColor, dynamic text)
    {
        Console.ForegroundColor = fgColor;
        Console.BackgroundColor = bgColor;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}