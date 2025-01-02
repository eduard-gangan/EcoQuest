/*
--/ Class for storing books \--

Overview:
 This class is used solely for storing books. 

Use the following format:
    new Book("Book title", "Contents of the book"),

When adding a new book:
    Add a cover image to the LibrarySystem/Covers folder.
    NOTE: It must match the title of the book!
 */

namespace EcoQuest;

public static class Books
{
    public readonly static List<Book> List = [
        new Book("Overfishing","Overfishing occurs when fish are removed from the ocean faster than they can naturally reproduce, leading to the depletion of fish populations.\nThis problem is primarily caused by high demand for seafood, unregulated fishing practices, and the use of destructive fishing methods.\nOverfishing doesn’t just affect fish populations, it disrupts entire marine ecosystems and threatens food security for millions of people.\nTo combat overfishing, implementing fishing quotas, enforcing sustainable fishing practices, and supporting marine protected areas are crucial steps.\nMonitoring fish species is equally important as it helps ensure sustainable fish populations and maintains the balance of ocean ecosystems.\nWithout proper management, species can dwindle not only from overfishing but also from habitat destruction, climate change, and pollution.\nWe must also remember the cascading effects of overfishing.\nThe loss of one species can impact predator-prey relationships, leading to further imbalances.\nEveryone has a role to play, from governments creating regulations to individuals choosing sustainably sourced seafood.\nProtecting our oceans today ensures marine biodiversity and resources for future generations.\n"),
        new Book("Coastal Pollution", "Coastal pollution is a significant environmental issue that impacts marine ecosystems and communities worldwide.\nIt primarily results from improper waste disposal, agricultural runoff, and industrial discharges,\nwhich introduce harmful substances like plastics, chemicals, and excess nutrients into coastal waters.\nThis pollution disrupts marine habitats, harms biodiversity, and contaminates food sources, affecting both aquatic life and human health.\nPreventing coastal pollution involves reducing plastic usage, improving waste management, and adopting sustainable agricultural practices.\nProtecting our coasts benefits marine life, enhances water quality, and supports livelihoods reliant on healthy oceans.\nWithout action, polluted waters can lead to dead zones, coral bleaching, and declines in fish populations.\nCoastal pollution is a major focus of SDG Goal 14, emphasizing the need to conserve and sustainably use ocean resources.\nSimple actions, like participating in beach cleanups and reducing single-use plastics, can make a big difference.\nAddressing coastal pollution is essential for preserving the oceans and ensuring a sustainable future for all life on Earth.\n"),
        new Book("Swamp Man", "Listen here, Donkey! Just because I like my swamp quiet, doesn’t mean I’m not goin' to get a little company now and then. But, by the looks of it, this place is turning into a circus!\nYou know, Donkey, I’ve lived here for years, and I’ve never had to deal with half the nonsense that’s come through here since you showed up! I used to have peace and quiet, now I got a dragon and a bunch of talking animals livin' in my backyard. MY backyard!\nBut, fine, I suppose I’ve got no choice now. You’re here to stay, and... well, you’re better than that horrid prince, so you’ve got that going for you.\nAnyway, what’s for dinner?")
    ];
}