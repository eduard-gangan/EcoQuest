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

    public void start()
    {
        Active = true;
        while (Active == true)
        {
            if(Reputation <= RequiredReputation)
            {
                this.start();
            }
            else
            {
                this.Finished();
            }
        }
    }

    public void Finished()
    {
        Completed = true;
        Active = false;
        Console.WriteLine($"You have reached {RequiredReputation} reputation. Quest is completed and you can move on to Indonesia");
    }

    public void getDescription()
    {
        Console.WriteLine(Description);
    }

}