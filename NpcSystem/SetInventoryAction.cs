namespace EcoQuest;

class SetInventoryAction : CustomAction
{
    private int Size;

    public SetInventoryAction(int size)
    {
        Size = size;
    }

    public void Invoke()
    {
        Inventory.InventoryCapacity = Size;
    }
}