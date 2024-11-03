namespace EcoQuest;

public class Quest
{
    public string Description { get; private set; }
    public bool Active { get; set; }
    public bool Completed { get; set; }
    public int Reputation { get; private set; }
    public int RequiredReputation { get; private set; }
    public Quest(string description, int reputation, int requiredReputation)
    {
        Description = description;
        Active = false;
        Completed = false;
        Reputation = reputation;
        RequiredReputation = requiredReputation;
    }

    public virtual void start()
    {

    }

    public virtual void Finished()
    {

    }
    
    public virtual void getDescription()
    {

    }
    
}