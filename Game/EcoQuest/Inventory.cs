namespace EcoQuest

public class Inventory
{
    public List<Item>? Items { get; private set; }
    public int inevntoryCapacity { get; private set}

    public Inventory(int inevntoryCapacity)
    {
        this.Items = new List<Item>;
        this.inevntoryCapacity = inevntoryCapacity;
    }

    public bool pickUpItem(Item item) 
    {
        if(item != null && Items.Count < this.inevntoryCapacity)
        {
            this.Items.add(item);
            return true;
        }
        else
        {
            return false
        }
    }

    public bool dropItem(Item item)
    {
        if(item != null && item.droppable == true)
        {
            Items.remove(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void use(Item item)
   {
      if (item.numberOfTimeUsable <= 1)
      {
        this.Items.remove(item);
      }
      else
      {
        item.numberOfTimeUsable--;
      }
   }


}