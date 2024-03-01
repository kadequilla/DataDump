namespace DemoDataDump;

public static class Utils
{
    public static int ElapsedSeconds = 0;

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

    public static void TimerCallback(object state)
    {
        // Increment the elapsed seconds
        ElapsedSeconds++;

        // Get current cursor position
        var originalLeft = Console.CursorLeft;
        var originalTop = Console.CursorTop;

        // Update the current line
        Console.SetCursorPosition(0, originalTop);
        Console.Write($"Elapsed Time: {ElapsedSeconds} seconds");

        // Restore cursor position
        Console.SetCursorPosition(originalLeft, originalTop);
    }
}