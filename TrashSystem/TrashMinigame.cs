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
    private static bool Open = false;

    public static void Start(Room currentRoom)
    {
        // Checm room and if the recycling station is open.
        if (currentRoom.RoomName != "Recycling Station")
        {
            AnsiConsole.MarkupLine("[bold red]You are not in the recycling station![/]");
            return;
        }
        if (!Open)
        {
            AnsiConsole.MarkupLine("[bold red]The Recycling Station isn't functional yet.[/]");
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
            AnsiConsole.MarkupLine("[bold red]You don't have any trash in your inventory![/]");
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
            AnsiConsole.MarkupLine($"Correctly sorted[bold magenta] [[{item.Name}]][/] as {trashType}");
            AnsiConsole.MarkupLine($"Earned [bold green] {item.Value * Multiplier}[/] reputation!\n");
            Reputation.Add(item.Value * Multiplier);
        }
        else
        {
            AnsiConsole.MarkupLine($"Incorrectly sorted[bold magenta] [[{item.Name}]][/] as {trashType}, the correct answer was {item.TrashType}.");
            AnsiConsole.MarkupLine($"Lost [bold red] {item.Value * Multiplier}[/] reputation!\n");
            Reputation.Decrease(item.Value * Multiplier);
        }
    }

    public static void RepairRecyclingStation()
    {
        Open = true;
    }
}