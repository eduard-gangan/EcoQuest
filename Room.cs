namespace EcoQuest
{
    public class Room
    {
        public string RoomName { get; private set; }
        public string RoomDescription { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public NPC? RoomNPC { get; private set; }
        public List<string> AvailableCommands { get; private set; } = new();
        public Room(string roomName, string roomDescription, List<string> availableCommands)
        {
            RoomName = roomName;
            RoomDescription = roomDescription;
            AvailableCommands = availableCommands;
        }
        public Room(string roomName, string roomDescription, List<string> availableCommands, NPC? roomNPC = null)
        {
            RoomName = roomName;
            RoomDescription = roomDescription;
            AvailableCommands = availableCommands;
            RoomNPC = roomNPC;
        }
        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }

        //For eventual consequences on the environment based on the player's actions
        public void ChangeRoomName(string newRoomName)
        {
            RoomName = newRoomName;
        }
        public void ChangeRoomDescription(string newRoomDescription)
        {
            RoomDescription = newRoomDescription;
        }

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
    }
}