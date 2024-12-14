namespace EcoQuest;

class RemoveOptionAction : CustomAction
{
    private string OptionName;
    private Dialogue Dialogue;

    public RemoveOptionAction(Dialogue dialogue, string optionName)
    {
        Dialogue = dialogue;
        OptionName = optionName;
    }

    public void Invoke()
    {
        Dialogue.RemoveOption(PlayerReply.GARRY_QUEST);
    }
}