/*
UNDERSTANDING THE SYSTEM:

It is the same as the Reputation System.

The money will be used for  buying upgrades from designated rooms/areas. The currency we will use is EcoCoins, EC for short, so for example when setting prices use this format: 50EC. 
Make sure to check if the player has enough money to buy something.

1.Accessing money
Use the Money.Get() method to get the current amount of money anywhere in the code
ex. Money.Get() will return the current amount of money, in integer format

2.Adding money
Use the Money.Add() method for adding money anywhere in the code by passing it the amount
you want to increase the money by
ex. Money.Add(30) will increase it by 30

3.Decreasing money
Use the  Money.Decrease() method for decreasing money anywhere in the code by passing it the amount
you want to decrease the money by. You cannot decrease it lower than 0.
ex. Money.Decrease(30) will decrease it by 30


 */

namespace EcoQuest
{
    public static class Money
    {
        private static int money = 0;

        public static void Add(int amount)
        {
            money += amount;
        }

        public static void Decrease(int amount)
        {
            if (money - amount >= 0)
            {
                money -= amount;
            }
            else
            {
                money = 0;
            }
        }

        public static int Get() { return money; }
    }
}