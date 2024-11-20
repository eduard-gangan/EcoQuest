namespace EcoQuest;

public static class QuestSriLanka
{
    public static string Description = "clean the beach";
    public static bool Active = false;
    public static bool Completed = false;
    public static int RequiredReputation = 100000;
    
    public static void start()
    {
        Active = true;
    }

    public static void CheckQuest()
    {
        if(Reputation.Get() > RequiredReputation)
        {
            Finished();
        }
    }

    public static void Finished()
    {
        Completed = true;
        Active = false;
        Console.WriteLine($"You have reached {RequiredReputation} reputation. Quest is completed and you can move on next location");
    }

    public static void GetDescription()
    {
        Console.WriteLine(Description);
    }

}