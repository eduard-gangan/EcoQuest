namespace EcoQuest;

public class Dialogue
{
    string? Intro;

    List<string> Replies;

    public void DisplayIntro()
    {
        System.Console.WriteLine(Intro);
    }

    public void DisplayReplies()
    {
        for (int i = 0; i < Replies.Count; i++)
        {
            System.Console.WriteLine($"{i + 1}. {Replies[i]}");
        }
    }
}