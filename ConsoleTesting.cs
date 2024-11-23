namespace EcoQuest;
using Spectre.Console;

public class ConsoleTesting
{
    public static void Test(){
        // Ask
        var bin = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"Sort [green]Jar[/] to the corresponding bin.")
                .PageSize(8)
                .MoreChoicesText("[grey](Move up and down to reveal more)[/]")
                .AddChoices(new[] {
                    "Organic", "Plastic", "Metal", 
                    "Glass", "Paper", "Electronic",
                    "Rubber", "Waste",
                }));

        // Echo the choice back to the terminal
        AnsiConsole.WriteLine($"You chose {bin}!");
    }

    public static void Test2(){
        var layout = CreateLayout();
        AnsiConsole.Write(layout);

        Console.ReadKey(true);
    }

    public static void Test3(){
        // Load an image
        var image = new CanvasImage("test.png");

        // Set the max width of the image.
        // If no max width is set, the image will take
        // up as much space as there is available.
        //image.MaxWidth();

        // Render the image to the console
        AnsiConsole.Write(image);
    }
    public static void Test4(){
        // Create the layout
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Right")
                    .SplitRows(
                        new Layout("Top"),
                        new Layout("Bottom")));

        // Update the left column
        layout["Left"].Update(
            new Panel(
                Align.Center(
                    new Markup("Hello [blue]World![/]"),
                    VerticalAlignment.Middle))
                .Expand());

        // Render the layout
        AnsiConsole.Write(layout);
    }

    public static void Figlet(string text){
        AnsiConsole.Write(
        new FigletText(text)
            .LeftJustified()
            .Color(Color.Blue));
    }

    



    private static Layout CreateLayout()
    {
        var layout = new Layout();

        layout.SplitRows(
            new Layout("Top")
                .SplitColumns(
                    new Layout("Left")
                        .SplitRows(
                            new Layout("LeftTop"),
                            new Layout("LeftBottom")),
                    new Layout("Right").Ratio(2),
                    new Layout("RightRight").Size(3)),
            new Layout("Bottom"));

        layout["LeftBottom"].Update(
            new Panel("[blink]PRESS ANY KEY TO QUIT[/]")
                .Expand()
                .BorderColor(Color.Yellow)
                .Padding(0, 0));

        layout["Right"].Update(
            new Panel(
                new Table()
                    .AddColumns("[blue]Qux[/]", "[green]Corgi[/]")
                    .AddRow("9", "8")
                    .AddRow("7", "6")
                    .Expand())
            .Header("A [yellow]Table[/] in a [blue]Panel[/] (Ratio=2)")
            .Expand());

        layout["RightRight"].Update(
            new Panel("Explicit-size-is-[yellow]3[/]")
                .BorderColor(Color.Yellow)
                .Padding(0, 0));

        layout["Bottom"].Update(
        new Panel(
                new FigletText("Hello World"))
            .Header("Some [green]Figlet[/] text")
            .Expand());

        return layout;
    }
}