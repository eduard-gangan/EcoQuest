namespace EcoQuest;
using Spectre.Console;

public static class Program
{
    public static Game game = new();
    public static void Main()
    {
        bool startMenu = true;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"
                 _____          _____                 _   
                |  ___|        |  _  |               | |  
                | |__  ___ ___ | | | |_   _  ___  ___| |_ 
                |  __|/ __/ _ \| | | | | | |/ _ \/ __| __|
                | |__| (_| (_) \ \/' / |_| |  __/\__ \ |_ 
                \____/\___\___/ \_/\_\\__,_|\___||___/\__|");
        Console.ResetColor();
        Console.WriteLine("\n");
        AnsiConsole.MarkupLine("[bold yellow]WARNING: For a better command-line experience, please use an external CLI instead of an integrated one[/]");
        AnsiConsole.MarkupLine("[grey37]Press any key to continue...[/]");
        while (startMenu)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                startMenu = false;
                game.Play();


            }

        }
    }
}