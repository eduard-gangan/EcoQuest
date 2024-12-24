/*
--/ Class for storing questions \--

Overview:
 This class is used solely for storing possible quiz questions. 

Use the following format:
    new Question("Question name", ["Choice_1", "Choice_2", "Choice_3"], "Choice_1"),

The second argument is a list of possible choices, the choices can be any string.
The last arugment is the answer of the question, i.e. an identical string as the choice.

 */
namespace EcoQuest;

public static class Questions
{
    public readonly static List<Question> List = [
        new Question("What is the benefit of preventing coastal pollution?", ["It protects marine life and ecosystems", "It reduces beach tourism", "It increases fish populations for overfishing", "It creates more plastic islands"],"It protects marine life and ecosystems"),
        new Question("How does coastal pollution affect the environment if it is not prevented?", ["It causes coral growth to accelerate", "It decreases water clarity for fishing boats", "It disrupts marine habitats and harms biodiversity", "It boosts local economy"],"It disrupts marine habitats and harms biodiversity"),
        new Question("How can coastal pollution be prevented?", ["By increasing oil drilling near the coast", "By building more dams near shorelines", "By using more fertilizers in agriculture", "By reducing plastic waste and improving waste management"],"By reducing plastic waste and improving waste management"),
        new Question("Why is coastal pollution harmful?", ["It helps fish adapt to polluted environments", "It makes the ocean deeper", "It contaminates food sources and destroys marine habitats", "It encourages mangrove forest growth"],"It contaminates food sources and destroys marine habitats"),
        new Question("Which activity contributes the most to coastal pollution?", ["Overfishing in deep oceans", "Improper waste disposal and runoff from land-based sources", "Installing underwater cables", "Increased tidal wave activity"],"Improper waste disposal and runoff from land-based sources"),
        new Question("What is overfishing?", ["Introducing new fish species into ecosystems", "Removing fish from the ocean faster than they can reproduce", "Fishing only during specific seasons", "Farming fish in controlled environments"],"Removing fish from the ocean faster than they can reproduce"),
        new Question("What causes overfishing?", ["High demand for seafood and unregulated fishing practices", "Climate change causing warmer waters", "Recreational fishing in coastal areas", "Using small nets to catch fish near the shore"],"High demand for seafood and unregulated fishing practices"),
        new Question("How can overfishing be prevented?", ["Increasing the number of fishing boats", "Releasing fish back into the water after catching them", "Implementing fishing quotas and sustainable fishing practices", "Avoiding fishing altogether"],"Implementing fishing quotas and sustainable fishing practices"),
        new Question("What is the benefit of monitoring fish species?", ["It decreases the cost of fishing operations", "It prevents storms and natural disasters", "It eliminates invasive species from the ocean", "It ensures sustainable fish populations and ecosystem balance"],"It ensures sustainable fish populations and ecosystem balance"),
        new Question("Aside from overfishing, how do marine species dwindle?", ["Excessive algae growth from underwater farming", "Habitat destruction, climate change, and pollution", "Changes in underwater currents caused by shipping", "Lack of sunlight reaching deeper ocean levels"],"Habitat destruction, climate change, and pollution"),

    ];
}