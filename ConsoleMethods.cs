namespace EcoQuest;
using Spectre.Console;

public static class ConsoleMethods
{
    public static void SlowWrite(string text, int min = 25, int max = 35)
    {
        Random rnd = new Random();
        bool skipDelay = false;
        foreach (char c in text)
        {
            Console.Write(c);

            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                skipDelay = true;
            }
            if (!skipDelay)
            {
                Thread.Sleep(rnd.Next(min, max));
            }
        }
    }
    public static void SlowWriteLine(string text, int min = 25, int max = 35)
    {
        Random rnd = new Random();
        bool skipDelay = false;
        foreach (char c in text)
        {
            Console.Write(c);

            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                skipDelay = true;
            }
            if (!skipDelay)
            {
                Thread.Sleep(rnd.Next(min, max));
            }
        }
        Console.WriteLine();
    }
    public static void FigletSlowWrite(string text, Color color)
    {
        foreach (char c in text)
        {
            AnsiConsole.Write(
                     new FigletText(c.ToString())
                     .Color(color));
            Thread.Sleep(100);
        }
    }


    public static void RecursiveWrite(string text, int loops)
    {
        for (int i = 0; i < loops; i++)
        {
            SlowWrite(text, 150, 250);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}