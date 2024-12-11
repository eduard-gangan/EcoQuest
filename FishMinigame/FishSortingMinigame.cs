namespace EcoQuest;
using Spectre.Console;
using Spectre.Console.Cli;


public class FishSortingMinigame
{
    public List<Fish> FishSpieces { get; set; }

    public FishSortingMinigame()
    {
        FishSpieces = new List<Fish>();
        FishSpieces.Add(new Fish(1, " ", "Fish 1"));
        FishSpieces.Add(new Fish(2, " ", "Fish 2"));
        FishSpieces.Add(new Fish(3, " ", "Fish 3"));
        FishSpieces.Add(new Fish(4, " ", "Fish 4"));
        FishSpieces.Add(new Fish(5, " ", "Fish 5"));
        FishSpieces.Add(new Fish(6, " ", "Fish 6"));
        FishSpieces.Add(new Fish(7, " ", "Fish 7"));
        FishSpieces.Add(new Fish(8, " ", "Fish 8"));
        FishSpieces.Add(new Fish(9, " ", "Fish 9"));
        FishSpieces.Add(new Fish(10, " ", "Fish 10"));
    }

    public void Play()
    {
        //randomly 10 times picking 4 fishes and then player is choosing the most endangered species from 4 options through spectre console
        //but it is returning answer as a string (name of the fish which player picked)
        Random rnd = new Random();
        int correctAnswers = 0;
        for (int i = 0; i < 10; i++)
        {
            int first = rnd.Next(0, 10);
            int second = rnd.Next(0, 10);
            while (first == second)
            {
                second = rnd.Next(0, 10);
            }
            int third = rnd.Next(0, 10);
            while (third == first || third == second)
            {
                third = rnd.Next(0, 10);
            }
            int fourth = rnd.Next(0, 10);
            while (fourth == first || fourth == second || fourth == third)
            {
                fourth = rnd.Next(0, 10);
            }

            var answer = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What's the most [red]endangered species[/]?")
                    .PageSize(15)
                    .AddChoices(new[] {
                        $"{FishSpieces[first].Name}", $"{FishSpieces[second].Name}",
                        $"{FishSpieces[third].Name}", $"{FishSpieces[fourth].Name}",
                    }));

            //converting answer from string to Fish
            Fish? fish = null;
            foreach (var k in FishSpieces)
            {
                if (k.Name == answer)
                {
                    fish = k;
                }
            }

            //checking if the answer was right
            //checking if the rarity of the fish was highest from the options
            if (fish.Rarity == Math.Max(FishSpieces[first].Rarity, Math.Max(FishSpieces[second].Rarity,
             Math.Max(FishSpieces[third].Rarity, FishSpieces[fourth].Rarity))))
            {
                correctAnswers++;
                AnsiConsole.Prompt(
                     new SelectionPrompt<string>()
                        .Title($"""
                            Your Answer was right.
                            {fish.Desription}
                            You have {correctAnswers} correct answers.                           
                        """)
                        .PageSize(15)
                        .AddChoices([
                            "Next",
                        ]));
            }
            else
            {
                AnsiConsole.Prompt(
                     new SelectionPrompt<string>()
                        .Title($"""
                            Your Answer was'n right.
                            {fish.Desription}
                            Remember it for later.
                            You have {correctAnswers} correct answers.                           
                        """)
                        .PageSize(15)
                        .AddChoices([
                            "Next",
                        ]));
            }
        }
        //checking if the player had enough correct answers for completing the quest if not he needs to do quiz again
        if (!QuestIndonesia.CheckQuest(correctAnswers))
        {
            AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                    .Title($"You had {correctAnswers} correct answers, it is not enpught. Try it again.")
                    .PageSize(15)
                    .AddChoices([
                        "Try again",
                    ]));
            Play();
        }
    }
}

