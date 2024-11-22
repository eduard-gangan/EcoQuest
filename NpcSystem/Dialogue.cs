namespace EcoQuest;

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
        System.Console.WriteLine(Prompt);
        while (Active)
        {

            PrintOptions();
            System.Console.Write($"> Choose an option(1-{OptionList.Count}): ");
            var consoleInput = Console.ReadLine();
            if (Int32.TryParse(consoleInput, out int playerChoice))
            {
                Console.Beep();
                HandlePlayerChoice(playerChoice);
                continue;
            }
            Game.ColorWriteLine("Please choose a valid option.", ConsoleColor.Red);

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
        OptionList.Add(reply);
        DialogueOptions.Add(reply, action);
    }


    private void HandlePlayerChoice(int index)
    {
        Console.Clear();
        if (index > 0 && index <= OptionList.Count)
        {
            System.Console.WriteLine($"> {OptionList[index - 1]}\n");
            DialogueOptions[OptionList[index - 1]].Invoke();
            System.Console.WriteLine();
        }
        else
        {
            System.Console.WriteLine("Invalid Option.");
        }
    }
    public void InsertOption(string reply, Action action, int index)
    {
        DialogueOptions.Add(reply, action);
        OptionList.Insert(index, reply);

    }
    //Key is the PlayerReply
    public void RemoveOption(string key)
    {
        DialogueOptions.Remove(key);
        OptionList.Remove(key);
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
