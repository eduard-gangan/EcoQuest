namespace EcoQuest;

    public class QuestSriLanka : Quest
{
    public QuestSriLanka(string description, int reputation, int requiredReputation) : base(description, reputation, requiredReputation)
    {

    }

    public override void start()
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

    public override void Finished()
    {
        Completed = true;
        Active = false;
        Console.WriteLine($"You have reached {RequiredReputation} reputation. Quest is completed and you can move on to Indonesia");
    }

    public override void getDescription()
    {
        Console.WriteLine(Description);
    }
}