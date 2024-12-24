// This is for the ending statistics
using System.Dynamic;
using Spectre.Console;
namespace EcoQuest;

public static class Credits
{
  public static int pickCommands = 0;
  public static int correctlySorted = 0;
  public static int incorrectlySorted = 0;
  public static int taggedSpecies = 4;
  public static int quizTries = 0;

  public static void AddPickCommand()
  {
    pickCommands += 1;
  }
  public static void AddCorrectlySorted()
  {
    correctlySorted += 1;
  }
  public static void AddIncorrectlySorted()
  {
    incorrectlySorted += 1;
  }
  public static void AddQuizTries()
  {
    quizTries += 1;
  }

  public static void ShowStats()
  {
    AnsiConsole.MarkupLine($"You received [green]{Reputation.Get()}[/] reputation.");
    Thread.Sleep(1000);
    AnsiConsole.MarkupLine($"You picked up trash [yellow]{pickCommands} [/]times.");
    Thread.Sleep(1000);
    if (incorrectlySorted == 0)
      AnsiConsole.MarkupLine($"You sorted trash correctly [green]{correctlySorted}[/] times, and didn't mess up a single time!");
    else
      AnsiConsole.MarkupLine($"You sorted trash correctly [green]{correctlySorted}[/] times, but also messed up [red]{incorrectlySorted} [/]times.");
    Thread.Sleep(1000);
    AnsiConsole.MarkupLine($"You tagged [cyan]{taggedSpecies} [/]species of fish for tracking!");
    Thread.Sleep(1000);
    AnsiConsole.MarkupLine($"You completed the quiz in [blue]{quizTries} [/]tries.");
    Thread.Sleep(1000);
    if (quizTries == 1)
      AnsiConsole.MarkupLine($"You must've played this before ! ;)");
    else if (quizTries <= 3)
      AnsiConsole.MarkupLine($"You're such a natural ! Nobody knows the sea as well as you !");
    else if (quizTries <= 7)
      AnsiConsole.MarkupLine($"Once you got the hang of it, you really went at it !");
    else if (quizTries <= 25)
      AnsiConsole.MarkupLine($"You were a bit slow, but in the end you got it !");
    else
      AnsiConsole.MarkupLine($"I think I've never seen someone do so bad...");
    Thread.Sleep(1000);
  }
  public static void ShowCredits()
  {
    var panel = new Panel("Victor Petrica\nEduard Gangan\nGene Enrick Miguel Giroy\nDeividas Petrulis\nToni Vera Gutierrez");

    AnsiConsole.MarkupLine("This game was developed on top of the pre-existing [purple] WorldOfZuul [/]game.");
    AnsiConsole.MarkupLine("It was created for the 1st Semester Project at SDU by Group nr. 20 consisting of students:");
    AnsiConsole.Write(panel);
  }
  public static void EndGame()
  {
    AnsiConsole.Clear();
    AnsiConsole.Write(
                     new FigletText("GAME END")
                     .Color(Color.Cyan1));
    System.Console.WriteLine();
    ShowStats();
    ShowCredits();
    ConsoleMethods.SlowWriteLine("THANK YOU FOR PLAYING ECOQUEST !");
    Program.game.ContinuePlaying = false;
  }

}
