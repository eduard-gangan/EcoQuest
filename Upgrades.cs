namespace EcoQuest
{
    public static class Upgrades
    {
        public static List<string> Multipliers = new([
            "Basic Glove Set", 
            "Eco-friendly Net", 
            "Eco Shoes", 
            "Mini Grabber Bot",
            "Advanced Eco Drone",
            "Community Support",
            "Nation-wide Campaign"
        ]);
        public static List<int> MultipliersRep = new([
            500,
            1500,
            4000,
            10000,
            30000,
            90000,
            300000
        ]);
        public static void Menu()
        {
            bool playing = true;
            int currentRep = Reputation.Get();

            while (playing) {
                for (int i = 0; i < Multipliers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Multipliers[i]} : 2x multiplier  ({MultipliersRep[i]} rep)");
                }
                int option = Int32.Parse(Console.ReadLine());
                if (option < 0 || option > Multipliers.Count)
                {
                    Console.WriteLine("There's no such option dumbass !");
                    playing = false;
                }
                else if (MultipliersRep[option - 1] > currentRep) {
                    Console.WriteLine("You don't have enough reputation for that !");
                    playing = false;
                } 
                else {
                    Stats.UpgradeM(2);
                    Multipliers.RemoveAt(option - 1);
                    MultipliersRep.RemoveAt(option - 1);
                    Console.WriteLine("Upgrade applied !");
                    playing = false;
                }
            }
        }
    } 
}