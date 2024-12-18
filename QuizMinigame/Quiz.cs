namespace EcoQuest;
using Spectre.Console;
using static Questions;


public static class Quiz
{
    public static List<Question> questionList = QuestionList;
    public static void Play(/*Room currentRoom*/)
    {
        // Check if the room is correct
        /*
        if (currentRoom.RoomName != "")
        {
            Game.ColorWriteLine("You are not in ", ConsoleColor.Red);
            return;
        }
        */

        // Reset Available Questions
        questionList = QuestionList;

        // 
        for(int i = 1; i <= 3; i++)
        {
            bool Continue = Prompt();
            if (!Continue)
                break;
        }
    }

    public static bool Prompt()
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
                                .PageSize(8)
                                .AddChoices(question.Options)
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
            AnsiConsole.MarkupLine("[dim gray]Do some more reading in the library.[/]");
            return false;
        }
    }
}