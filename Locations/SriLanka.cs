namespace EcoQuest
{
    public class SriLanka : Location
    {
        public SriLanka(string locationName, string locationDescription, int reputationReq) : base(locationName, locationDescription, reputationReq)
        {
            CreateRooms();
        }
        public void CreateRooms()
        {
            Room? srilanka_port = new("Sri Lanka Port",
            $"You are at the central port. To the east is a very dirty and polluted beach, to the north there is a recycling station, and to the west is the Town Hall. \nThere is a stranger standing in the port. His face lights up when sees you, maybe you should go talk to him.",
            NPCs.Garry

            );

            Room? srilanka_beach = new("Polluted Beach",
             "You are walking through piles of trash. Supposedly you are on a beach."
               );

            Room? srilanka_townhall = new("Town Hall", "You are at the Town Hall, you can see the mayor's office but the door is closed and you're not allowed in, he doesn't speak to no names.", NPCs.Lanka);

            Room? srilanka_recycling_station = new("Recycling Station",
             "You aproach the run-down building. the place stands as an empty shell, of what once may have been a center for recycling and a haven for waste materials that are given their new life. around the station, there are piles of unsorted trash that consisted of plastic, glass, metals and rusted cans that are decomposing and emitting a strong smell through the heat of the sun… as you enter the station, you witness the room being filled with dimming light from the sun as it enters the shattered and dusted windows… and the machinery that are once rumbling as they process wastes materials, sit silently and broken, while wires hanging and rusts spreading across the metal framework of the machines.\n[There is an NPC in the Room!]\nWhile exploring around the property, you stumble upon a gloomy looking stranger, sitting above the railing looking down upon the machinery. the stranger looks warily as you approach.",
             NPCs.Larry
            );

            srilanka_port.SetExits(srilanka_recycling_station, srilanka_beach, null, srilanka_townhall);
            srilanka_beach.SetExit("west", srilanka_port);
            srilanka_recycling_station.SetExit("south", srilanka_port);
            srilanka_townhall.SetExit("east", srilanka_port);

            Rooms["port"] = srilanka_port;
            Rooms["beach"] = srilanka_beach;
            Rooms["townhall"] = srilanka_townhall;
            Rooms["station"] = srilanka_recycling_station;
        }
    }
}