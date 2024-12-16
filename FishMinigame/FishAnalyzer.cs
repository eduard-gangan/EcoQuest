using Spectre.Console;
namespace EcoQuest;

public static class FishAnalyzer
{
    public static List<Fish> FishSpecies =
    [
        //First fish selection
        new Fish(false, FishStrings.YellowfinTuna, "Yellow Fin Tuna", "./FishMinigame/images/yellow_fin_tuna.png", "Species #001"),
        new Fish(true, FishStrings.Shark , "Shark", "./FishMinigame/images/shark.png", "Species #002"),
        new Fish(false, FishStrings.Goldfish, "Goldfish", "./FishMinigame/images/goldfish.png", "Species #003"),
        new Fish(false, FishStrings.Jellyfish, "Jellyfish", "./FishMinigame/images/jellyfish.png", "Species #004"),
        //Second fish selection
        new Fish(false, FishStrings.Pufferfish, "Pufferfish", "./FishMinigame/images/pufferfish.png", "Species #005"),
        new Fish(true, FishStrings.BlueWhale, "Blue Whale", "./FishMinigame/images/bluewhale.png", "Species #006"),
        new Fish(false, FishStrings.DoryFish, "Dory Fish (Regal Tang)", "./FishMinigame/images/dory.png", "Species #007"),
        new Fish(false, FishStrings.Sardines, "Sardines", "./FishMinigame/images/sardines.png", "Species #008"),
        // Third fish selection
        new Fish(false, FishStrings.Shrimp, "Shrimp", "./FishMinigame/images/shrimp.png", "Species #009"),
        new Fish(false, FishStrings.Clams, "Clam", "./FishMinigame/images/clam.png", "Species #010"),
        new Fish(true, FishStrings.SeaTurtle, "Sea Turtle", "./FishMinigame/images/turtle.png", "Species #011"),
        new Fish(false, FishStrings.Seahorse, "Sea Horse", "./FishMinigame/images/seahorse.png", "Species #012"),
        //Forth fish selection
        new Fish(false, FishStrings.MantaRays, "Manta Ray", "./FishMinigame/images/mantaray.png", "Species #013"),
        new Fish(false, FishStrings.GreenMorayEel, "Green Moray Eel", "./FishMinigame/images/eel.png", "Species #014"),
        new Fish(false, FishStrings.SlenderGrouper, "Slender Grouper", "./FishMinigame/images/grouper.png", "Species #015"),
        new Fish(true, FishStrings.BluefinTuna, "Blue Fin Tuna", "./FishMinigame/images/blue_fin_tuna.png", "Species #016")
    ];

    public static void Play()
    {
        AnsiConsole.Clear();

        Welcome();

        //Fish selection
        Fish first = FishSelection(FishSpecies[0], FishSpecies[1], FishSpecies[2], FishSpecies[3]);
        Fish second = FishSelection(FishSpecies[4], FishSpecies[5], FishSpecies[6], FishSpecies[7]);
        Fish third = FishSelection(FishSpecies[8], FishSpecies[9], FishSpecies[10], FishSpecies[11]);
        Fish fourth = FishSelection(FishSpecies[12], FishSpecies[13], FishSpecies[14], FishSpecies[15]);

        //Mini game ending
        AnsiConsole.Write(
            new FigletText("Congrats!")
                .LeftJustified()
                .Color(Color.Cyan1));
        System.Console.WriteLine();
        var table_congrats = new Table();
        table_congrats.AddColumn("You have found and tagged all endagered species!\n");
        var table_endangered_species = new Table();

        table_endangered_species.AddColumn(first.Name);
        table_endangered_species.AddColumn(second.Name);
        table_endangered_species.AddColumn(third.Name);
        table_endangered_species.AddColumn(fourth.Name);


        table_endangered_species.AddRow(first.Image, second.Image, third.Image, fourth.Image);
        AnsiConsole.Write(table_congrats);

        AnsiConsole.Write(table_endangered_species);
        System.Console.WriteLine();
        AnsiConsole.Prompt(
                         new SelectionPrompt<string>()
                            .Title("")
                            .PageSize(15)
                            .AddChoices([
                                "[green]Continue [/]",
                            ]));
    }
    public static void Welcome()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
                 new FigletText("<Life-form Analyzer>")
                 .LeftJustified()
                 .Color(Color.Cyan1));
        var table = new Table();
        table.AddColumn("This is the Life-form Analyzer machine.\nYou will be presented with a few species of underwater creatures.\nYou are tasked with identifying the endangered species and tag them in order to track their movements in order to protect them from the threats they are facing.");
        AnsiConsole.Write(table);
        AnsiConsole.Write(
                new FigletText("Instructions:")
                .LeftJustified()
                .Color(Color.Cyan1));
        var table2 = new Table();
        table2.AddColumn("Use arrow keys to scroll through the menu.\nPress ENTER to select the species you think needs to be tracked.");
        AnsiConsole.Write(table2);
        AnsiConsole.WriteLine();

        AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("")
            .PageSize(30)
            .AddChoices(new[] {
                        "Continue"

            }));
        AnsiConsole.Clear();

    }
    public static Fish FishSelection(Fish first, Fish second, Fish third, Fish fourth)
    {
        var firstImage = new CanvasImage(first.ImagePath);
        var secondImage = new CanvasImage(second.ImagePath);
        var thirdImage = new CanvasImage(third.ImagePath);
        var fourthImage = new CanvasImage(fourth.ImagePath);
        Fish selectedFish;


        while (true)
        {
            AnsiConsole.Write(
                 new FigletText("<Life-form Analyzer>")
                 .LeftJustified()
                 .Color(Color.Cyan1));

            var table = new Table();

            table.AddColumn(first.ID);
            table.AddColumn(second.ID);
            table.AddColumn(third.ID);
            table.AddColumn(fourth.ID);
            table.AddRow(firstImage, secondImage, thirdImage, fourthImage);


            AnsiConsole.Write(table);


            var answer = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which species needs to be [red]tagged and tracked[/]?")
                    .PageSize(30)
                    .AddChoices(new[] {
                        $"{first.ID}", $"{second.ID}", $"{third.ID}", $"{fourth.ID}",

                    }));
            // Converting string to fish :D
            selectedFish = FishSpecies.Find(fish => fish.ID == answer);
            if (selectedFish == null)
            {
                throw new Exception("Fish does not exist in list.");
            }

            if (selectedFish.Endangered)
            {
                System.Console.WriteLine();
                AnsiConsole.Prompt(
                                 new SelectionPrompt<string>()
                                    .Title($"[green]Correct! Successfully tagged. [/] \n {selectedFish.Description}")
                                    .PageSize(15)
                                    .AddChoices([
                                        "[green]Continue [/]",
                                    ]));
                AnsiConsole.Clear();
                return selectedFish;
            }
            else
            {
                AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                    .Title($"[yellow]Try again! [/] \n The life form analyzer reveals this is [yellow] {selectedFish.Name}[/]. \n{selectedFish.Description}")
                    .PageSize(15)
                    .AddChoices([
                        "[green]Continue [/]",
                    ]));
                selectedFish.ID = selectedFish.Name;

                AnsiConsole.Clear();


            }
        }

    }
}

