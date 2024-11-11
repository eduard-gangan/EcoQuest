/*
A system for a trash sorting minigame [INSERT DOCUMENTATION HERE]

- Use the Start() method to start the sorting minigame.

 */

namespace EcoQuest;
public static class TrashMinigame
{
    private static List<Item> Trash = [];
    private static int Multiplier = 2;
    private static int MaxCount = 3;
    private static int EnergyConsumption = 5;

    public static void Start(string currentRoomName)
    {
        // Check energy and room
        if (currentRoomName != "Recycling Station")
        {
                Console.WriteLine(currentRoomName);
                Console.WriteLine("You are not in the recycling station!");
                return;
        }
        if (Energy.Get() < EnergyConsumption)
        {
            Game.ColorWriteLine("You don't have enough energy to pick up this trash!", ConsoleColor.Red);
            return;
        }
        
        Energy.Decrease(EnergyConsumption);

        // (Uncommented code)
        if (Inventory.Items.Count > 0)
        {
            foreach (Item item in Inventory.Items.ToList())
            {
                if (Trash.Count == MaxCount)
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


            foreach (Item item in Trash.ToList())
            {
                Prompt(item);
                Trash.Remove(item);
                Console.WriteLine(">-----------------------------<");
                Console.WriteLine(">-----------------------------<");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("You don't have any trash in your inventory!");
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
                    Assert(item, Item.TrashTypes.Organic);
                    isPlaying = false;
                    break;
                case "2":
                    Assert(item, Item.TrashTypes.Plastic);
                    isPlaying = false;
                    break;
                case "3":
                    Assert(item, Item.TrashTypes.Metal);
                    isPlaying = false;
                    break;
                case "4":
                    Assert(item, Item.TrashTypes.Glass);
                    isPlaying = false;
                    break;
                case "5":
                    Assert(item, Item.TrashTypes.Paper);
                    isPlaying = false;
                    break;
                case "6":
                    Assert(item, Item.TrashTypes.Electronic);
                    isPlaying = false;
                    break;
                case "7":
                    Assert(item, Item.TrashTypes.Rubber);
                    isPlaying = false;
                    break;
                case "8":
                    Assert(item, Item.TrashTypes.Waste);
                    isPlaying = false;
                    break;
                default:
                    Console.WriteLine("Type the corresponding number");
                    Console.WriteLine("      [1] through [8]        ");
                    break;

            }
        }
    }

    private static void Assert(Item item, Item.TrashTypes trashType)
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
            Console.WriteLine($"as {trashType}");
            Console.Write($"Lost");
            Game.ColorWrite($" {item.Value * Multiplier} ", ConsoleColor.Green);
            Console.WriteLine("reputation!");
            Console.WriteLine();
            Reputation.Decrease(item.Value * Multiplier);
        }
    }
}