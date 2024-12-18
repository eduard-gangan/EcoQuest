namespace EcoQuest;

public class Question(string questionName, List<string> options, string answer)
{
    public string QuestionName { get; } = questionName;
    public List<string> Options { get; } = options;
    public string Answer { get; } = answer;
}