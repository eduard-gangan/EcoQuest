using Spectre.Console;

namespace EcoQuest;
public class Fish
{
    public bool Endangered { get; private set; }
    public bool Endangered { get; private set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public bool Found { get; set; }
    public string ImagePath { get; set; }
    public string ID { get; set; }
    public CanvasImage Image { get; set; }

    public Fish(bool endangered, string description, string name, string imagePath, string id)
    public Fish(bool endangered, string description, string name, string imagePath, string id)
    {
        Endangered = endangered;
        Endangered = endangered;
        Description = description;
        Name = name;
        ImagePath = imagePath;
        ID = id;
        Image = new CanvasImage(ImagePath);
    }

}