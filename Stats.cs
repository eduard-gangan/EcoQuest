using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoQuest
{
    public static class Stats
    {
        public static int stat = 1; //This is how much trash the player can pick up at a time

        public static int Get() { return stat; }
        public static void UpgradeA(int upgrade)
        { //This method upgrades by adding to the stat
            stat += upgrade;
        }
        public static void UpgradeM(int multiplier)
        { //This method upgrades by multiplying to the stat
            stat *= multiplier;
        }
    }
}
