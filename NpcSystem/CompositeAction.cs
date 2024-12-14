namespace EcoQuest;

class CompositeAction : CustomAction
{
    List<CustomAction> Actions;

    public CompositeAction(List<CustomAction> actions)
    {
        Actions = actions;
    }

    public void Invoke()
    {
        foreach (CustomAction action in Actions)
        {
            action.Invoke();
        }
    }

    public static CompositeAction from(List<CustomAction> actions)
    {
        return new CompositeAction(actions);
    }
}

