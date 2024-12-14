namespace EcoQuest;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Linq;

public class Dialogue
{
    public string Prompt { get; set; }
    // public Dictionary<string, Action> DialogueOptions { get; set; } = new Dictionary<string, Action>();

    // public List<string> OptionList { get; set; } = new List<string>();

    private List<Option> optionList = new List<Option>();

    private bool Active;


    public Dialogue(string prompt)
    {
        Prompt = prompt;
    }

    public void Start()
    {
        ToggleDialogue();
        Game.SlowWrite(Prompt);
        while (Active)
        {
            var playerChoice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title(null)
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(getOptionNames()));
            HandlePlayerChoice(playerChoice);
        }
    }

    public void AddOption(string reply, CustomAction action)
    {
        AddOption(new Option(reply, action));
    }

    public void AddOption(string reply, Action action)
    {
        AddOption(new Option(reply, new GenericAction(action)));
    }

    public void AddOption(Option option)
    {
        if (optionList.Any((item) => item.Name == option.Name))
        {
            return;
        }
        else
        {
            optionList.Add(option);
        }
    }

    private void HandlePlayerChoice(string choice)
    {

        Console.Clear();
        Option? chosenOption = optionList.Find((option) => option.Name == choice);
        if (chosenOption == null)
        {
            throw new Exception("Non-existent option");
        }

        chosenOption.Action.Invoke();

        System.Console.WriteLine();
    }

    public void InsertOption(string reply, Action action, int index)
    {
        if (optionList.Any((option) => option.Name == reply))
        {
            return;
        }
        else
        {
            optionList.Insert(index, new Option(reply, new GenericAction(action)));
        }

    }
    //Key is the PlayerReply
    public void RemoveOption(string key)
    {
        int removedOptionIndex = optionList.FindIndex((option) => option.Name == key);

        optionList.RemoveAt(removedOptionIndex);
    }

    public void ToggleDialogue()
    {
        if (Active == false)
        {
            Active = true;
        }
        else
        {
            Active = false;
        }
    }

    private List<string> getOptionNames()
    {
        return optionList.Select((option) => option.Name).ToList();
    }
}
