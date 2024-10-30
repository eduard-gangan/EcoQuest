//This will be changed to a dialogue class

namespace EcoQuest;
public class NPC
{
    private List<Option> Options;
    public NPC(List<Option> options)
    {
        Options = options;
    }

    public void Talk()
    {
        System.Console.WriteLine(NpcReply.GREETING);

        var isTalkingTo = true;
        while (isTalkingTo)
        {
            for (int i = 0; i < Options.Count(); i++)
            {
                System.Console.WriteLine($"{i + 1}. {Options[i].Prompt}");
            }

            //Takes input converts index into Options array
            int input = Int32.Parse(Console.ReadLine()) - 1;
            var chosenOption = Options[input];
            if (chosenOption.Reply != null)
            {
                System.Console.WriteLine(chosenOption.Reply);
            }

            if (chosenOption.Type == OptionType.CLOSE_DIALOGUE)
            {
                isTalkingTo = false;
            }
            if (chosenOption.Type == OptionType.ADD_INVENTORY_ITEM)
            {
                // Inventory.Add(item);
            }

        }
    }
}