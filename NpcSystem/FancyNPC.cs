using Spectre.Console;

namespace EcoQuest;

public class FancyNPC : NPC
{
    public int ReputationReq { get; private set; }
    public FancyNPC(string name, string greeting, int reputationReq) : base(name, greeting)
    {
        Name = name;
        Greeting = greeting;
        ReputationReq = reputationReq;
        MainDialogue = new Dialogue(Greeting);
    }
    public override void Talk()
    {
        if (Reputation.Get() >= ReputationReq)
        {
            MainDialogue.Start();

        }
        else
        {
            AnsiConsole.MarkupLine($"[bold red]You don't have enough reputation to talk to this NPC! You have ({Reputation.Get()} / {ReputationReq}) reputation.[/]");
        }
    }
}