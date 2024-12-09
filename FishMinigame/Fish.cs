namespace EcoQuest;

public class Fish
{
    public int Rarity { get; set; }
    public string ?Desription { get; set; }
    public string ?Name { get; set; }

    public Fish (int rarity, string desription, string name)
    {
        Rarity = rarity;
        Desription = desription;
        Name = name;
    }

}