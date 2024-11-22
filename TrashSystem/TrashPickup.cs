namespace EcoQuest
{
    public static class Trash
    {
        public static int amount = 10000;
        private static int Get() { return amount; }

        private static void PickUp()
        {
            if (amount - Stats.Get() > 0)
            {
                amount -= Stats.Get();
            }
            else
            {
                Console.WriteLine("There is no more trash !");
            }
        }
        public static void Pick(Room currentRoom, SriLanka sriLanka)
        {
            if (currentRoom != sriLanka.Rooms["beach"])
            {
                Game.ColorWriteLine("There is no trash here!", ConsoleColor.Red);
            }
            else
            {
                if (Inventory.Items.Count() == Inventory.InventoryCapacity)
                    Game.ColorWriteLine("Your inventory is full!", ConsoleColor.Red);
                else if (Energy.Decrease(5) == false)
                {
                    Game.ColorWriteLine("You don't have enough energy to pick up this trash!", ConsoleColor.Red);
                }
                else
                {
                    int stat = Stats.Get();
                    for (int i = 0; i < stat; i++)
                    {
                        Random rnd = new Random();
                        int random = rnd.Next(1, 10000);
                        Item item;

                        if (random <= 1500)
                        {
                            item = new("Plastic Bottle", "Common", true, true, Item.TrashTypes.Plastic, 1, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Plastic Bottle ! (Common)", ConsoleColor.Gray);
                        }
                        else if (random <= 3000)
                        {
                            item = new("Candy Wrapper", "Common", true, true, Item.TrashTypes.Plastic, 1, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Candy Wrapper ! (Common)", ConsoleColor.Gray);
                        }
                        else if (random <= 4500)
                        {
                            item = new("Soda Can", "Common", true, true, Item.TrashTypes.Metal, 2, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Soda Can ! (Common)", ConsoleColor.Gray);
                        }
                        else if (random <= 6000)
                        {
                            item = new("Paper Cup", "Common", true, true, Item.TrashTypes.Paper, 1, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Paper Cup ! (Common)", ConsoleColor.Gray);
                        }
                        else if (random <= 6500)
                        {
                            item = new("Glass Bottle", "Uncommon", true, true, Item.TrashTypes.Glass, 5, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Glass Bottle ! (Uncommon)", ConsoleColor.Green);
                        }
                        else if (random <= 7000)
                        {
                            item = new("Cardboard Box", "Uncommon", true, true, Item.TrashTypes.Paper, 5, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Cardboard Box ! (Uncommon)", ConsoleColor.Green);
                        }
                        else if (random <= 7500)
                        {
                            item = new("Bottle Cap", "Uncommon", true, true, Item.TrashTypes.Metal, 5, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Bottle Cap ! (Uncommon)", ConsoleColor.Green);
                        }
                        else if (random <= 8000)
                        {
                            item = new("Old Newspaper", "Uncommon", true, true, Item.TrashTypes.Paper, 5, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found an Old Newspaper ! (Uncommon)", ConsoleColor.Green);
                        }
                        else if (random <= 8250)
                        {
                            item = new("Old Book", "Rare", true, true, Item.TrashTypes.Paper, 15, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found an Old Book ! (Rare)", ConsoleColor.Cyan);
                        }
                        else if (random <= 8500)
                        {
                            item = new("Bicycle Wheel", "Rare", true, true, Item.TrashTypes.Rubber, 15, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Bicycle Wheel ! (Rare)", ConsoleColor.Cyan);
                        }
                        else if (random <= 8750)
                        {
                            item = new("Old Magazine", "Rare", true, true, Item.TrashTypes.Paper, 15, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found an Old Magazine ! (Rare)", ConsoleColor.Cyan);
                        }
                        else if (random <= 9000)
                        {
                            item = new("Shopping Cart", "Rare", true, true, Item.TrashTypes.Metal, 20, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Shopping Cart ! (Rare)", ConsoleColor.Cyan);
                        }
                        else if (random <= 9100)
                        {
                            item = new("Used Condom", "Rare", true, true, Item.TrashTypes.Rubber, 15, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Used Condom ! (Rare)", ConsoleColor.Cyan);
                        }
                        else if (random <= 9200)
                        {
                            item = new("iPhone 6S", "Legendary", true, true, Item.TrashTypes.Electronic, 50, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found an iPhone 6S ! (Legendary)", ConsoleColor.Yellow);
                        }
                        else if (random <= 9300)
                        {
                            item = new("Antique Coin", "Legendary", true, true, Item.TrashTypes.Metal, 60, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found an Antique Coin ! (Legendary)", ConsoleColor.Yellow);
                        }
                        else if (random <= 9400)
                        {
                            item = new("Lenovo Laptop", "Legendary", true, true, Item.TrashTypes.Electronic, 50, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Lenovo Laptop ! (Legendary)", ConsoleColor.Yellow);
                        }
                        else if (random <= 9500)
                        {
                            item = new("Vintage Vinyl Record", "Legendary", true, true, Item.TrashTypes.Plastic, 50, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Vintage Vinyl Record ! (Legendary)", ConsoleColor.Yellow);
                        }
                        else if (random <= 9600)
                        {
                            item = new("Used Airforce Shoe", "Legendary", true, true, Item.TrashTypes.Waste, 50, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Used Airforce Shoe ! (Legendary)", ConsoleColor.Yellow);
                        }
                        else if (random <= 9700)
                        {
                            item = new("Gold Ring", "Legendary", true, true, Item.TrashTypes.Metal, 60, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Gold Ring ! (Legendary)", ConsoleColor.Yellow);
                        }
                        else if (random <= 9750)
                        {
                            item = new("Ancient Artifact", "Mythical", true, true, Item.TrashTypes.Glass, 150, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found an Ancient Artifact ! (Mythical)", ConsoleColor.Magenta);
                        }
                        else if (random <= 9800)
                        {
                            item = new("Signed Celebrity Photo", "Mythical", true, true, Item.TrashTypes.Paper, 150, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Signed Celebrity Photo ! (Mythical)", ConsoleColor.Magenta);
                        }
                        else if (random <= 9850)
                        {
                            item = new("Deeds to Land in Congo", "Mythical", true, true, Item.TrashTypes.Paper, 200, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found Deeds to Land in Congo ! (Mythical)", ConsoleColor.Magenta);
                        }
                        else if (random <= 9900)
                        {
                            item = new("Treasure Map", "Mythical", true, true, Item.TrashTypes.Paper, 150, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Treasure Map ! (Mythical)", ConsoleColor.Magenta);
                        }
                        else if (random <= 9950)
                        {
                            item = new("Ancient Sandwich", "Mythical", true, true, Item.TrashTypes.Organic, 200, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found an Ancient Sandwich ! (Mythical)", ConsoleColor.Magenta);
                        }
                        else if (random <= 9998)
                        {
                            item = new("Smelly Cheese", "Mythical", true, true, Item.TrashTypes.Organic, 150, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found Smelly Cheese! (Mythical)", ConsoleColor.Magenta);
                        }
                        else if (random == 9999)
                        {
                            item = new("Half-full Nutella Jar", "Godly", true, true, Item.TrashTypes.Glass, 500, 0);
                            Inventory.PickUpItem(item);
                            Game.ColorWriteLine("You found a Half-full Nutella Jar ! (Godly)", ConsoleColor.Blue);
                        }

                    }
                    PickUp();
                    Game.ColorWriteLine($"Your intuition tells you there are {Get()} pieces of trash left on this beach", ConsoleColor.DarkGray);
                }
            }
        }
    }
}