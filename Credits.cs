// This is for the ending statistics

using Spectre.Console;

namespace EcoQuest{
  public static class Credits {
    public static int pickCommands = 0;
    public static int correctlySorted = 0;
    public static int incorrectlySorted = 0;
    public static int discoveredSpecies = 16;
    public static int quizTries = 0;

    public static void AddPickCommand(){
      pickCommands += 1;
    }
    public static void AddCorrectlySorted(){
      correctlySorted += 1;
    }
    public static void AddIncorrectlySorted(){
      incorrectlySorted += 1;
    }
    public static void AddQuizTries(){
      quizTries += 1;
    }

    public static void Show(){
      ConsoleMethods.SlowWrite($"You received {Reputation.Get()} reputation \n");
      ConsoleMethods.SlowWrite($"You picked up trash {pickCommands} times \n");
      if(incorrectlySorted == 0) 
        ConsoleMethods.SlowWrite($"You sorted trash correctly {correctlySorted} times, and didn't mess up a single time ! \n");
      else 
        ConsoleMethods.SlowWrite($"You sorted trash correctly {correctlySorted} times, but also messed up {incorrectlySorted} times \n");
      ConsoleMethods.SlowWrite($"You discovered {discoveredSpecies} new species of fish ! \n");
      ConsoleMethods.SlowWrite($"You completed the quiz in {quizTries} tries");
      if (quizTries == 1) 
        ConsoleMethods.SlowWrite($"You must've played this before ! ;)\n");
      else if (quizTries <= 3) 
        ConsoleMethods.SlowWrite($"You're such a natural ! Nobody knows the sea as well as you !\n");
      else if (quizTries <= 7) 
        ConsoleMethods.SlowWrite($"Once you got the hang of it, you really went at it !\n");
      else if (quizTries <= 25) 
        ConsoleMethods.SlowWrite($"You were a bit slow, but in the end you got it !\n");
      else 
        ConsoleMethods.SlowWrite($"I think I've never seen someone do so bad. Might need to go get your IQ checked !\n");
      ConsoleMethods.SlowWrite("THANK YOU FOR PLAYING ECOQUEST !");
    }
  }
}