using System;

namespace EcoQuest
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private Location? currentLocation;
        public Start startingLocation;
        public SriLanka sriLanka;
        public Australia australia;
        public Indonesia indonesia;

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
                Console.WriteLine(currentRoom?.RoomName);
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
                        Console.WriteLine("Where do you want to sail ?");
                        Console.WriteLine("(Sri Lanka, Sea)");
                        Sail(Console.ReadLine());
                        break;

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
                        ColorWriteLine($"You currently have {Money.Get()} EcoCoins in your wallet!", ConsoleColor.Green);
                        break;
                    case "reputation":
                        Console.WriteLine($"You currently have {Reputation.Get()} reputation!");
                        break;
                    case "energy":
                        if (Energy.Get() <= 1)
                        {
                            Console.WriteLine($"You need to sleep, you have {Energy.Get()} energy!");
                        }
                        else
                        {
                            Console.WriteLine($"You currently have {Energy.Get()} energy!");
                        }
                        break;
                    case "inventory":
                        Inventory.DisplayInventory();
                        break;

                    default:
                        Console.WriteLine("I don't know that command.");
                        break;
                }
            }
            Console.ResetColor();
            Console.WriteLine("Thank you for playing EcoQuest");
        }

        private void Sail(string destination) //Sail will be used to move between locations
        {
            switch (destination)
            {
                case "sri":
                case "sri lanka":
                case "srilanka":
                    if(currentLocation != sriLanka)
                    {
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");
                        Console.WriteLine("You're on your way to Sri Lanka... \n\n\n");
                        Console.WriteLine("You arrived in Sri Lanka !");
                        currentLocation = sriLanka;
                        currentRoom = sriLanka.Rooms["port"];
                    } else
                    {
                        Console.WriteLine("You're already in Sri Lanka !");
                    }
                break;
                case "sea":
                    if(currentLocation != startingLocation)
                    {
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");
                        Console.WriteLine("You're travelling back to the big blue sea...");
                        currentLocation = startingLocation;
                    } else
                    {
                        Console.WriteLine("You're already at sea !");
                    }
                break;
                default: Console.WriteLine("You sure that place exists ?");
                break;
            }
        }

        private void Move(string direction) //Move will be used inside of locations
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
            Console.Clear(); //This doesn't work properly
            Console.WriteLine("\x1b[3J"); //For some reason this clears the console, I found this on stackoverflow -V
            ColorWriteLine("Welcome to EcoQuest!", ConsoleColor.Blue);

            Console.WriteLine("EcoQuest is a new, incredibly interesting adventure game.");

            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are at the Starting Point,"); //needs to be changed
            Console.WriteLine("go do some shit."); // also needs to be changed :))
            Console.WriteLine();
            Console.WriteLine("Type 'sail' to go to another destination");
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'balance' to see how many EcoCoins you have.");
            Console.WriteLine("Type 'reputation' to see your reputation.");
            Console.WriteLine("Type 'energy' to see your energy levels.");
            Console.WriteLine("Type 'inventory' to see your inventory.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }

        //Bro i don't even know why I added this, it's cool i guess
        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            System.Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
