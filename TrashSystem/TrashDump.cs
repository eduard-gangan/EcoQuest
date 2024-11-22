/*
A basic trash-dumping system, usable only in the recycling station room, iterates through
all trash items in the player's inventory, awarding a small amount of reputation based on
each item's value.

- Use the Dump() method to dispose of all trash items in exchange for reputation.

 */


namespace EcoQuest;
public static class TrashDump
{
    private static List<Item> Trash = [];
    public static void Dump()
    {
        if (Energy.Decrease(5) == false)
        {
            Game.ColorWriteLine("You don't have enough energy to dump your trash!", ConsoleColor.Red);
            return;
        }
        
        if (Inventory.Items.Count > 0)
        {
            foreach (Item item in Inventory.Items.ToList())
            {
                if (item.Trash == true)
                {
                    Inventory.DropItem(item);
                    Trash.Add(item);
                }
            }
        }

        if (Trash.Count > 0)
        {
            foreach (Item item in Trash.ToList())
            {
                Reputation.Add(item.Value);
                Trash.Remove(item);
                Console.Write($"Threw away {item.Name}, gained ");
                Game.ColorWrite($"{item.Value}", ConsoleColor.Green);
                Console.WriteLine(" reputation!");
            }
        }
        else
        {
            Game.ColorWriteLine("You don't have any trash in your inventory!", ConsoleColor.Red);
        }
    }
}