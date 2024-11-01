/*
An Option is a class that contains a Prompt, a Reply and a type.
The Prompt is what the player asks the NPC, the Reply is what the NPC's replies.
Depending on the type, the option may call a method when the player chooses it or do something else like start a new Dialogue.

*/
namespace EcoQuest;

public enum OptionType
{
    CLOSE_DIALOGUE,

    ADD_INVENTORY_ITEM,

    START_NEW_DIALOGUE,
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