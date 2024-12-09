namespace EcoQuest;

public static class QuestIndonesia
{
    public static bool Active = false;
    public static bool Completed = false;
    public static int RequiredCorrectAnswers = 7;
    
    public static void start()
    {
        Active = true;
    }

    public static bool CheckQuest(int correctAnswers)
    {
        if(correctAnswers >= RequiredCorrectAnswers)
        {
            Finished();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Finished()
    {
        Completed = true;
        Active = false;
        Console.WriteLine($"You had at least {RequiredCorrectAnswers} int quiz, quest is completed");
        Reputation.Add(20000);
    }

    public static void GetDescription()
    {
        Console.WriteLine($"get at least {RequiredCorrectAnswers} correct answers in quiz");
    }
}
