namespace EcoQuest;

public class Quest
{
    public string Description { get; private set; }
    public bool Active { get; private set; }
    public bool Completed { get; private set; }
    public Reputation Reputation { get; private set; }
    public int RequiredReputation { get; private set; }
    public Quest(string description, Reputation reputation, int requiredReputation)
    {
        Description = description;
        Active = false;
        Completed = false;
        Reputation = reputation;
        RequiredReputation = requiredReputation;
    }

    public abstract void start();

    public abstract void Finished();
    
    public abstract void getDescription();
    
}