namespace EcoQuest;

public static class QuestIndonesia
{
    public static bool Active = false;
    public static bool Completed = false;


    public static void start()
    {
        Active = true;
    }

    public static void Finished()
    {
        Completed = true;
        Active = false;
        Console.WriteLine($"You have finished the quest.");
        Reputation.Add(20000);
    }

    public static void GetDescription()
    {
        Console.WriteLine($"Find all endangered species in order to finish this quest.");
    }
}
