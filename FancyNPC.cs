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
            Game.ColorWriteLine($"You don't have enought reputation to talk to this NPC! You have ({Reputation.Get()} / {ReputationReq}) reputation.", ConsoleColor.Red);

        }
    }
}