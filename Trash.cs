namespace EcoQuest
{
    public static class Trash
    {
        public static int amount = 1000000;


        public static int Get() { return amount; }

        public static void PickUp()
        {
            if (amount - Stats.Get() > 0)
            {
                Energy.Decrease(5);
                amount -= Stats.Get();
            }
            else
            {
                Console.WriteLine("There is no more trash !");
            }
        }
    }
}