using System;
using System.Formats.Asn1;
using System.Security.Claims;
using System.Transactions;
using Spectre.Console;
using Spectre.Console.Cli;

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
            startingLocation = new Start(
                "Ship",
                "Somewhere at sea..",
                0,
                sriLanka,
                australia,
                indonesia
            );

            currentLocation = startingLocation;

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
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
                        Console.WriteLine(previousRoom);
                        if (previousRoom == null)
                        {
                            ColorWriteLine("You can't go back from here!", ConsoleColor.Red);
                        }
                        else
                        {
                            Console.Clear();
                            foreach (KeyValuePair<string, Room> exit in currentRoom.Exits)
                            {
                                if (exit.Value == previousRoom)
                                {
                                    Move(exit.Key);
                                }
                            }
                        }
                        break;
                    case "sail":
                        if (!QuestSriLanka.Active)
                        {
                            if (
                                currentLocation == startingLocation
                                || (currentRoom?.RoomName?.Contains("Port") ?? false)
                            )
                            {
                                Console.Clear();
                                var choice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Where do you want to sail?")
                                        .PageSize(10)
                                        .MoreChoicesText(
                                            "[grey](Move up and down to reveal more options)[/]"
                                        )
                                        .AddChoices(new[] { "Sri Lanka", "Indonesia" })
                                );
                                Sail(choice);
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"You can't depart from {currentRoom?.RoomName}, go to the port."
                                );
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can't sail while you have an active quest.");
                        }
                        break;

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
                            MapSriLanka.ShowMap(currentRoom);
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
                        if (Energy.Get() <= 5)
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
                        Energy.Replenish(currentRoom);
                        break;
                    case "dump":
                        TrashDump.Dump(currentRoom);
                        break;
                    case "sort":
                        TrashMinigame.Start(currentRoom);
                        break;
                    case "talk":
                        if (currentRoom?.RoomNPC != null)
                        {
                            currentRoom?.RoomNPC.Talk();
                        }
                        else
                        {
                            System.Console.WriteLine("There is no one here...");
                        }
                        break;
                    case "pick": //At the moment, the system doesnt take upgrades into account
                        Trash.Pick(currentRoom, sriLanka);
                        break;
                    default:
                        break;
                }
            }
            Console.ResetColor();
            ColorWriteLine("Thank you for playing EcoQuest!", ConsoleColor.Blue);
        }

        private void Sail(string destination) // Use the Sail method to move between locations
        {
            if (currentLocation.LocationName != destination)
            {
                switch (destination)
                {
                    case "Sri Lanka":

                        if (currentLocation != sriLanka)
                        {

                            currentLocation = sriLanka;
                            currentRoom = sriLanka?.Rooms["port"];
                        }
                        else
                        {
                            Console.WriteLine("You're already in Sri Lanka!");
                        }
                        break;
                    // case "Sea":
                    //     if (currentLocation != startingLocation)
                    //     {
                    //         currentRoom = null;
                    //         Console.Clear();
                    //         Console.WriteLine("You're travelling back to the big blue sea... \n");
                    //         RecursiveWrite("...", 5);
                    //         Console.Clear();
                    //         Console.WriteLine("You are at sea!");
                    //         currentLocation = startingLocation;
                    //     }
                    //     else
                    //     {
                    //         Console.WriteLine("You're already at sea!");
                    //     }
                    //     break;
                    case "Indonesia":
                        currentLocation = indonesia;
                        currentRoom = indonesia.Rooms["port"];
                        break;
                }

                Console.Clear();
                Console.WriteLine($"You're on your way to {currentLocation.LocationName} \n");
                RecursiveWrite("...", 5);
                Console.Clear();
                Console.WriteLine($"You arrived in {currentLocation.LocationName}!");
                ColorWriteLine(
                    "Type 'look' to see you what's around you.",
                    ConsoleColor.DarkGray
                );
                ColorWriteLine(
                    "Navigate by typing 'north', 'south', 'east', or 'west'.\n",
                    ConsoleColor.DarkGray
                );
            }
            else
            {
                System.Console.WriteLine($"You are already in {currentLocation.LocationName}!");
            }
        }

        private void Move(string direction) // Use the Move method to move inside locations
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                Console.Clear();
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];

                Console.WriteLine($"[{currentRoom?.RoomName}]");
                Console.WriteLine(currentRoom.RoomDescription);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[Suggested Commands]: ");
                foreach (string commands in currentRoom.AvailableCommands)
                {
                    Console.Write($"{commands} ");
                }
                Console.WriteLine();
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        private static void PrintWelcome()
        {
            Console.Clear();
            ColorWriteLine("            Welcome to EcoQuest!\n", ConsoleColor.Blue);
            SlowWrite(
                "You are aboard a research vessel, drifting along calm blue waters under an open sky.\nThe ship gently rocks, its deck bustling with equipment, nets, sonar tools, oxygen tanks, and more. \n\nAs an aspiring marine biologist, you're on your first expedition.\nThe salty air and distant cry of seagulls fill you with excitement for the adventure that awaits.\n\nTo the helm of the ship stands Captain Sylvia Earle, a legendary oceanographer, explorer, and marine biologist\nwith a lifetime of experience beneath the waves.\nHer sharp and thoughtful eyes reflect countless voyage and experience for being at sea.\nHer weathered face and confident stance presents the wisdom of someone who has spent decades charting unknown\nwaters and fighting tirelessly to protect marine ecosystems.\nAs her hands rest firmly on the ship's wheel, steadily guiding the vessel, she can't help but notice you staring\nat her, and decides to approach.\n \n",
                1,
                1
            );
            ColorWriteLine("Press any key to continue...", ConsoleColor.DarkGray);
            Console.ReadKey();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            SlowWrite("*Captain Sylvia approaches, her voice, steady and inspiring*\n", 25, 35);
            Console.ResetColor();
            SlowWrite(
                "Welcome aboard explorer! The ocean faces grave threats.\nPollution, overfishing, warming waters, and habitat destruction, but it's not too late to act.\nI've spent a lifetime beneath the waves, witnessing both devastation and resilience.\nNow, it's your turn. The United Nations calls us to action through Goal 14: Life Below Water, a mission to restore and protect our Ocean.\nEvery action, no matter how small, creates ripples of change. With passion and persistence, we can bring life back to these waters.\nSo, are you ready to dive in and be the hero the ocean needs? Let's make waves for a better future.\n",
                25,
                35
            );
            ColorWriteLine("Press any key to continue...", ConsoleColor.DarkGray);
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Type 'sail' to choose your destination.");
            ColorWriteLine(
                "Type 'help' to see a list of available commands.",
                ConsoleColor.DarkGray
            );
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("|------------------------------------------------------------|");
            Console.WriteLine("  Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("  Type 'sail' to go to another destination.");
            Console.WriteLine("  Type 'look' for more details.");
            Console.WriteLine("  Type 'back' to go to the previous room.");
            Console.WriteLine("  Type 'balance' to see how many EcoCoins you have.");
            Console.WriteLine("  Type 'reputation' to see your reputation.");
            Console.WriteLine("  Type 'energy' to see your energy levels.");
            Console.WriteLine("  Type 'inventory' to see your inventory.");
            Console.WriteLine("  Type 'dump' to dump your trash.");
            Console.WriteLine("  Type 'sort' to sort your trash.");
            Console.WriteLine("  Type 'talk' to talk to an NPC.");
            Console.WriteLine("  Type 'help' to print this message again.");
            Console.WriteLine("  Type 'quit' to exit the game.");
            Console.WriteLine("|------------------------------------------------------------|");
            Console.ResetColor();
        }

        private void CreateNpcs()
        {
            //Garry
            NPCs.Garry.MainDialogue.AddOption(
                PlayerReply.GARRY_NAME,
                () => SlowWrite(NpcReply.GARRY_NAME)
            );
            NPCs.Garry.MainDialogue.AddOption(
                PlayerReply.GARRY_BACKSTORY,
                () => SlowWrite(NpcReply.GARRY_BACKSTORY)
            );
            NPCs.Garry.MainDialogue.AddOption(
                PlayerReply.GARRY_WHY,
                () => SlowWrite(NpcReply.GARRY_WHY)
            );
            NPCs.Garry.MainDialogue.AddOption(
                PlayerReply.GARRY_QUEST,
                () =>
                {
                    SlowWrite(NpcReply.GARRY_QUEST);
                    Inventory.InventoryCapacity = 5;
                    QuestSriLanka.start();
                    ColorWriteLine(
                        "Your inventory space has been increased to 5!",
                        ConsoleColor.Green
                    );
                    NPCs.Garry.MainDialogue.RemoveOption(PlayerReply.GARRY_QUEST);
                    NPCs.Garry.MainDialogue.TriggerDialogue();
                }
            );
            NPCs.Garry.MainDialogue.AddOption(
                PlayerReply.BYE,
                () =>
                {
                    SlowWrite(NpcReply.GARRY_BYE);
                    NPCs.Garry.MainDialogue.TriggerDialogue();
                }
            );

            //Larry
            NPCs.Larry.MainDialogue.AddOption(
                PlayerReply.LARRY_NAME,
                () => SlowWrite(NpcReply.LARRY_NAME)
            );
            NPCs.Larry.MainDialogue.AddOption(
                PlayerReply.LARRY_BACKSTORY,
                () => SlowWrite(NpcReply.LARRY_BACKSTORY)
            );
            NPCs.Larry.MainDialogue.AddOption(
                PlayerReply.LARRY_PLAYER,
                () => SlowWrite(NpcReply.LARRY_PLAYER)
            );
            NPCs.Larry.MainDialogue.AddOption(
                PlayerReply.LARRY_BYE,
                () =>
                {
                    SlowWrite(NpcReply.LARRY_BYE);
                    NPCs.Larry.MainDialogue.TriggerDialogue();
                }
            );

            //Mayor Lanka
            NPCs.Lanka.MainDialogue.AddOption(
                PlayerReply.LANKA_PLAYER,
                () =>
                {
                    SlowWrite(NpcReply.LANKA_PLAYER);
                    NPCs.Lanka.MainDialogue.InsertOption(
                        PlayerReply.LANKA_STATION,
                        () =>
                        {
                            if (Reputation.Get() >= 0)
                            {
                                SlowWrite(NpcReply.LANKA_STATION_YES);
                                TrashMinigame.RepairRecyclingStation();
                                ColorWriteLine(
                                    "The Recycling Station is now functional, use command (sort) to sort items while in the Recycling Station",
                                    ConsoleColor.DarkGray
                                );
                                NPCs.Lanka.MainDialogue.RemoveOption(PlayerReply.LANKA_STATION);
                                NPCs.Lanka.MainDialogue.RemoveOption(PlayerReply.LANKA_PLAYER);
                            }
                            else
                            {
                                SlowWrite(NpcReply.LANKA_STATION_NO);
                                ColorWriteLine(
                                    "You need 500 reputation to repair the recycling station",
                                    ConsoleColor.DarkGray
                                );
                            }
                        },
                        1
                    );
                }
            );
            NPCs.Lanka.MainDialogue.AddOption(
                "Upgrade",
                () =>
                {
                    Upgrades.Menu();
                }
            );
            NPCs.Lanka.MainDialogue.AddOption(
                PlayerReply.BYE,
                () =>
                {
                    SlowWrite(NpcReply.LANKA_BYE);
                    NPCs.Lanka.MainDialogue.TriggerDialogue();
                }
            );
            //Indonesia NPC
            NPCs.IndonesiaNPC.MainDialogue.AddOption("start the fucking game", () => { System.Console.WriteLine("starting bro."); });
            NPCs.IndonesiaNPC.MainDialogue.AddOption("bye", () => { System.Console.WriteLine("bye bro."); NPCs.IndonesiaNPC.MainDialogue.TriggerDialogue(); });
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

        public static void SlowWrite(string text, int min = 25, int max = 35)
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
                    Thread.Sleep(rnd.Next(min, max));
                }
            }
            Console.WriteLine();
        }

        public static void RecursiveWrite(string text, int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                SlowWrite(text, 150, 250);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }
    }
}
