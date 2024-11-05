
/*

Dialogue Class contains a list of Options
Start() prints out the npc greeting, displays all of the options, takes input from the players then it checks the type(This is how you cause things to happen based on the player's input)

Dialogue dialogue = new(List<Option> options)
Use dialogue.Start() to start a Dialogue with the Npc

All strings used in the Dialogue must be saved in a separate class as constants. Ex: 
public static class MayorReply{
    public const string HELLO = "Hey there young man!";
}
*/
namespace EcoQuest;

public class Dialogue
{
    private List<Option> Options;
    public Dialogue(List<Option> options)
    {
        Options = options;
    }

    public void Start(string dialogueStart)
    {
        string DialogueStart = dialogueStart;
        System.Console.WriteLine(DialogueStart);

        var isTalkingTo = true;
        while (isTalkingTo)
        {
            for (int i = 0; i < Options.Count(); i++)
            {
                System.Console.WriteLine($"{i + 1}. {Options[i].Prompt}");
            }
            System.Console.WriteLine();

            //Takes input converts index into Options array
            System.Console.Write(">");
            int input = Int32.Parse(Console.ReadLine()) - 1;
            System.Console.WriteLine();
            var chosenOption = Options[input];
            if (chosenOption.Reply != null)
            {
                System.Console.WriteLine(chosenOption.Reply);
                System.Console.WriteLine();
            }

            if (chosenOption.Type == OptionType.CLOSE_DIALOGUE)
            {
                isTalkingTo = false;
            }
            if (chosenOption.Type == OptionType.ADD_INVENTORY_ITEM)
            {
                // Inventory.Add(item);
            }
            if (chosenOption.Type == OptionType.GARY_ADD_OPTION)
            {
                Options.Insert(Options.Count() - 1, new Option(PlayerReply.GARY_START_QUEST, NpcReply.GARY_QUEST, OptionType.GARY_QUEST));
            }
            if (chosenOption.Type == OptionType.GARY_QUEST)
            {
                isTalkingTo = false;
                Inventory.InventoryCapacity = 5;
                Game.ColorWriteLine("\n(You received a trash bag! Your inventory space has been increased to 5)", ConsoleColor.Green);


                System.Console.WriteLine();
            }

        }
    }
}