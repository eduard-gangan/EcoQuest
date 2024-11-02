using System;

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
                /*
                Noticed it was printing a blank line when starting, also if you sail to sri lanka then sail
                back to sea currentRoom stays port or whatever location you were in which is probably not intended.
                - PS: We probably need to have a room inside of the "Sea/StartingLocation" location called ship/boat
                so that we could have descriptions for the look command.
                 */
                if (currentRoom?.RoomName == null){
                    ColorWriteLine("-NO ROOM-", ConsoleColor.Red);
                }
                else{
                    Console.WriteLine($"[{currentRoom?.RoomName}]");
                }

                //Console.WriteLine($"[{currentRoom?.RoomName}]");
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
                    if (currentLocation == startingLocation || currentRoom?.RoomName == "Port")
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

                    case "north": case "south": case "east": case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;
                    case "balance":
                        ColorWriteLine($"You currently have {Money.Get()} EcoCoins in your wallet!", ConsoleColor.Green);
                        break;
                    case "reputation":
                        Console.WriteLine($"You currently have {Reputation.Get()} reputation!");
                        break;
                    case "energy":
                        if (Energy.Get() <= 1)
                            Console.WriteLine($"You need to sleep, you have {Energy.Get()} energy!");
                        else
                            Console.WriteLine($"You currently have {Energy.Get()} energy!");
                        break;
                    case "inventory":
                        Inventory.DisplayInventory();
                        break;
                    case "dump":
                        if (currentRoom?.RoomName == "Recycling Station")
                            TrashDump.Dump();
                        else
                            Console.WriteLine("You are not in the recycling station!");
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
                case "Sri": case "sri": case "Sri Lanka": case "sri lanka": case "Srilanka": case "srilanka": case "Lanka": case "lanka":
                    if(currentLocation != sriLanka)
                    {
                        Console.Clear();
                        Console.WriteLine("You're on your way to Sri Lanka... \n\n\n");
                        Console.WriteLine("You arrived in Sri Lanka!");
                        currentLocation = sriLanka;
                        currentRoom = sriLanka?.Rooms["port"];
                    } else
                    {
                        Console.WriteLine("You're already in Sri Lanka!");
                    }
                break;
                case "Sea": case "sea":
                    if(currentLocation != startingLocation)
                    {
                        Console.Clear();
                        Console.WriteLine("You're travelling back to the big blue sea...");
                        currentLocation = startingLocation;
                    } else
                    {
                        Console.WriteLine("You're already at sea!");
                    }
                break;
                default: Console.WriteLine("You sure that place exists?");
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
            Console.Clear(); // -- Works after upgrading to .NET 8.0 (Was set to 6.0 previously)
            ColorWriteLine("Welcome to EcoQuest!", ConsoleColor.Blue);
            Console.WriteLine();
            
            // [Insert game lore here]
            // @Gene to the rescue right there
            Console.WriteLine("You are on a boat, at sea.");
            Console.WriteLine("Type 'sail' to choose your destination.");
            // Felt like all the 'help' stuff was overcluttering the console in the beginning.
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
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }

// Temporary console styling methods
        private static void ColorWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            System.Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}