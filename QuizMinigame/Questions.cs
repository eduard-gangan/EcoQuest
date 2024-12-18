using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace EcoQuest;

public static class Questions
{
    public readonly static List<Question> QuestionList = [
        new Question("What is 1 + 1?", ["Its obviously 3 bro", "I give up", "2 maybe"], "2 maybe"),
        new Question("What is a fish?", ["Fish is a human", "Fish is an animal", "Never heard of em"], "Fish is an animal"),
        new Question("How tall are you?", ["True", "False"], "True")
    ];
}