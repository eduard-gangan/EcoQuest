namespace EcoQuest;
public class NPC
{
    public List<Dialogue> Dialogues { get; private set; }
    public string Name { get; private set; }

    public string NpcDescription { get; private set; }

    public NPC(string name, string npcDescription, List<Dialogue> dialogues)
    {
        Name = name;
        NpcDescription = npcDescription;
        Dialogues = dialogues;
    }

    public void Talk(int dialogueNumber)
    {
        Dialogues[dialogueNumber].Start(NpcReply.GREETING);
    }
}
