using Spectre.Console;

namespace EcoQuest;

public static class QuestIndonesia
{
    public static string Description = "Find, tag and track all endangered species using the Life Form Analyzer Machine on the Submarine!";
    public static bool Active = false;
    public static bool Completed = false;


    public static void Start()
    {
        if (!Active && !Completed)
        {
            AnsiConsole.Markup($"[yellow]Quest started![/] \n {Description}");
            System.Console.WriteLine();
            Active = true;
        }
    }

    public static void Finished()
    {
        Completed = true;
        Active = false;
        AnsiConsole.Markup("[yellow]Quest finished![/] \nYou have found all endangered species and finished the quest.\nYou gained enough reputation to move onto the next location.");
        System.Console.WriteLine();
        Reputation.Add(20000);
    }


}
