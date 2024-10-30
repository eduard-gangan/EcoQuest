namespace EcoQuest;
public enum OptionType
{
    CLOSE_DIALOGUE,

    ADD_INVENTORY_ITEM,
    DEFAULT
}
public class Option
{
    public readonly string Prompt;
    public readonly string Reply;
    public readonly OptionType Type = OptionType.DEFAULT;

    public Item AddedItem;

    public Option(string prompt, string reply)
    {
        Prompt = prompt;
        Reply = reply;
    }
    public Option(string prompt, string reply, OptionType type)
    {
        Prompt = prompt;
        Reply = reply;
        Type = type;
    }

    public Option(string prompt, string reply, Item item)
    {
        Prompt = prompt;
        Reply = reply;

        Type = OptionType.ADD_INVENTORY_ITEM;
    }

}