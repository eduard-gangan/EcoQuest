namespace EcoQuest;

public class Option
{
    public readonly string Name;
    public readonly Action Action;

    public Option(string name, Action action)
    {
        Name = name;
        Action = action;
    }

    public Option andThen(Action anotherAction)
    {
        return new Option(Name, () =>
        {
            Action.Invoke();
            anotherAction.Invoke();
        });
    }
}