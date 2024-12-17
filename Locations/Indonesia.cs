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
            $"You are at the central port. You look around, and see a sign saying 'Submarine dock: North'",
            ["look", "talk", "map"], NPCs.Andrew
            );
            Room? indonesia_submarinedock = new("Submarine Dock",
             "You're at the submarine dock. There's a parked Submarine in front of you.",
             new List<string> { "look", "descend", "map" },
             null
            );
            Room? indonesia_submarine = new("Research Submarine", "You are inside the Submarine Research Station. There's lots of buttons, wonder what they might do.", new List<string> { "look", "analyze", "ascend" }, null);

            indonesia_port.SetExit("north", indonesia_submarinedock);
            indonesia_submarine.SetExit("south", indonesia_port);

            Rooms["port"] = indonesia_port;
            Rooms["submarinedock"] = indonesia_submarinedock;
            Rooms["submarine"] = indonesia_submarine;

        }
    }
}