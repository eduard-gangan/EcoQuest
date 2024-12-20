namespace EcoQuest
{
    public static class Upgrades
    {
        public static List<string> Multipliers = new([
            "Basic Glove Set",
            "Eco-friendly Net",
            "Eco Shoes",
            "Mini Grabber Bot",
            "Advanced Eco Drone",
            "Community Support",
            "Nation-wide Campaign"
        ]);
        public static List<int> MultipliersRep = new([
            500,
            1500,
            4000,
            10000,
            30000,
            90000,
            300000
        ]);
        public static List<string> InventoryUpgrades = new([
            "Lidl Bag",
            "Reinforced Backpack",
            "Trash Sledge",
            "Family-size Shopping Cart",
            "Eco Trailer",
            "Solar-Powered Trash Compactor"
        ]);
        public static List<int> InventoryRep = new([
            1000,
            2750,
            7000,
            20000,
            60000,
            150000
        ]);
        public static void Menu()
        {
            bool playing = true;
            int currentRep = Reputation.Get();

            while (playing)
            {
                Console.WriteLine("\nTrash pick-up upgrades:\n");
                for (int i = 0; i < Multipliers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Multipliers[i]} : {Math.Pow(2, i + 1)}x multiplier  ({MultipliersRep[i]} rep)");
                }
                Console.WriteLine("\nInventory upgrades:\n");
                for (int i = 0; i < InventoryUpgrades.Count; i++)
                {
                    Console.WriteLine($"{i + 1 + Multipliers.Count}. {InventoryUpgrades[i]} : {5 * Math.Pow(2, i + 2)} capacity  ({InventoryRep[i]}) rep");
                }
                try
                {
                    int option = Int32.Parse(Console.ReadLine());
                    if (option < 0 || option > Multipliers.Count + InventoryUpgrades.Count)
                    {
                        Console.WriteLine("There's no such option dumbass !");
                        playing = false;
                    }
                    else if (option > Multipliers.Count)
                    {
                        if (InventoryRep[option - 1 - Multipliers.Count] > currentRep)
                        {
                            Console.WriteLine("You don't have enough reputation for that !");
                            playing = false;
                        }
                        Inventory.InventoryCapacity = Inventory.InventoryCapacity <= 5 ? 20 : Inventory.InventoryCapacity * 2;
                        InventoryUpgrades.RemoveAt(option - Multipliers.Count - 1);
                        InventoryRep.RemoveAt(option - Multipliers.Count - 1);
                        Console.WriteLine("Upgrade applied !");
                        playing = false;
                    }
                    else
                    {
                        if (MultipliersRep[option - 1] > currentRep)
                        {
                            Console.WriteLine("You don't have enough reputation for that !");
                            playing = false;
                        }
                        Stats.UpgradeM(2);
                        Multipliers.RemoveAt(option - 1);
                        MultipliersRep.RemoveAt(option - 1);
                        Console.WriteLine("Upgrade applied !");
                        playing = false;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Type a number dumbass !");
                    playing = false;
                }
            }
        }
    }
}