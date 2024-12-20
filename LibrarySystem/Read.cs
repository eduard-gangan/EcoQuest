/*
--/ Class used for the book reading activity \--

Overview:
 This class is used for reading material related to the quiz.
 This activity can only be accessed in the library in Australia.

 - Use the Start() method to start reading.

Key Variables:
 - Choices: A list of possible book titles. Used to store title choices,
  also to add the additional choice to stop reading.
 */
namespace EcoQuest;
using static Books;
using Spectre.Console;

public static class Read
{
    private static List<string> Choices = [];
    
    public static void Start(Room currentRoom)
    {
        // Check if the room is correct.
        if (currentRoom.RoomName.Contains("Library"))
        {
            AnsiConsole.MarkupLine("[bald red]You are not in the Library![/]");
            return;
        }

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
        Game.DisplayRoomInformation(currentRoom);
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
                var bookTable = new Table().ShowRowSeparators();
                bookTable.Border = TableBorder.Double;
                bookTable.AddColumn($"[bold white]{chosenBook.Title}[/]");
                bookTable.AddRow(new CanvasImage($"./LibrarySystem/Covers/{chosenBook.Title}.png").MaxWidth(16));
                bookTable.AddRow(chosenBook.Contents);
                AnsiConsole.Write(bookTable);
                return true;
            }
        return false;
    }
}