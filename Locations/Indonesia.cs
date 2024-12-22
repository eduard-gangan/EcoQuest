namespace EcoQuest
{
    public class Indonesia : Location
    {
        public Indonesia(string locationName, string locationDescription, int reputationReq) : base(locationName, locationDescription, reputationReq)
        {
            CreateRooms();
        }
        public void CreateRooms()
        {
            Room? indonesia_port = new("Indonesia Port",
            $"You are at the central port. You look around, and see a sign saying 'Submarine dock: North'\n[NPC]\nThere's a guy leaning on a fence, looking towards the ocean",
            ["talk", "map", "north"], NPCs.Andrew
            );
            Room? indonesia_submarinedock = new("Submarine Dock",
             "You're at the submarine dock. There's a parked Submarine in front of you.",
             new List<string> {"descend", "map", "south" },
             null
            );
            Room? indonesia_submarine = new("Research Submarine", "You are inside the Submarine Research Station. There's lots of buttons, wonder what they might do.", new List<string> { "analyze", "ascend", "map" }, null);

            indonesia_port.SetExit("north", indonesia_submarinedock);
            indonesia_submarinedock.SetExit("south", indonesia_port);

            Rooms["port"] = indonesia_port;
            Rooms["submarinedock"] = indonesia_submarinedock;
            Rooms["submarine"] = indonesia_submarine;

        }
    }
}