/*
--/ Class for the Quiz Minigame \--

Overview:
 In this minigame, players receive a series of random questions that they have to answer.
 If they answer incorrectly, they are forced out of the minigame and have to start over.


 - Use the Play() method to initiate the quiz minigame.

Key Variables:
 - questionList: A list of possible questions. This list is used to remove questions from the list
 so that they would not appear multiple times.

 */
namespace EcoQuest;
using Spectre.Console;
using static Questions;


public static class Quiz
{
    private static List<Question> questionList = [];

    public static void Play(Room currentRoom)
    {

        // End the Dialogue
        NPCs.Captain.MainDialogue.TriggerDialogue();

        Console.Clear();

        // Reset Available Questions
        questionList = new List<Question>(List);

        // Prompt with a number of questions
        for (int i = 1; i <= 8; i++)
        {
            bool Continue = Prompt(i);
            if (!Continue)
            {
                Game.DisplayRoomInformation(currentRoom);
                return;
            }
        }

        // If you answer everything correctly then..
        // The captain writes some encouraging words.
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold white]You did it champ![/]");
        AnsiConsole.MarkupLine("[bold white][[Insert some more words here]][/]");
        AnsiConsole.MarkupLine("[grey37]Press any key to end the game...[/]");

        bool end = false;
        while (!end)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                end = true;
                Console.Clear();
            }

        }

        // Trigger the game ending.
        Credits.EndGame();
    }

    private static bool Prompt(int streak)
    {
        // Random question
        Random rnd = new Random();
        Question question = questionList[rnd.Next(0, questionList.Count)];

        // Remove the question from the current questions list, so that it couldn't be used in the future.
        questionList.Remove(question);

        // Prompt the question
        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title(question.QuestionName)
                                .AddChoices(question.Choices)
        );

        // Validate answer
        if (choice == question.Answer)
        {
            AnsiConsole.MarkupLine($"[bold green]Correct! [/][bold grey37][[{streak}/8]][/]");
            return true;
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Incorrect![/]");
            if (streak == 1) AnsiConsole.MarkupLine($"[bold grey37]Answered [[{streak - 1}/8]] correctly![/]");
            AnsiConsole.MarkupLine("[grey37]Come back here after doing some research in the library[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey37]Press any key to continue...[/]");

            bool end = false;
            while (!end)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    end = true;
                    Console.Clear();
                }

            }
            return false;
        }
    }
}