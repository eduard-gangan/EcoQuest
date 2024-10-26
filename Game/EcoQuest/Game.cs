namespace EcoQuest
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;

        public Game()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
            //I think we need to make another class for the Locations such as Sri Lanka, instead of using the same room system. -V
            Room? start = new("Starting Room", "Sri Lanka to the East."); // This is for reference only, needs to be changed -V
            Room? srilanka_boat = new("Sri Lanka", "You just arrived at Sri Lanka. Get off the boat and go to the port.");
            // I'm still thinking about this, so it's not final 
            Room? srilanka_port = new("Port", "You are at the central port. To the east is a very dirty and polluted beach, to the north there is a recycling station, and to the west is the Town Hall.");
            Room? srilanka_beach = new("Polluted Beach", "You are walking through piles of trash. Supposedly you are on a beach.");
            Room? srilnaka_townhall = new("Town Hall", "You are at the Town Hall, you can see the mayor's office but the door is closed and you're not allowed in, he doesn't speak to no names.");
            Room? srilanka_recycling_station = new("Recycling Station", "You are at the Recycling Station, currently it is not working.");

            // Room? pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
            // Room? lab = new("Lab", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
            // Room? office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

            start.SetExits(null, srilanka_boat, null, null); // North, East, South, West

            srilanka_boat.SetExits(srilanka_port, null, null, start);
            srilanka_port.SetExits(srilanka_recycling_station, srilanka_beach, srilanka_boat, srilnaka_townhall);
            srilanka_beach.SetExit("west", srilanka_port);
            srilanka_recycling_station.SetExit("south", srilanka_port);
            srilnaka_townhall.SetExit("east", srilanka_port);
            // theatre.SetExit("west", outside);

            // pub.SetExit("east", outside);

            // lab.SetExits(outside, office, null, null);

            // office.SetExit("west", lab);

            currentRoom = start;
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
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
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch (command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.LongDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
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

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing EcoQuest");
        }

        private void Move(string direction)
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
            Console.WriteLine("Welcome to EcoQuest!");
            Console.WriteLine("EcoQuest is a new, incredibly interesting adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are at the Starting Point,"); //needs to be changed
            Console.WriteLine("go do some shit."); // also needs to be changed :))
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}
