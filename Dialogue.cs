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
            int playerChoice = Int32.Parse(Console.ReadLine());
            HandlePlayerChoice(playerChoice);

        }
    }

    private void PrintOptions()
    {
        for (int i = 0; i < OptionList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {OptionList[i]}");
        }
    }

    public void AddOption(string reply, Action action)
    {
        OptionList.Add(reply);
        DialogueOptions.Add(reply, action);
    }


    private void HandlePlayerChoice(int index)
    {
        if (index > 0 && index <= OptionList.Count)
        {
            DialogueOptions[OptionList[index - 1]].Invoke();
        }
        else
        {
            System.Console.WriteLine("Invalid Option.");
        }
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
