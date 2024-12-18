namespace EcoQuest;

public class Question(string questionName, List<string> choices, string answer)
{
    public string QuestionName { get; } = questionName;
    public List<string> Choices { get; } = choices;
    public string Answer { get; } = answer;
}