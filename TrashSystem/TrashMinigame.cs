/*
--/ Class for the Trash Sorting Minigame \--

Overview:
 In this minigame, players are presented with various trash items 
 (e.g., "plastic bottle," "banana peel," "glass jar") and must sort 
 each item into the correct recycling bin category (e.g., organic, plastic, 
 metal, glass, electronics, rubber, paper, or waste). Players earn reputation 
 points for correct sorting and lose reputation points for incorrect sorting.

 - Use the Start() method to initiate the sorting minigame.

Key Variables:
 - Multiplier: Determines how much the reputation points are multiplied by. 
    This can be adjusted, for example, as part of an upgrade.

 - EnergyConsumption: Represents the amount of energy consumed during sorting. 
    This can be adjusted, for example, as part of an upgrade.

 - ReputationRequirement: Represents the amount of reputation required to access the 'sort' command. 
    This can't be adjusted, since there is no need.

 */

using System.Runtime.InteropServices;
using Spectre.Console;


namespace EcoQuest;
public static class TrashMinigame
{
    private static List<Item> Trash = [];
    public static int Multiplier { get; set; } = 5;
    public static int EnergyConsumption { get; set; } = 5;
    private static bool Open = false;

    public static void Start(Room currentRoom)
    {
        // Check energy, room and if the recycling station is open.
        if (currentRoom.RoomName != "Recycling Station")
        {
            Game.ColorWriteLine("You are not in the recycling station!", ConsoleColor.Red);
            return;
        }
        if (!Open)
        {
            Game.ColorWriteLine("The Recycling Station isn't functional yet.", ConsoleColor.Red);
            return;
        }
        if (Energy.Decrease(EnergyConsumption) == false)
        {
            Game.ColorWriteLine("You don't have enough energy to sort this trash!", ConsoleColor.Red);
            return;
        }

        // Iterate through all items and add items that are trash to the Trash list.
        if (Inventory.Items.Count > 0)
        {
            foreach (Item item in Inventory.Items.ToList())
            {
                if (Trash.Count == 5)
                {
                    break;
                }
                if (item.Trash == true)
                {
                    Trash.Add(item);
                }
            }
        }

        if (Trash.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine(">-------------------------------<");
            Console.WriteLine("      Trash Sorting Minigame");
            Console.WriteLine(">-------------------------------<");
            Console.WriteLine("      Select the correct bin");
            Console.WriteLine("Gain reputation by selecting the");
            Console.WriteLine("correct bin and lose reputation");
            Console.WriteLine("    by selecting the wrong one");
            Console.WriteLine(">-------------------------------<");
            Console.WriteLine();


            foreach (Item item in Trash.ToList()) // Iterate through all the trash and prompt the minigame for each item.
            {
                Prompt(item);
                Trash.Remove(item);
                Inventory.Items.Remove(item);
                Console.WriteLine(">-------------------------------<");
                Console.WriteLine(">-------------------------------<");
                Console.WriteLine();
            }
        }
        else
        {
            Game.ColorWriteLine("You don't have any trash in your inventory!", ConsoleColor.Red);
        }
    }
    private static void Prompt(Item item)
    {
        var choice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title($"Sort [magenta][[{item.Name}]][/] to the corresponding bin.")
                                        .PageSize(8)
                                        .AddChoices(new[] { "Organic", "Plastic", "Metal", "Glass", "Paper", "Electronic", 
                                        "Rubber", "Waste"})
                                );
        ValidateSorting(item, (Item.TrashTypes)Enum.Parse(typeof(Item.TrashTypes), choice));
    }

    private static void ValidateSorting(Item item, Item.TrashTypes trashType) // A method used to determine whether the item was sorted correctly.
    {
        if (item.TrashType == trashType)
        {
            Console.Write("Correctly sorted");
            Game.ColorWrite($" [{item.Name}] ", ConsoleColor.Magenta);
            Console.WriteLine($"as {trashType}");
            Console.Write($"Earned");
            Game.ColorWrite($" {item.Value * Multiplier} ", ConsoleColor.Green);
            Console.WriteLine("reputation!");
            Console.WriteLine();
            Reputation.Add(item.Value * Multiplier);
        }
        else
        {
            Console.Write("Incorrectly sorted");
            Game.ColorWrite($" [{item.Name}] ", ConsoleColor.Magenta);
            Console.WriteLine($"as {trashType}, the correct answer was {item.TrashType}.");
            Console.Write($"Lost");
            Game.ColorWrite($" {item.Value * Multiplier} ", ConsoleColor.Red);
            Console.WriteLine("reputation!");
            Console.WriteLine();
            Reputation.Decrease(item.Value * Multiplier);
        }
    }

    public static void RepairRecyclingStation()
    {
        Open = true;
    }
}