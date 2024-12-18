namespace EcoQuest;

public class Book(string title, string contents)
{
    public string Title { get; } = title;
    public string Contents { get; } = contents;
}