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
            $"You are at the central port.",
            ["look", "talk"], NPCs.IndonesiaNPC
            );
            Rooms["port"] = indonesia_port;
        }
    }
}