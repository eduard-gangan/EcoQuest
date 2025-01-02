using Spectre.Console;

namespace EcoQuest;

public static class Inventory
{
    public static List<Item> Items = new List<Item>();
    public static int InventoryCapacity { get; set; } = 1;



    public static bool PickUpItem(Item item)
    {
        if (item != null && Items.Count < InventoryCapacity)
        {
            Items.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool DropItem(Item item)
    {
        if (item != null && Items.Contains(item) && item.Droppable == true)
        {

            return Items.Remove(item);
        }
        else
        {
            return false;
        }
    }

    public static void Use(Item item)
    {
        if (item.NumberOfTimeUsable <= 1)
        {
            Items.Remove(item);
        }
        else
        {
            item.NumberOfTimeUsable--;
        }
    }

    public static void DisplayInventory()
    {
        AnsiConsole.MarkupLine($"[grey]>Inventory({Items.Count}/{InventoryCapacity}):[/]");
        if (Items.Count > 0)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Items[i].Name}");
            }
        }
        else
        {
            Console.WriteLine("Your inventory is empty.");
        }

    }


}