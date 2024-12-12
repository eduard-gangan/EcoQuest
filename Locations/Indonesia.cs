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
            ["look", "talk"], NPCs.IndonesiaNPC
            );
            Room? indonesia_submarine = new("Submarine Dock",
             "You're at the submarine dock",
             new List<string> { "look", },
             null
            );

            indonesia_port.SetExit("north", indonesia_submarine);
            indonesia_submarine.SetExit("south", indonesia_port);

            Rooms["port"] = indonesia_port;
            Rooms["submarine"] = indonesia_submarine;
        }
    }
}