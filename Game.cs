using System;
using System.Formats.Asn1;
using System.Security.Claims;
using System.Transactions;

namespace EcoQuest
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private Location? currentLocation;
        public Start? startingLocation;
        public SriLanka? sriLanka;
        public Australia? australia;
        public Indonesia? indonesia;

        public void Play()
        {

            Parser parser = new();
            CreateNpcs();

            sriLanka = new SriLanka("Sri Lanka", "Description for Sri Lanka", 0);
            australia = new Australia("Australia", "Description for Australia", 1000);
            indonesia = new Indonesia("Indonesia", "Description for Indonesia", 2000);
            startingLocation = new Start("Somewhere at sea", "Description for starting location", 0, sriLanka, australia, indonesia);

            currentLocation = startingLocation;

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                if (currentRoom?.RoomName == null)
                    System.Console.WriteLine("[Boat]");
                else
                    Console.WriteLine($"[{currentRoom?.RoomName}]");

                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    ColorWriteLine("I don't know that command.", ConsoleColor.Red);
                    continue;
                }

                switch (command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.RoomDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                            ColorWriteLine("You can't go back from here!", ConsoleColor.Red);
                        else

                            currentRoom = previousRoom;
                        Console.Write($"[Suggested Commands]: ");
                        foreach (string commands in currentRoom.AvailableCommands)
                        {
                            Console.Write($"{commands} ");
                        }
                        System.Console.WriteLine();
                        break;

                    case "sail":
                        if (QuestSriLanka.Active != true)
                        {
                            if (currentLocation == startingLocation || currentRoom.RoomName.Contains("Port"))
                            {
                                Console.Clear();
                                Console.WriteLine("Where do you want to sail?");
                                Console.WriteLine("(Sri Lanka, Sea)");
                                Sail(Console.ReadLine());
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"You can't depart from {currentRoom?.RoomName}, go to the port.");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can't sail while you have an active quest");
                            break;
                        }


                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        Console.Clear();
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "map":
                        if (currentLocation == sriLanka)
                        {
                            MapSriLanka.ShowMap(currentRoom);
                        }
                        break;

                    case "balance":
                        Console.Write($"You currently have");
                        ColorWrite($" {Money.Get()} ", ConsoleColor.Yellow);
                        Console.WriteLine("EcoCoins in your wallet!");
                        break;
                    case "reputation":
                        Console.Write($"You currently have");
                        ColorWrite($" {Reputation.Get()} ", ConsoleColor.Green);
                        Console.WriteLine("reputation!");
                        break;
                    case "energy":
                        if (Energy.Get() <= 1)
                        {
                            Console.Write("You need to sleep, you have");
                            ColorWrite($" {Energy.Get()} ", ConsoleColor.Cyan);
                            Console.WriteLine("energy!");
                        }
                        else
                        {
                            Console.Write($"You currently have");
                            ColorWrite($" {Energy.Get()} ", ConsoleColor.Cyan);
                            Console.WriteLine("energy!");
                        }
                        break;
                    case "inventory":
                        Inventory.DisplayInventory();
                        break;
                    case "sleep":
                        if (currentRoom.RoomName.Contains("Port"))
                        {
                            Energy.Replenish();
                            Console.WriteLine("You slept and your energy was replenished!");
                        }
                        else
                        {
                            Console.WriteLine("You can't sleep here, dumbass...");
                        }
                        break;
                    case "dump":
                        if (currentRoom?.RoomName == "Recycling Station")
                            TrashDump.Dump();
                        else
                            Console.WriteLine("You are not in the recycling station!");
                        break;
                    case "sort":
                        TrashMinigame.Start(currentRoom?.RoomName);
                        break;
                    case "talk":
                        currentRoom?.RoomNPC.Talk();
                        break;
                    case "pick": //At the moment, the system doesnt take upgrades into account
                        if (currentRoom != sriLanka.Rooms["beach"])
                        {
                            Console.WriteLine("What are you picking up dumbass ?");
                        }
                        else
                        {
                            if (Inventory.Items.Count() == Inventory.InventoryCapacity)
                                Console.WriteLine("Your inventory is full!.");
                            else if (Energy.Get() < 5)
                            {
                                ColorWriteLine("You don't have enough energy to pick up this trash!", ConsoleColor.Red);
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
                                        ColorWriteLine("You found a Plastic Bottle ! (Common)", ConsoleColor.Gray);
                                    }
                                    else if (random <= 3000)
                                    {
                                        item = new("Candy Wrapper", "Common", true, true, Item.TrashTypes.Plastic, 1, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Candy Wrapper ! (Common)", ConsoleColor.Gray);
                                    }
                                    else if (random <= 4500)
                                    {
                                        item = new("Soda Can", "Common", true, true, Item.TrashTypes.Metal, 2, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Soda Can ! (Common)", ConsoleColor.Gray);
                                    }
                                    else if (random <= 6000)
                                    {
                                        item = new("Paper Cup", "Common", true, true, Item.TrashTypes.Paper, 1, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Paper Cup ! (Common)", ConsoleColor.Gray);
                                    }
                                    else if (random <= 6500)
                                    {
                                        item = new("Glass Bottle", "Uncommon", true, true, Item.TrashTypes.Glass, 5, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Glass Bottle ! (Uncommon)", ConsoleColor.Green);
                                    }
                                    else if (random <= 7000)
                                    {
                                        item = new("Cardboard Box", "Uncommon", true, true, Item.TrashTypes.Paper, 5, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Cardboard Box ! (Uncommon)", ConsoleColor.Green);
                                    }
                                    else if (random <= 7500)
                                    {
                                        item = new("Bottle Cap", "Uncommon", true, true, Item.TrashTypes.Metal, 5, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Bottle Cap ! (Uncommon)", ConsoleColor.Green);
                                    }
                                    else if (random <= 8000)
                                    {
                                        item = new("Old Newspaper", "Uncommon", true, true, Item.TrashTypes.Paper, 5, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found an Old Newspaper ! (Uncommon)", ConsoleColor.Green);
                                    }
                                    else if (random <= 8250)
                                    {
                                        item = new("Old Book", "Rare", true, true, Item.TrashTypes.Paper, 15, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found an Old Book ! (Rare)", ConsoleColor.Cyan);
                                    }
                                    else if (random <= 8500)
                                    {
                                        item = new("Bicycle Wheel", "Rare", true, true, Item.TrashTypes.Rubber, 15, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Bicycle Wheel ! (Rare)", ConsoleColor.Cyan);
                                    }
                                    else if (random <= 8750)
                                    {
                                        item = new("Old Magazine", "Rare", true, true, Item.TrashTypes.Paper, 15, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found an Old Magazine ! (Rare)", ConsoleColor.Cyan);
                                    }
                                    else if (random <= 9000)
                                    {
                                        item = new("Shopping Cart", "Rare", true, true, Item.TrashTypes.Metal, 20, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Shopping Cart ! (Rare)", ConsoleColor.Cyan);
                                    }
                                    else if (random <= 9100)
                                    {
                                        item = new("Used Condom", "Rare", true, true, Item.TrashTypes.Rubber, 15, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Used Condom ! (Rare)", ConsoleColor.Cyan);
                                    }
                                    else if (random <= 9200)
                                    {
                                        item = new("iPhone 6S", "Legendary", true, true, Item.TrashTypes.Electronic, 50, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found an iPhone 6S ! (Legendary)", ConsoleColor.Yellow);
                                    }
                                    else if (random <= 9300)
                                    {
                                        item = new("Antique Coin", "Legendary", true, true, Item.TrashTypes.Metal, 60, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found an Antique Coin ! (Legendary)", ConsoleColor.Yellow);
                                    }
                                    else if (random <= 9400)
                                    {
                                        item = new("Lenovo Laptop", "Legendary", true, true, Item.TrashTypes.Electronic, 50, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Lenovo Laptop ! (Legendary)", ConsoleColor.Yellow);
                                    }
                                    else if (random <= 9500)
                                    {
                                        item = new("Vintage Vinyl Record", "Legendary", true, true, Item.TrashTypes.Plastic, 50, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Vintage Vinyl Record ! (Legendary)", ConsoleColor.Yellow);
                                    }
                                    else if (random <= 9600)
                                    {
                                        item = new("Used Airforce Shoe", "Legendary", true, true, Item.TrashTypes.Waste, 50, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Used Airforce Shoe ! (Legendary)", ConsoleColor.Yellow);
                                    }
                                    else if (random <= 9700)
                                    {
                                        item = new("Gold Ring", "Legendary", true, true, Item.TrashTypes.Metal, 60, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Gold Ring ! (Legendary)", ConsoleColor.Yellow);
                                    }
                                    else if (random <= 9750)
                                    {
                                        item = new("Ancient Artifact", "Mythical", true, true, Item.TrashTypes.Glass, 150, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found an Ancient Artifact ! (Mythical)", ConsoleColor.Magenta);
                                    }
                                    else if (random <= 9800)
                                    {
                                        item = new("Signed Celebrity Photo", "Mythical", true, true, Item.TrashTypes.Paper, 150, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Signed Celebrity Photo ! (Mythical)", ConsoleColor.Magenta);
                                    }
                                    else if (random <= 9850)
                                    {
                                        item = new("Deeds to Land in Congo", "Mythical", true, true, Item.TrashTypes.Paper, 200, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found Deeds to Land in Congo ! (Mythical)", ConsoleColor.Magenta);
                                    }
                                    else if (random <= 9900)
                                    {
                                        item = new("Treasure Map", "Mythical", true, true, Item.TrashTypes.Paper, 150, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Treasure Map ! (Mythical)", ConsoleColor.Magenta);
                                    }
                                    else if (random <= 9950)
                                    {
                                        item = new("Ancient Sandwich", "Mythical", true, true, Item.TrashTypes.Organic, 200, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found an Ancient Sandwich ! (Mythical)", ConsoleColor.Magenta);
                                    }
                                    else if (random <= 9998)
                                    {
                                        item = new("Smelly Cheese", "Mythical", true, true, Item.TrashTypes.Organic, 150, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found Smelly Cheese! (Mythical)", ConsoleColor.Magenta);
                                    }
                                    else if (random == 9999)
                                    {
                                        item = new("Half-full Nutella Jar", "Godly", true, true, Item.TrashTypes.Glass, 500, 0);
                                        Inventory.PickUpItem(item);
                                        ColorWriteLine("You found a Half-full Nutella Jar ! (Godly)", ConsoleColor.Blue);
                                    }

                                }
                                Trash.PickUp();
                                Console.WriteLine($"\n Your intuition tells you there are {Trash.Get()} pieces of trash left on this beach");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.ResetColor();
            Console.WriteLine("Thank you for playing EcoQuest");
        }

        private void Sail(string destination) // Use the Sail method to move between locations
        {
            switch (destination)
            {
                case "Sri":
                case "sri":
                case "Sri Lanka":
                case "sri lanka":
                case "Srilanka":
                case "srilanka":
                case "Lanka":
                case "lanka":
                    if (currentLocation != sriLanka)
                    {
                        Console.Clear();
                        Console.WriteLine("You're on your way to Sri Lanka... \n");
                        Console.WriteLine("You arrived in Sri Lanka!\n");
                        Console.WriteLine("Type 'look' to see you what's around you.\n");
                        currentLocation = sriLanka;
                        currentRoom = sriLanka?.Rooms["port"];
                    }
                    else
                    {
                        Console.WriteLine("You're already in Sri Lanka!");
                    }
                    break;
                case "Sea":
                case "sea":
                    if (currentLocation != startingLocation)
                    {
                        currentRoom = null;
                        Console.Clear();
                        Console.WriteLine("You're travelling back to the big blue sea...");
                        currentLocation = startingLocation;
                    }
                    else
                    {
                        Console.WriteLine("You're already at sea!");
                    }
                    break;
                default:
                    Console.WriteLine("You sure that place exists?");
                    break;
            }
        }

        private void Move(string direction) // Use the Move method to move inside locations
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                Console.Clear();
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
                Console.Write($"[Suggested Commands]: ");
                foreach (string command in currentRoom.AvailableCommands)
                {
                    Console.Write($"{command} ");
                }
                System.Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        private static void PrintWelcome()
        {
            Console.Clear();
            ColorWriteLine("Welcome to EcoQuest!\n", ConsoleColor.Blue);
            Console.WriteLine("You are aboard a research vessel, drifting along calm blue waters under an open sky.\nThe ship gently rocks, its deck bustling with equipment, nets, sonar tools, oxygen tanks, and more. \nAs an aspiring marine biologist, you're on your first expedition. \nThe salty air and distant cry of seagulls fill you with excitement for the adventure that awaits.\nTo the helm of the ship stands Captain Sylvia Earle, a legendary oceanographer, explorer, and marine biologist with a lifetime of experience beneath the waves.\nHer sharp and thoughtful eyes reflect countless voyage and experience for being at sea.\nHer weathered face and confident stance presents the wisdom of someone who has spent decades charting unknown waters \nand fighting tirelessly to protect marine ecosystems.\nAs her hands rest firmly on the ship's wheel, steadily guiding the vessel, she can't help but notice you staring at her, and decides to approach.\n");
            Thread.Sleep(1000);
            Console.WriteLine("Captain Sylvia approaches, her voice, steady and inspiring:\n\n[Captain Sylvia]: ");
            Thread.Sleep(1000);
            SlowWrite("Welcome aboard explorer! The ocean faces grave threats.\nPollution, overfishing, warming waters, and habitat destruction, but it's not too late to act.\nI've spent a lifetime beneath the waves, witnessing both devastation and resilience.\nNow, it's your turn. The United Nations calls us to action through Goal 14: Life Below Water, a mission to restore and protect our Ocean.\nEvery action, no matter how small, creates ripples of change. With passion and persistence, we can bring life back to these waters.\nSo, are you ready to dive in and be the hero the ocean needs? Let's make waves for a better future.\n");
            // [Insert more lore about the character here];
            Console.WriteLine("Type 'sail' to choose your destination.");
            Console.WriteLine("Type 'help' to see a list of available commands.");
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'sail' to go to another destination.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'balance' to see how many EcoCoins you have.");
            Console.WriteLine("Type 'reputation' to see your reputation.");
            Console.WriteLine("Type 'energy' to see your energy levels.");
            Console.WriteLine("Type 'inventory' to see your inventory.");
            Console.WriteLine("Type 'dump' to dump your trash.");
            Console.WriteLine("Type 'sort' to sort your trash.");
            Console.WriteLine("Type 'talk' to talk to an NPC.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");

        }

        private void CreateNpcs()
        {
            //Garry 
            NPCs.Garry.MainDialogue.AddOption(PlayerReply.GARRY_NAME, () => SlowWrite(NpcReply.GARRY_NAME));
            NPCs.Garry.MainDialogue.AddOption(PlayerReply.GARRY_BACKSTORY, () => SlowWrite(NpcReply.GARRY_BACKSTORY));
            NPCs.Garry.MainDialogue.AddOption(PlayerReply.GARRY_WHY, () => SlowWrite(NpcReply.GARRY_WHY));
            NPCs.Garry.MainDialogue.AddOption(PlayerReply.GARRY_QUEST, () => { SlowWrite(NpcReply.GARRY_QUEST); Inventory.InventoryCapacity = 5; QuestSriLanka.start(); ColorWriteLine("Your inventory space has been increased to 5!", ConsoleColor.Green); NPCs.Garry.MainDialogue.RemoveOption(PlayerReply.GARRY_QUEST); NPCs.Garry.MainDialogue.TriggerDialogue(); });
            NPCs.Garry.MainDialogue.AddOption(PlayerReply.BYE, () => { SlowWrite(NpcReply.GARRY_BYE); NPCs.Garry.MainDialogue.TriggerDialogue(); });

            //Larry
            NPCs.Larry.MainDialogue.AddOption(PlayerReply.LARRY_NAME, () => SlowWrite(NpcReply.LARRY_NAME));
            NPCs.Larry.MainDialogue.AddOption(PlayerReply.LARRY_BACKSTORY, () => SlowWrite(NpcReply.LARRY_BACKSTORY));
            NPCs.Larry.MainDialogue.AddOption(PlayerReply.LARRY_PLAYER, () => SlowWrite(NpcReply.LARRY_PLAYER));
            NPCs.Larry.MainDialogue.AddOption(PlayerReply.LARRY_BYE, () => { SlowWrite(NpcReply.LARRY_BYE); NPCs.Larry.MainDialogue.TriggerDialogue(); });

            //Mayor Lanka
            NPCs.Lanka.MainDialogue.AddOption(PlayerReply.LANKA_PLAYER, () => SlowWrite(NpcReply.LANKA_PLAYER));
            NPCs.Lanka.MainDialogue.AddOption("Upgrade", () => { Upgrades.Menu(); });
            NPCs.Lanka.MainDialogue.AddOption(PlayerReply.BYE, () => { SlowWrite(NpcReply.LANKA_BYE); NPCs.Lanka.MainDialogue.TriggerDialogue(); });
        }

        // Temporary console styling methods
        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            System.Console.Write(text);
            Console.ResetColor();
        }

        public static void SlowWrite(string text)
        {
            Random rnd = new Random();
            bool skipDelay = false;
            foreach (char c in text)
            {
                Console.Write(c);

                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    skipDelay = true;
                }
                if (!skipDelay)
                {
                    Thread.Sleep(rnd.Next(30, 50));
                }

            }
            Console.WriteLine();
        }
    }
}