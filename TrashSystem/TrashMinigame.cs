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

namespace EcoQuest;
public static class TrashMinigame
{
    private static List<Item> Trash = [];
    public static int Multiplier { get; set; } = 5;
    public static int EnergyConsumption { get; set; } = 5;
    private static bool Open = true;

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
            Console.WriteLine(">-----------------------------<");
            Console.WriteLine("     Trash Sorting Minigame");
            Console.WriteLine(">-----------------------------<");
            Console.WriteLine("Select either of the options by");
            Console.WriteLine("typing the corresponding number");
            Console.WriteLine("       [1] through [8]         ");
            Console.WriteLine(">-----------------------------<");
            Console.WriteLine();


            foreach (Item item in Trash.ToList()) // Iterate through all the trash and prompt the minigame for each item.
            {
                Prompt(item);
                Trash.Remove(item);
                Inventory.Items.Remove(item);
                Console.WriteLine(">-----------------------------<");
                Console.WriteLine(">-----------------------------<");
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
        Console.Write("Sort");
        Game.ColorWrite($" [{item.Name}] ", ConsoleColor.Magenta);
        Console.WriteLine("to the corresponding bin");
        Console.WriteLine("1. >   Organic  <");
        Console.WriteLine("2. >   Plastic  <");
        Console.WriteLine("3. >    Metal   <");
        Console.WriteLine("4. >    Glass   <");
        Console.WriteLine("5. >    Paper   <");
        Console.WriteLine("6. > Electronic <");
        Console.WriteLine("7. >    Rubber  <");
        Console.WriteLine("8. >    Waste   <");
        Console.WriteLine();
        Console.Write("> ");

        bool isPlaying = true;
        while (isPlaying)
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ValidateSorting(item, Item.TrashTypes.Organic);
                    isPlaying = false;
                    break;
                case "2":
                    ValidateSorting(item, Item.TrashTypes.Plastic);
                    isPlaying = false;
                    break;
                case "3":
                    ValidateSorting(item, Item.TrashTypes.Metal);
                    isPlaying = false;
                    break;
                case "4":
                    ValidateSorting(item, Item.TrashTypes.Glass);
                    isPlaying = false;
                    break;
                case "5":
                    ValidateSorting(item, Item.TrashTypes.Paper);
                    isPlaying = false;
                    break;
                case "6":
                    ValidateSorting(item, Item.TrashTypes.Electronic);
                    isPlaying = false;
                    break;
                case "7":
                    ValidateSorting(item, Item.TrashTypes.Rubber);
                    isPlaying = false;
                    break;
                case "8":
                    ValidateSorting(item, Item.TrashTypes.Waste);
                    isPlaying = false;
                    break;
                default:
                    Console.WriteLine("Type the corresponding number");
                    Console.WriteLine("      [1] through [8]        ");
                    Console.Write("> ");
                    break;

            }
        }
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