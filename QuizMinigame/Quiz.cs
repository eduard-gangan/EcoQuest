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
    private static List<Question> questionList = List;

    public static void Play(Room currentRoom)
    {
        // Check if the room is correct
        if (currentRoom.RoomName.Contains("Port"))
        {
            AnsiConsole.MarkupLine("[bald red]You are not in the Port![/]");
            return;
        }

        // Reset Available Questions
        questionList = List;

        // Prompt with a number of questions
        for(int i = 1; i <= 3; i++)
        {
            bool Continue = Prompt();
            if (!Continue)
                return;
        }

        // If you answer everything correctly then..
            // The captain writes some encouraging words.
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold white]You did it champ![/]");

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
    }

    private static bool Prompt()
    {
        // Random question
        Random rnd = new Random();
        Question question = questionList[rnd.Next(questionList.Count)];

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
            AnsiConsole.MarkupLine("[bold green]Correct![/]");
            return true;
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Incorrect![/]");
            AnsiConsole.MarkupLine("[grey37]Do some more reading in the library.[/]");
            return false;
        }
    }
}