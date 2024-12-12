namespace EcoQuest;

public class Fish
{
    public int Rarity { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }

    public Fish(int rarity, string description, string name)
    {
        Rarity = rarity;
        Description = description;
        Name = name;
    }

}