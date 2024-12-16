using Spectre.Console;
namespace EcoQuest;

public static class FishSortingMinigame
{
    public static List<Fish> FishSpecies =
    [
        //First fish selection
        new Fish(false, "this species needs to be tracked", "Winter skate", "./images/winterskate.png", "Species #001"),
        new Fish(true, "this species needs to be tracked", "Shark", "./images/shark.png", "Species #002"),
        new Fish(false, "this species needs to be tracked", "Orange fish", "./images/orangefish.png", "Species #003"),
        new Fish(false, "this species needs to be tracked", "Eel", "./images/eel.png", "Species #004"),
        //Second fish selection
        new Fish(false, "this species needs to be tracked", "Grouper", "./images/grouper.png", "Species #005"),
        new Fish(true, "this species needs to be tracked", "Blue Whale", "./images/bluewhale.png", "Species #006"),
        new Fish(false, "this species needs to be tracked", "Conch", "./images/conch.png", "Species #007"),
        new Fish(false, "this species needs to be tracked", "Tuna", "./images/tuna.png", "Species #008")

    ];

    public static void Play()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(
    new FigletText("Tag n' Track!")
        .LeftJustified()
        .Color(Color.Cyan1));

        Fish first = FishSelection(FishSpecies[0], FishSpecies[1], FishSpecies[2], FishSpecies[3]);
        Fish second = FishSelection(FishSpecies[4], FishSpecies[5], FishSpecies[6], FishSpecies[7]);
        Fish third = FishSelection(FishSpecies[0], FishSpecies[1], FishSpecies[2], FishSpecies[3]);
        Fish fourth = FishSelection(FishSpecies[0], FishSpecies[1], FishSpecies[2], FishSpecies[3]);

        AnsiConsole.Write(
            new FigletText("Congrats!")
                .LeftJustified()
                .Color(Color.Cyan1));
        System.Console.WriteLine();
        AnsiConsole.Write(new Markup("You have found and tagged all endagered species!\n"));
        Table table = new Table();

        table.AddColumn(first.Name);
        table.AddColumn(second.Name);
        table.AddColumn(third.Name);
        table.AddColumn(fourth.Name);

        var firstImage = new CanvasImage(first.ImagePath);
        var secondImage = new CanvasImage(second.ImagePath);
        var thirdImage = new CanvasImage(third.ImagePath);
        var fourthImage = new CanvasImage(fourth.ImagePath);

        table.AddRow(firstImage, secondImage, thirdImage, fourthImage);

        AnsiConsole.Write(table);
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
                                    .Title($"[green]Correct! [/] \n {selectedFish.Description}")
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

