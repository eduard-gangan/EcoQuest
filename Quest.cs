namespace EcoQuest;

public class Quest
{
    public string Description { get; private set; }
    public bool Active { get; set; }
    public bool Completed { get; set; }
    public int RequiredReputation { get; private set; }
    public Quest(string description, int requiredReputation)
    {
        Description = description;
        Active = false;
        Completed = false;
        RequiredReputation = requiredReputation;
    }

    public void start()
    {
        Active = true;
        while (Active == true)
        {
            if(Reputation.Get() <= RequiredReputation)
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
        Console.WriteLine($"You have reached {RequiredReputation} reputation. Quest is completed and you can move on next location");
    }

    public void GetDescription()
    {
        Console.WriteLine(Description);
    }

}