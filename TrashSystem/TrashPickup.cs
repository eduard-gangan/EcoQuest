using Spectre.Console;

namespace EcoQuest
{
    public static class Trash
    {
        public static int amount = 10000;
        private static int Get() { return amount; }

        private static bool[] milestones = [false, false, false, false];

        private static void PickUp()
        {
            if (amount - Stats.Get() > 0)
            {
                amount -= Stats.Get();
                if (amount < 9500 && !milestones[0])
                {
                    AnsiConsole.MarkupLine("[bold green]\nYou have cleared 500 pieces of trash !\n[/]");
                    AnsiConsole.MarkupLine("[bold green]As the beach clears of trash, the water becomes clearer, and small fish cautiously return to the shore.\n[/]");
                    milestones[0] = true;

                }
                else if (amount < 8000 && !milestones[1])
                {
                    AnsiConsole.MarkupLine("[bold green]\nYou have cleared 2000 pieces of trash !\n[/]");
                    AnsiConsole.MarkupLine("[bold green]With continued cleanup, vibrant corals begin to bloom, and coastal plants take root, slowly breathing life into the shore.\n[/]");
                    milestones[1] = true;
                }
                else if (amount < 5000 && !milestones[2])
                {
                    AnsiConsole.MarkupLine("[bold green]\nYou have cleared 5000 pieces of trash !\n[/]");
                    AnsiConsole.MarkupLine("[bold green]Seabirds start visiting the beach again, and a sea turtle nest appears — a hopeful sign of the ecosystem’s revival.\n[/]");
                    milestones[2] = true;
                }
                else if (amount == 0 && !milestones[3])
                {
                    AnsiConsole.MarkupLine("[bold green]\nYou have cleared all the trash !\n[/]");
                    AnsiConsole.MarkupLine("[bold green]The beach thrives with life! Dolphins leap in the distance, the reef buzzes with activity, and nature’s beauty is fully restored.\n[/]");
                    milestones[3] = true;
                }
            }
            else
            {
                amount = 0;
                Console.WriteLine("The beach thrives with life! Dolphins leap in the distance, the reef buzzes with activity, and nature’s beauty is fully restored. \n There is no more trash !");
            }
        }
        public static void Pick(Room currentRoom, SriLanka sriLanka)
        {
            if (currentRoom != sriLanka.Rooms["beach"])
            {
                AnsiConsole.MarkupLine("[bold red]Go to the beach to pick up trash ![/]");
            }
            else
            {
                if (Inventory.Items.Count() == Inventory.InventoryCapacity)
                {
                    AnsiConsole.MarkupLine("[bold red]Your inventory is full![/]");
                    AnsiConsole.MarkupLine("[grey] Go to the recycling station and use the command 'sort' or 'dump'.[/]");
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
                            AnsiConsole.MarkupLine("[gray]You found a Plastic Bottle ! (Common)[/]");
                        }
                        else if (random <= 3000)
                        {
                            item = new("Candy Wrapper", "Common", true, true, Item.TrashTypes.Plastic, 1, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[gray]You found a Candy Wrapper ! (Common)[/]");
                        }
                        else if (random <= 4500)
                        {
                            item = new("Soda Can", "Common", true, true, Item.TrashTypes.Metal, 2, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[gray]You found a Soda Can ! (Common)[/]");
                        }
                        else if (random <= 6000)
                        {
                            item = new("Paper Cup", "Common", true, true, Item.TrashTypes.Paper, 1, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[gray]You found a Paper Cup ! (Common)[/]");
                        }
                        else if (random <= 6500)
                        {
                            item = new("Glass Bottle", "Uncommon", true, true, Item.TrashTypes.Glass, 5, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[green]You found a Glass Bottle ! (Uncommon)[/]");
                        }
                        else if (random <= 7000)
                        {
                            item = new("Cardboard Box", "Uncommon", true, true, Item.TrashTypes.Paper, 5, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[green]You found a Cardboard Box ! (Uncommon)[/]");
                        }
                        else if (random <= 7500)
                        {
                            item = new("Bottle Cap", "Uncommon", true, true, Item.TrashTypes.Metal, 5, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[green]You found a Bottle Cap ! (Uncommon)[/]");
                        }
                        else if (random <= 8000)
                        {
                            item = new("Old Newspaper", "Uncommon", true, true, Item.TrashTypes.Paper, 5, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[green]You found an Old Newspaper ! (Uncommon)[/]");
                        }
                        else if (random <= 8250)
                        {
                            item = new("Old Book", "Rare", true, true, Item.TrashTypes.Paper, 15, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[cyan]You found an Old Book ! (Rare)[/]");
                        }
                        else if (random <= 8500)
                        {
                            item = new("Bicycle Wheel", "Rare", true, true, Item.TrashTypes.Rubber, 15, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[cyan]You found a Bicycle Wheel ! (Rare)[/]");
                        }
                        else if (random <= 8750)
                        {
                            item = new("Old Magazine", "Rare", true, true, Item.TrashTypes.Paper, 15, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[cyan]You found an Old Magazine ! (Rare)[/]");
                        }
                        else if (random <= 9000)
                        {
                            item = new("Shopping Cart", "Rare", true, true, Item.TrashTypes.Metal, 20, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[cyan]You found a Shopping Cart ! (Rare)[/]");
                        }
                        else if (random <= 9100)
                        {
                            item = new("Used Condom", "Rare", true, true, Item.TrashTypes.Rubber, 15, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[cyan]You found a Used Condom ! (Rare)[/]");
                        }
                        else if (random <= 9200)
                        {
                            item = new("iPhone 6S", "Legendary", true, true, Item.TrashTypes.Electronic, 50, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold yellow]You found an iPhone 6S ! (Legendary)[/]");
                        }
                        else if (random <= 9300)
                        {
                            item = new("Antique Coin", "Legendary", true, true, Item.TrashTypes.Metal, 60, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold yellow]You found an Antique Coin ! (Legendary)[/]");
                        }
                        else if (random <= 9400)
                        {
                            item = new("Lenovo Laptop", "Legendary", true, true, Item.TrashTypes.Electronic, 50, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold yellow]You found a Lenovo Laptop ! (Legendary)[/]");
                        }
                        else if (random <= 9500)
                        {
                            item = new("Vintage Vinyl Record", "Legendary", true, true, Item.TrashTypes.Plastic, 50, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold yellow]You found a Vintage Vinyl Record ! (Legendary)[/]");
                        }
                        else if (random <= 9600)
                        {
                            item = new("Used Airforce Shoe", "Legendary", true, true, Item.TrashTypes.Waste, 50, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold yellow]You found a Used Airforce Shoe ! (Legendary)[/]");
                        }
                        else if (random <= 9700)
                        {
                            item = new("Gold Ring", "Legendary", true, true, Item.TrashTypes.Metal, 60, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold yellow]You found a Gold Ring ! (Legendary)[/]");
                        }
                        else if (random <= 9750)
                        {
                            item = new("Ancient Artifact", "Mythical", true, true, Item.TrashTypes.Glass, 150, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold magenta]You found an Ancient Artifact ! (Mythical)[/]");
                        }
                        else if (random <= 9800)
                        {
                            item = new("Signed Celebrity Photo", "Mythical", true, true, Item.TrashTypes.Paper, 150, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold magenta]You found a Signed Celebrity Photo ! (Mythical)[/]");
                        }
                        else if (random <= 9850)
                        {
                            item = new("Deeds to Land in Congo", "Mythical", true, true, Item.TrashTypes.Paper, 200, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold magenta]You found Deeds to Land in Congo ! (Mythical)[/]");
                        }
                        else if (random <= 9900)
                        {
                            item = new("Treasure Map", "Mythical", true, true, Item.TrashTypes.Paper, 150, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold magenta]You found a Treasure Map ! (Mythical)[/]");
                        }
                        else if (random <= 9950)
                        {
                            item = new("Ancient Sandwich", "Mythical", true, true, Item.TrashTypes.Organic, 200, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold magenta]You found an Ancient Sandwich ! (Mythical)[/]");
                        }
                        else if (random <= 9998)
                        {
                            item = new("Smelly Cheese", "Mythical", true, true, Item.TrashTypes.Organic, 150, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold magenta]You found Smelly Cheese! (Mythical)[/]");
                        }
                        else if (random == 9999)
                        {
                            item = new("Half-full Nutella Jar", "Godly", true, true, Item.TrashTypes.Glass, 500, 0);
                            Inventory.PickUpItem(item);
                            AnsiConsole.MarkupLine("[bold blue]You found a Half-full Nutella Jar ! (Godly)[/]");
                        }

                    }
                    PickUp();
                    AnsiConsole.MarkupLine($"[grey37]Your intuition tells you there are {Get()} pieces of trash left on this beach[/]");
                }
            }
        }
    }
}