namespace EcoQuest;

public class NPC
{

    public string Name { get; set; }
    public string Greeting { get; set; }
    public Dialogue MainDialogue { get; set; }


    public NPC(string name, string greeting)
    {
        Name = name;
        Greeting = greeting;
        MainDialogue = new Dialogue(Greeting);
    }

    public void ChangeGreeting(string newGreeting)
    {
        Greeting = newGreeting;
        MainDialogue.Prompt = newGreeting;
    }

    public virtual void Talk()
    {
        MainDialogue.Start();
    }


}
