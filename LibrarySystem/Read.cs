namespace EcoQuest;
using static Books;
using Spectre.Console;

public static class Read
{
    private static List<string> Choices = [];
    public static void Start(/*Room currentRoom*/)
    {
        // Check if the room is correct.
        /*
        if (currentRoom.RoomName != "Library")
        {
            Game.ColorWriteLine("You are not in the Library!", ConsoleColor.Red);
            return;
        }
        */

        // Add all the titles to a list.
        Choices = [];
        foreach(Book book in List)
        {
            Choices.Add(book.Title);
        }
        Choices.Add("[dim bold][[Stop reading]][/]");

        // Prompt the book selection.
        bool Continue = true;
        while (Continue)
            Continue = Prompt();
        Console.Clear();
    }

    private static bool Prompt()
    {
        // Prompt the question.
        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold]Select a book.[/]")
                                .PageSize(100)
                                .AddChoices(Choices)
        );

        Console.Clear();
        
        // Find the book.
        Book? chosenBook = List.Find(Book => Book.Title == choice);

        // Print the book
        if (chosenBook != null)
            {
                var panel = new Panel(chosenBook.Contents);
                panel.Header = new PanelHeader($"[bold white]{chosenBook.Title}[/]");
                panel.Border = BoxBorder.Double;
                panel.Padding = new Padding(1, 1, 1, 1);
                AnsiConsole.Write(panel);
                return true;
            }
        return false;
    }
}