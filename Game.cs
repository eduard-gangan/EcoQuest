using System;
using System.Formats.Asn1;
using System.Security.Claims;
using System.Transactions;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;

namespace EcoQuest;

public class Game
{
    public static Room? currentRoom;
    private Room? previousRoom;
    private Location? currentLocation;
    public Start? startingLocation;
    public SriLanka? sriLanka;
    public Australia? australia;
    public Indonesia? indonesia;
    public bool ContinuePlaying { get; set; } = true;

    public void Play()
    {
        Parser parser = new();
        CreateNpcs();

        sriLanka = new SriLanka("Sri Lanka", "You arrive in Sri Lanka. Once a vibrant paradise, the beaches now struggle under the weight of plastic and waste, highlighting the urgent need to combat pollution and protect marine life.", 0);
        indonesia = new Indonesia("Indonesia", "You arrive in Indonesia, where overfishing has taken its toll on the ocean. Perhaps understanding the movements of vulnerable fish could help create safe zones for their recovery.", 10000);
        australia = new Australia("Australia", "You arrive in Australia, unsure of your purpose here. The fading Great Barrier Reef comes to mind—the coral is bleaching, and marine life is vanishing. ", 30000);
        startingLocation = new Start(
            "Ship",
            "Somewhere at sea..",
            0,
            sriLanka,
            indonesia,
            australia
        );

        currentLocation = startingLocation;

        PrintWelcome();

        while (ContinuePlaying)
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
                AnsiConsole.MarkupLine("[bold red]I don't know that command.[/]");
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
                        AnsiConsole.MarkupLine("[bold red]You can't go back from here![/]");
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
                case "descend":
                    if (currentRoom == indonesia.Rooms["submarinedock"])
                    {
                        currentRoom = indonesia.Rooms["submarine"];
                        previousRoom = null;
                        DisplayRoomInformation();

                    }
                    else
                    {
                        System.Console.WriteLine("You can not use this command here. Go to the Submarine Dock.");
                    }
                    break;
                case "ascend":
                    if (currentRoom == indonesia.Rooms["submarine"])
                    {
                        currentRoom = indonesia.Rooms["submarinedock"];
                        previousRoom = null;
                        DisplayRoomInformation();
                    }
                    else
                    {
                        System.Console.WriteLine("You can not use this command here. You have to be in the Submarine.");
                    }
                    break;
                case "analyze":
                    if (currentRoom == indonesia.Rooms["submarine"])
                    {
                        FishAnalyzer.Play();
                    }
                    else
                    {
                        System.Console.WriteLine("You can not use this command here. You have to be in the Submarine.");
                    }
                    break;
                case "sail":
                    if (!QuestSriLanka.Active || !QuestIndonesia.Active)
                    {
                        if (
                            currentLocation == startingLocation
                            || (currentRoom?.RoomName?.Contains("Port") ?? false)
                        )
                        {
                            Console.Clear();
                        Restart:
                            var choice = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("Where do you want to sail?")
                                    .PageSize(10)
                                    .MoreChoicesText(
                                        "[grey](Move up and down to reveal more options)[/]"
                                    )
                                    .AddChoices(new[] { "Sri Lanka", "Indonesia", "Australia" })
                            );
                            if (choice == "Indonesia" && Reputation.Get() < indonesia?.ReputationReq)
                            {
                                AnsiConsole.MarkupLine($"[bold red]You must have {indonesia?.ReputationReq} reputation to sail to Indonesia![/]");
                                goto Restart;
                            }
                            else if (choice == "Australia" && Reputation.Get() < australia?.ReputationReq)
                            {
                                AnsiConsole.MarkupLine($"[bold red]You must have {australia?.ReputationReq} reputation to sail to Australia![/]");
                                goto Restart;
                            }
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
                    ContinuePlaying = false;
                    break;
                case "help":
                    PrintHelp();
                    break;
                case "map":
                    if (currentLocation == sriLanka)
                        MapSriLanka.ShowMap(currentRoom);
                    else if (currentLocation == indonesia)
                        MapIndonesia.ShowMap(currentRoom);
                    else if (currentLocation == australia)
                        MapAustralia.ShowMap(currentRoom);
                    break;
                case "reputation":
                    AnsiConsole.MarkupLine($"You currently have [bold green]{Reputation.Get()}[/] reputation!");
                    break;
                case "inventory":
                    Inventory.DisplayInventory();
                    break;
                case "dump":
                    TrashDump.Dump(currentRoom);
                    break;
                case "sort":
                    TrashMinigame.Start(currentRoom);
                    break;
                case "fishsort":
                    FishAnalyzer.Play();
                    break;
                case "showcase":
                    Reputation.Add(1000000);
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
                case "pick":
                    Credits.AddPickCommand();
                    Trash.Pick(currentRoom, sriLanka);
                    break;
                case "read":
                    Read.Start(currentRoom);
                    break;
                default:
                    break;
            }
        }
        Console.ResetColor();
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
                        AnsiConsole.MarkupLine("[bold red]You're already in Sri Lanka !");
                    }
                    break;
                case "Indonesia":
                    if (currentLocation != indonesia)
                    {
                        currentLocation = indonesia;
                        currentRoom = indonesia?.Rooms["port"];
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]You're already in Indonesia !");
                    }
                    break;
                case "Australia":
                    if (currentLocation != australia)
                    {
                        currentLocation = australia;
                        currentRoom = australia?.Rooms["port"];
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]You're already in Australia !");
                    }
                    break;
            }

            Console.Clear();
            Console.WriteLine($"You're on your way to {currentLocation.LocationName} \n");
            AnsiConsole.Progress()
                .Columns(new ProgressColumn[]
                {
                    new ProgressBarColumn(),
                })
                .Start(ctx =>
                {
                    var Task = ctx.AddTask("SailingTask", autoStart: false).IsIndeterminate(true);
                    Thread.Sleep(1500);
                    Task.StartTask();
                    Task.IsIndeterminate(false);
                    Task.Increment(100);
                });
            Console.Clear();
            var rule = new Rule($"[bold][[{currentLocation.LocationName}]][/]").LeftJustified();
            AnsiConsole.Write(rule);
            AnsiConsole.MarkupLine($"{currentLocation.LocationDescription}");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey37]Type 'look' to see you what's around you.[/]");
            AnsiConsole.MarkupLine("[grey37]Navigate by typing 'north', 'south', 'east', or 'west'.\n[/]");
            AnsiConsole.MarkupLine("[grey37]Press any key to step ashore..[/]");

            bool Continue = false;
            while (!Continue)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    Continue = true;
                }

            }
            DisplayRoomInformation();
        }
        else
        {

            Console.WriteLine($"You are already in {currentLocation.LocationName}!");
        }
    }

    private void Move(string direction) // Use the Move method to move inside locations
    {
        if (currentRoom?.Exits.ContainsKey(direction) == true)
        {
            previousRoom = currentRoom;
            currentRoom = currentRoom?.Exits[direction];
            DisplayRoomInformation();
        }
        else
        {
            Console.WriteLine($"You can't go {direction}!");
        }
    }


    public static void DisplayRoomInformation()
    {
        Console.Clear();
        var rule = new Rule($"[bold][[{currentRoom.RoomName}]][/]").LeftJustified();
        AnsiConsole.Write(rule);

        Console.WriteLine(currentRoom?.RoomDescription);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"[Suggested Commands]: ");
        foreach (string commands in currentRoom.AvailableCommands)
        {
            Console.Write($"{commands} ");
        }

        Console.WriteLine();
        Console.ResetColor();
    }
    private static void PrintWelcome()
    {
        Console.Clear();
        ConsoleMethods.SlowWrite(
            "You are aboard a research vessel, drifting along calm blue waters under an open sky.\nThe ship gently rocks, its deck bustling with equipment, nets, sonar tools, oxygen tanks, and more. \n\nAs an aspiring marine biologist, you're on your first expedition.\nThe salty air and distant cry of seagulls fill you with excitement for the adventure that awaits.\n\nTo the helm of the ship stands Captain Sylvia Earle, a legendary oceanographer, explorer, and marine biologist\nwith a lifetime of experience beneath the waves.\nHer sharp and thoughtful eyes reflect countless voyage and experience for being at sea.\nHer weathered face and confident stance presents the wisdom of someone who has spent decades charting unknown\nwaters and fighting tirelessly to protect marine ecosystems.\nAs her hands rest firmly on the ship's wheel, steadily guiding the vessel, she can't help but notice you staring\nat her, and decides to approach.\n \n",
            1,
            1
        );
        AnsiConsole.MarkupLine("[grey37]Press any key to continue...[/]");
        Console.ReadKey();
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        ConsoleMethods.SlowWrite("*Captain Sylvia approaches, her voice, steady and inspiring*\n", 25, 35);
        Console.ResetColor();
        ConsoleMethods.SlowWrite(
            "Welcome aboard explorer! The ocean faces grave threats.\nPollution, overfishing, warming waters, and habitat destruction, but it's not too late to act.\nI've spent a lifetime beneath the waves, witnessing both devastation and resilience.\nNow, it's your turn. The United Nations calls us to action through Goal 14: Life Below Water, a mission to restore and protect our Ocean.\nEvery action, no matter how small, creates ripples of change. With passion and persistence, we can bring life back to these waters.\nSo, are you ready to dive in and be the hero the ocean needs? Let's make waves for a better future.\n",
            25,
            35
        );
        AnsiConsole.MarkupLine("[grey37]Press any key to continue...[/]");
        Console.ReadKey();
        Console.Clear();

        Console.WriteLine("Type 'sail' to choose your destination.");
        AnsiConsole.MarkupLine("[grey37]Type 'help' to see a list of available commands.[/]");
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
        Console.WriteLine("  Type 'reputation' to see your reputation.");
        Console.WriteLine("  Type 'read' in the library to open up a book.");
        Console.WriteLine("  Type 'inventory' to see your inventory.");
        Console.WriteLine("  Type 'dump' to dump your trash.");
        Console.WriteLine("  Type 'sort' to sort your trash.");
        Console.WriteLine("  Type 'talk' to talk to an NPC.");
        Console.WriteLine("  Type 'map' to view the game map.");
        Console.WriteLine("  Type 'descend' to go down to the submarine while on the Submarine Dock.");
        Console.WriteLine("  Type 'ascend' to get out of the submarine.");
        Console.WriteLine("  Type 'analyze' when you are on the submarine to use the Life Form Analyzer.");
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
            () => ConsoleMethods.SlowWrite(NpcReply.GARRY_NAME)
        );
        NPCs.Garry.MainDialogue.AddOption(
            PlayerReply.GARRY_BACKSTORY,
            () => ConsoleMethods.SlowWrite(NpcReply.GARRY_BACKSTORY)
        );
        NPCs.Garry.MainDialogue.AddOption(
            PlayerReply.GARRY_WHY,
            () => ConsoleMethods.SlowWrite(NpcReply.GARRY_WHY)
        );
        NPCs.Garry.MainDialogue.AddOption(
            PlayerReply.GARRY_QUEST,
            () =>
            {
                ConsoleMethods.SlowWrite(NpcReply.GARRY_QUEST);
                System.Console.WriteLine();
                Inventory.InventoryCapacity = 5;
                QuestSriLanka.start();
                AnsiConsole.MarkupLine("[rapidblink purple]Your inventory space has been increased to 5![/]");
                NPCs.Garry.MainDialogue.RemoveOption(PlayerReply.GARRY_QUEST);
                NPCs.Garry.MainDialogue.TriggerDialogue();
            }
        );
        NPCs.Garry.MainDialogue.AddOption(
            PlayerReply.BYE,
            () =>
            {
                ConsoleMethods.SlowWrite(NpcReply.GARRY_BYE);
                NPCs.Garry.MainDialogue.TriggerDialogue();
                DisplayRoomInformation();
            }
        );

        //Larry
        NPCs.Larry.MainDialogue.AddOption(
            PlayerReply.LARRY_NAME,
            () => ConsoleMethods.SlowWrite(NpcReply.LARRY_NAME)
        );
        NPCs.Larry.MainDialogue.AddOption(
            PlayerReply.LARRY_BACKSTORY,
            () => ConsoleMethods.SlowWrite(NpcReply.LARRY_BACKSTORY)
        );
        NPCs.Larry.MainDialogue.AddOption(
            PlayerReply.LARRY_PLAYER,
            () => ConsoleMethods.SlowWrite(NpcReply.LARRY_PLAYER)
        );
        NPCs.Larry.MainDialogue.AddOption(
            PlayerReply.LARRY_BYE,
            () =>
            {
                ConsoleMethods.SlowWrite(NpcReply.LARRY_BYE);
                NPCs.Larry.MainDialogue.TriggerDialogue();
                DisplayRoomInformation();
            }
        );

        //Mayor Lanka
        NPCs.Lanka.MainDialogue.AddOption(
            PlayerReply.LANKA_PLAYER,
            () =>
            {
                ConsoleMethods.SlowWrite(NpcReply.LANKA_PLAYER);
                NPCs.Lanka.MainDialogue.InsertOption(
                    PlayerReply.LANKA_STATION,
                    () =>
                    {
                        if (Reputation.Get() >= 500)
                        {
                            ConsoleMethods.SlowWriteLine(NpcReply.LANKA_STATION_YES);
                            TrashMinigame.RepairRecyclingStation();
                            AnsiConsole.MarkupLine("[grey37]The Recycling Station is now functional, use command (sort) to sort items while in the Recycling Station[/]");
                            sriLanka.Rooms["station"].ChangeRoomDescription("You approach the restored building. The recycling station is now fully operational, with organized bins separating materials and machinery humming as it processes waste. Sunlight streams through repaired windows, illuminating clean, well-maintained equipment. The station stands as a symbol of renewal, running smoothly and efficiently..\n[NPC]\nYou see Larry again, he seems very happy to see you, maybe you should go talk to him.");
                            NPCs.Larry.ChangeGreeting(NpcReply.LARRY_GREETING2);
                            NPCs.Larry.MainDialogue.OptionList = [];
                            NPCs.Larry.MainDialogue.DialogueOptions = [];
                            NPCs.Larry.MainDialogue.AddOption(PlayerReply.BYE, () => { ConsoleMethods.SlowWriteLine(NpcReply.LARRY_BYE2); NPCs.Larry.MainDialogue.TriggerDialogue(); });
                            NPCs.Lanka.MainDialogue.RemoveOption(PlayerReply.LANKA_STATION);
                            NPCs.Lanka.MainDialogue.RemoveOption(PlayerReply.LANKA_PLAYER);
                            NPCs.Lanka.MainDialogue.RemoveOption(PlayerReply.BYE);
                            NPCs.Lanka.MainDialogue.AddOption(PlayerReply.BYE, () => { ConsoleMethods.SlowWriteLine(NpcReply.LANKA_BYE2); NPCs.Lanka.MainDialogue.TriggerDialogue(); });
                            NPCs.Lanka.ChangeGreeting(NpcReply.LANKA_GREETING2);
                            sriLanka.Rooms["townhall"].ChangeRoomDescription("You are at the Town Hall, you can see the mayor's office. His assistant lets you in.");
                        }
                        else
                        {
                            ConsoleMethods.SlowWriteLine(NpcReply.LANKA_STATION_NO);
                            AnsiConsole.MarkupLine("[grey37]You need 500 reputation to repair the recycling station[/]");
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
                ConsoleMethods.SlowWrite(NpcReply.LANKA_BYE);
                NPCs.Lanka.MainDialogue.TriggerDialogue();
                DisplayRoomInformation();
            }
        );
        //Indonesia NPC
        NPCs.Andrew.MainDialogue.AddOption(PlayerReply.ANDREW_1, () =>
        {
            ConsoleMethods.SlowWriteLine(NpcReply.ANDREW_11);
            ConsoleMethods.SlowWriteLine(NpcReply.ANDREW_12);
            NPCs.Andrew.MainDialogue.RemoveOptionAt(0);
            NPCs.Andrew.MainDialogue.AddOption(PlayerReply.ANDREW_2, () =>
            {
                ConsoleMethods.SlowWriteLine(NpcReply.ANDREW_21);
                ConsoleMethods.SlowWriteLine(NpcReply.ANDREW_22);
                ConsoleMethods.SlowWriteLine(NpcReply.ANDREW_23);
                NPCs.Andrew.MainDialogue.RemoveOptionAt(0);
                NPCs.Andrew.MainDialogue.AddOption(PlayerReply.ANDREW_3, () =>
                {
                    ConsoleMethods.SlowWrite(NpcReply.ANDREW_3);
                    NPCs.Andrew.MainDialogue.RemoveOptionAt(0);
                    NPCs.Andrew.MainDialogue.AddOption(PlayerReply.BYE, () =>
                {
                    ConsoleMethods.SlowWriteLine(NpcReply.ANDREW_BYE);
                    NPCs.Andrew.MainDialogue.TriggerDialogue();
                    QuestIndonesia.Start();
                    AnsiConsole.MarkupLine("[gray]Head North to the Submarine Dock and use command 'descend' to enter the Research Submarine and descend into the Ocean.[/]");
                });

                });


            });

        });
        // Captain Sylvia
        NPCs.Captain.MainDialogue.AddOption(PlayerReply.CAPTAIN_WHAT, () =>
        {
            ConsoleMethods.SlowWrite(NpcReply.CAPTAIN_WHAT);
        });
        NPCs.Captain.MainDialogue.AddOption(PlayerReply.CAPTAIN_WHY, () =>
       {
           ConsoleMethods.SlowWrite(NpcReply.CAPTAIN_WHY);
       });
        NPCs.Captain.MainDialogue.AddOption(PlayerReply.CAPTAIN_TURNOVER, () =>
       {
           ConsoleMethods.SlowWrite(NpcReply.CAPTAIN_TURNOVER);
       });
        NPCs.Captain.MainDialogue.AddOption(PlayerReply.CAPTAIN_TEST, () =>
        {
            ConsoleMethods.SlowWrite(NpcReply.CAPTAIN_TEST);
            NPCs.Captain.MainDialogue.InsertOption("Start Quiz", () =>
            {
                Quiz.Play(currentRoom);
            }, NPCs.Captain.MainDialogue.OptionList.Count - 1);
        });
        NPCs.Captain.MainDialogue.AddOption(PlayerReply.BYE, () =>
        {
            NPCs.Captain.MainDialogue.TriggerDialogue();
            DisplayRoomInformation();
        });
    }
}