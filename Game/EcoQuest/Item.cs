namespace EcoQuest

pulic class Item
{
   public string name { get; private set; }
   public string descripion { get; private set; }
   public bool droppable { get; set; }
   public int numberOfTimeUsable { get; private set; }

   
   public Item(string name, string description, bool droppable, int numberOfTimeUsable)
   {
      this.name = name;
      this.descripion = descripion;
      this.droppable = droppable;
      this.numberOfTimeUsable = numberOfTimeUsable;
   }

   public void printDescription()
   {
      Console.WriteLine(this.descripion);
   }

}