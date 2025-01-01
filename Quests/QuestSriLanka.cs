using Spectre.Console;

namespace EcoQuest;

public static class QuestSriLanka
{
    public static string Description = "Help clean the beach and gain reputation while doing so.\n You need to gain 10000 reputation. Afterwards you can move on to the next location.";
    public static bool Active = false;
    public static bool Completed = false;
    public static int RequiredReputation = 10000;

    public static void start()
    {
        AnsiConsole.MarkupLine($"[yellow]Quest started![/] \n {Description}");
        Active = true;
    }

    public static void CheckQuest()
    {
        if (Reputation.Get() > RequiredReputation && Active)
        {
            Finished();
        }
    }

    public static void Finished()
    {
        Completed = true;
        Active = false;
        AnsiConsole.MarkupLine($"[yellow]You have reached {RequiredReputation} reputation. Quest is completed and you can move onto the next location or continue to help the locals clean the beach further. [/]");
    }


}