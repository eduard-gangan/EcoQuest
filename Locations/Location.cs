using System;
using System.Collections.Generic;

namespace EcoQuest
{
    public class Location
    {
        public string LocationName { get; private set; }
        public string LocationDescription { get; private set; }
        public int ReputationReq { get; private set; }
        public Dictionary<string, Room> Rooms { get; private set; } = new();

        public Location(string locationName, string locationDescription, int reputationReq)
        {
            LocationName = locationName;
            LocationDescription = locationDescription;
            ReputationReq = reputationReq;
        }
    }
}
