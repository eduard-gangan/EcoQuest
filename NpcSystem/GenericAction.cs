namespace EcoQuest;


public class GenericAction : CustomAction
{
    private readonly Action Action;

    public GenericAction(Action action)
    {
        Action = action;
    }

    public void Invoke()
    {
        Action.Invoke();
    }
}