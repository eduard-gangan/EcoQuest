namespace EcoQuest;
using Spectre.Console;
using Spectre.Console.Cli;

public class Dialogue
{
    public string Prompt { get; set; }
    public Dictionary<string, Action> DialogueOptions { get; set; } = new Dictionary<string, Action>();

    public List<string> OptionList { get; set; } = new List<string>();
    private bool Active;


    public Dialogue(string prompt)
    {
        Prompt = prompt;
    }
    public void Start()
    {
        TriggerDialogue();
        ConsoleMethods.SlowWrite(Prompt);
        while (Active)
        {
            var playerChoice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices(OptionList));
            HandlePlayerChoice(playerChoice);
        }
    }

    private void PrintOptions()
    {
        for (int i = 0; i < OptionList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {OptionList[i]}");
            Thread.Sleep(250);
        }
    }

    public void AddOption(string reply, Action action)
    {
        if (OptionList.Contains(reply))
        {
            return;
        }
        else
        {
            OptionList.Add(reply);
            DialogueOptions.Add(reply, action);
        }
    }


    private void HandlePlayerChoice(string choice)
    {
        Console.Clear();
        DialogueOptions[choice].Invoke();
        System.Console.WriteLine();
    }
    public void InsertOption(string reply, Action action, int index)
    {
        if (OptionList.Contains(reply))
        {
            return;
        }
        else
        {
            DialogueOptions.Add(reply, action);
            OptionList.Insert(index, reply);
        }

    }
    //Key is the PlayerReply
    public void RemoveOption(string key)
    {
        DialogueOptions.Remove(key);
        OptionList.Remove(key);
    }
    public void RemoveOptionAt(int index)
    {
        DialogueOptions.Remove(OptionList[index]);
        OptionList.RemoveAt(index);
    }
    public void TriggerDialogue()
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
}
