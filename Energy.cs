/*
UNDERSTANDING THE SYSTEM:

1. Accessing energy
Use the Energy.Get() method to get the current amount of energy anywhere in the code.
ex. Energy.Get() will return the current amount of energy, in integer format.

2. Replenishing energy
Use the Energy.Replenish() method to replenish energy anywhere in the code, this method should idealy
only be used when the player is in the start/boat room.
ex. Energy.Replenish() will replenish energy to maximum capacity.

3. Decreasing energy
Use the Energy.Decrease() method for decreasing energy anywhere in the code by passing it the amount
you want to decrease the energy by. You cannot decrease it lower than 0.
If you try to decrease energy below 0 if will return false, which should be used as a way to know if a
certain action can be used.
ex. Energy.Decrease(30) will decrease it by 30.
ex. 2 Energy.Decrease(30) (when energy is 10) will return false and now decrease the energy.

4. Increasing capacity
Use the Energy.IncreaseCapacity() method to increase the maximum capacity the energy level can be replenished to.
ex. Energy.IncreaseCapacity(30) will increase the capacity by 30.
 */

namespace EcoQuest
{
    public static class Energy
    {
        private static int maxCapacity = 100;
        private static int energy = maxCapacity;

        public static int Get() { return energy; }

        public static void Replenish(Room currentRoom)
        {
            if (currentRoom.RoomName.Contains("Port"))
            {
                energy = maxCapacity;
                Console.WriteLine("You slept and your energy was replenished!");
            }
            else
            {
                Console.WriteLine("You can't sleep here, dumbass...");
            }
        }

        public static bool Decrease(int amount)
        {
            if (energy - amount >= 0)
            {
                energy -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void IncreaseCapacity(int amount)
        {
            maxCapacity += amount;
        }
    }
}