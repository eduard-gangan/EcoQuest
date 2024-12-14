namespace EcoQuest;

class SlowWriteAction : CustomAction
{
    private string Text;

    public SlowWriteAction(string text)
    {
        Text = text;
    }

    public void Invoke()
    {
        Game.SlowWrite(Text);
    }

    public static SlowWriteAction say(string text)
    {
        return new SlowWriteAction(text);
    }
}