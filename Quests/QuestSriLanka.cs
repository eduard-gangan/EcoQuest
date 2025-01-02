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
        Changes();
    }

    public static void Changes()
    {
        NPCs.Garry.ChangeGreeting("Hey, you, again. Thank you for everything we've done, I knew I could trust you.");
        NPCs.Garry.MainDialogue.OptionList = [];
        NPCs.Garry.MainDialogue.DialogueOptions = [];
        NPCs.Garry.MainDialogue.AddOption(PlayerReply.BYE, () => { ConsoleMethods.SlowWriteLine(">GARRY: Bye, you've done enough for us, you should go find other places in need of your help... "); NPCs.Garry.MainDialogue.TriggerDialogue(); Game.DisplayRoomInformation(); });
    }
}