/*
UNDERSTANDING THE SYSTEM:

1.Accessing reputation
Use the Reputation.Get() method to get the current amount of reputation anywhere in the code
ex. Reputation.Get() will return the current amount of reputation, in integer format

2.Adding reputation
Use the Reputation.Add() method for adding reputation anywhere in the code by passing it the amount
you want to increase the reputation by
ex. Reputation.Add(30) will increase it by 30

3.Decreasing reputation
Use the Reputation.Decrease() method for decreasing reputation anywhere in the code by passing it the amount
you want to decrease the reputation by. You cannot decrease it lower than 0
ex. Reputation.Decrease(30) will decrease it by 30
 */

namespace EcoQuest
{
    public static class Reputation
    {
        public static int reputation = 0;
        public static void Add(int amount)
        {
            reputation += amount;
            QuestSriLanka.CheckQuest();
        }
        public static void Decrease(int amount)
        {
            if (reputation - amount >= 0)
            {
                reputation -= amount;
            }
            else
            {
                reputation = 0;
            }
        }
        public static int Get() { return reputation; }
    }
}