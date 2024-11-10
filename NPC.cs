namespace EcoQuest;
public class NPC
{
    public List<Dialogue> Dialogues { get; private set; }
    public string Name { get; private set; }

    private string Greeting;


    public NPC(string name, string greeting, List<Dialogue> dialogues)
    {
        Name = name;
        Dialogues = dialogues;
        Greeting = greeting;
    }

    public void Talk(int dialogueNumber)
    {
        Dialogues[dialogueNumber].Start(Greeting);
    }
}
