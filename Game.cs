﻿using System;

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
                        break;

                    case "sail":
                        if (currentLocation == startingLocation || currentRoom.RoomName.Contains("Port"))
                        {
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

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
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
                    case "talk":
                        currentRoom?.RoomNPC.Talk(0);
                        break;
                    case "pick": //At the moment, the system doesnt take upgrades into account
                        if (currentRoom != sriLanka.Rooms["beach"])
                        {
                            Console.WriteLine("What are you picking up dumbass ?");
                        }
                        else
                        {
                            if (Inventory.Items.Count() == Inventory.InventoryCapacity)
                                Console.WriteLine("Your inventory is full !");
                            else if (Energy.Get() < 5)
                            {
                                ColorWriteLine("You don't have enough energy to pick up this trash!", ConsoleColor.Red);
                            }
                            else
                            {
                                Random rnd = new Random();
                                int random = rnd.Next(1, 10000);
                                Item item;

                                if (random <= 1500)
                                {
                                    item = new("Plastic Bottle", "Common", true, true, 1, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Plastic Bottle ! (Common)", ConsoleColor.Gray);
                                }
                                else if (random <= 3000)
                                {
                                    item = new("Candy Wrapper", "Common", true, true, 1, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Candy Wrapper ! (Common)", ConsoleColor.Gray);
                                }
                                else if (random <= 4500)
                                {
                                    item = new("Soda Can", "Common", true, true, 2, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Soda Can ! (Common)", ConsoleColor.Gray);
                                }
                                else if (random <= 6000)
                                {
                                    item = new("Paper Cup", "Common", true, true, 1, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Paper Cup ! (Common)", ConsoleColor.Gray);
                                }
                                else if (random <= 6500)
                                {
                                    item = new("Glass Bottle", "Uncommon", true, true, 5, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Glass Bottle ! (Uncommon)", ConsoleColor.Green);
                                }
                                else if (random <= 7000)
                                {
                                    item = new("Cardboard Box", "Uncommon", true, true, 5, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Cardboard Box ! (Uncommon)", ConsoleColor.Green);
                                }
                                else if (random <= 7500)
                                {
                                    item = new("Bottle Cap", "Uncommon", true, true, 5, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Bottle Cap ! (Uncommon)", ConsoleColor.Green);
                                }
                                else if (random <= 8000)
                                {
                                    item = new("Old Newspaper", "Uncommon", true, true, 5, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found an Old Newspaper ! (Uncommon)", ConsoleColor.Green);
                                }
                                else if (random <= 8250)
                                {
                                    item = new("Old Book", "Rare", true, true, 15, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found an Old Book ! (Rare)", ConsoleColor.Cyan);
                                }
                                else if (random <= 8500)
                                {
                                    item = new("Bicycle Wheel", "Rare", true, true, 15, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Bicycle Wheel ! (Rare)", ConsoleColor.Cyan);
                                }
                                else if (random <= 8750)
                                {
                                    item = new("Old Book", "Rare", true, true, 15, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found an Old Book ! (Rare)", ConsoleColor.Cyan);
                                }
                                else if (random <= 9000)
                                {
                                    item = new("Shopping Cart", "Rare", true, true, 20, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Shopping Cart ! (Rare)", ConsoleColor.Cyan);
                                }
                                else if (random <= 9100)
                                {
                                    item = new("Used Condom", "Rare", true, true, 15, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Used Condom ! (Rare)", ConsoleColor.Cyan);
                                }
                                else if (random <= 9200)
                                {
                                    item = new("iPhone 6S", "Legendary", true, true, 50, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found an iPhone 6S ! (Legendary)", ConsoleColor.Yellow);
                                }
                                else if (random <= 9300)
                                {
                                    item = new("Antique Coin", "Legendary", true, true, 60, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found an Antique Coin ! (Legendary)", ConsoleColor.Yellow);
                                }
                                else if (random <= 9400)
                                {
                                    item = new("Lenovo Laptop", "Legendary", true, true, 50, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Lenovo Laptop ! (Legendary)", ConsoleColor.Yellow);
                                }
                                else if (random <= 9500)
                                {
                                    item = new("Vintage Vinyl Record", "Legendary", true, true, 50, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Vintage Vinyl Record ! (Legendary)", ConsoleColor.Yellow);
                                }
                                else if (random <= 9600)
                                {
                                    item = new("Used Airforce Shoe", "Legendary", true, true, 50, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Used Airforce Shoe ! (Legendary)", ConsoleColor.Yellow);
                                }
                                else if (random <= 9700)
                                {
                                    item = new("Gold Ring", "Legendary", true, true, 60, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Gold Ring ! (Legendary)", ConsoleColor.Yellow);
                                }
                                else if (random <= 9750)
                                {
                                    item = new("Ancient Artifact", "Mythical", true, true, 150, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found an Ancient Artifact ! (Mythical)", ConsoleColor.Magenta);
                                }
                                else if (random <= 9800)
                                {
                                    item = new("Signed Celebrity Photo", "Mythical", true, true, 150, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Signed Celebrity Photo ! (Mythical)", ConsoleColor.Magenta);
                                }
                                else if (random <= 9850)
                                {
                                    item = new("Deeds to Land in Congo", "Mythical", true, true, 200, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found Deeds to Land in Congo ! (Mythical)", ConsoleColor.Magenta);
                                }
                                else if (random <= 9900)
                                {
                                    item = new("Treasure Map", "Mythical", true, true, 150, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Treasure Map ! (Mythical)", ConsoleColor.Magenta);
                                }
                                else if (random <= 9950)
                                {
                                    item = new("Ancient Sandwich", "Mythical", true, true, 200, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found an Ancient Sandwich ! (Mythical)", ConsoleColor.Magenta);
                                }
                                else if (random <= 9998)
                                {
                                    item = new("Smelly Cheese", "Mythical", true, true, 150, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found Smelly Cheese! (Mythical)", ConsoleColor.Magenta);
                                }
                                else if (random == 9999)
                                {
                                    item = new("Half-full Nutella Jar", "Godly", true, true, 500, 0);
                                    Inventory.PickUpItem(item);
                                    ColorWriteLine("You found a Half-full Nutella Jar ! (Godly)", ConsoleColor.Blue);
                                }
                                Trash.PickUp();
                                Console.WriteLine($"\n Your intuition tells you there are {Trash.Get()} pieces of trash left on this beach");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("I don't know that command.");
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
                        Console.WriteLine("You're on your way to Sri Lanka... \n\n\n");
                        Console.WriteLine("You arrived in Sri Lanka!");
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
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        private static void PrintWelcome()
        {
            Console.Clear();
            ColorWriteLine("Welcome to EcoQuest!", ConsoleColor.Blue);
            Console.WriteLine();

            Console.WriteLine("You begin on board a research vessel, drifting along calm blue waters under an open sky.\nThe ship gently rocks, its deck bustling with equipment—nets, sonar tools, oxygen tanks, and more—all arranged with precision. \nAs an aspiring marine biologist, you're on your first expedition. The setting is new, thrilling, and a little overwhelming. \nThe crisp salt air and distant cry of seagulls fill you with excitement for the adventure that awaits.\n");
            // [Insert more lore about the character here]
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
            Console.WriteLine("Type 'talk' to talk to an NPC.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }

        private static void CreateNpcs()
        {

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
    }
}