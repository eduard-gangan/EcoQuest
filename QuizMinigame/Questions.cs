/*
--/ Class for storing questions \--

Overview:
 This class is used solely for storing possible quiz questions. 

Use the following format:
    new Question("Question name", ["Choice_1", "Choice_2", "Choice_3"], "Choice_1"),

The second argument is a list of possible choices, the choices can be any string.
The last argument is the answer of the question, i.e. an identical string as the choice.

 */
namespace EcoQuest;

public static class Questions
{
    public readonly static List<Question> List = [
        new Question("What is 1 + 1?", ["Its obviously 3 bro", "I give up", "2 maybe"], "2 maybe"),
        new Question("What is a fish?", ["Fish is a human", "Fish is an animal", "Never heard of em"], "Fish is an animal"),
        new Question("How tall are you?", ["True", "False"], "True")
    ];
}