namespace EcoQuest
{
    public class Australia : Location
    {
        public Australia(string locationName, string locationDescription, int reputationReq) : base(locationName, locationDescription, reputationReq)
        {
            CreateRooms();
        }
        public void CreateRooms()
        {
            Room? australia_port = new("Australia Port",
            $"You are at the central port. You're not sure how, but you just feel there's a library to the north",
            ["look", "map"], NPCs.Captain
            );
            Room? australia_library = new("Library",
             "You're at the local library. You look around a bit, and locate the 'Marine Life' section",
             new List<string> { "look", "map", "talk" },
             null
            );

            australia_port.SetExit("north", australia_library);
            australia_library.SetExit("south", australia_port);

            Rooms["port"] = australia_port;
            Rooms["library"] = australia_library;
        }
    }
}