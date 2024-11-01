namespace EcoQuest;

public class Item
{
   public string Name { get; private set; }
   public string Description { get; private set; }
   public bool Droppable { get; set; }
   public int NumberOfTimeUsable { get; set; }


   public Item(string name, string description, bool droppable, int numberOfTimeUsable)
   {
      this.Name = name;
      this.Description = description;
      this.Droppable = droppable;
      this.NumberOfTimeUsable = numberOfTimeUsable;
   }

   public void PrintDescription()
   {
      Console.WriteLine(this.Description);
   }

}