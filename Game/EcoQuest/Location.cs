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

    public class Start : Location
    {
        public Dictionary<string, Location> Destinations { get; private set; } = new();
        public Start(string locationName, string locationDescription, int reputationReq, Location sriLanka, Location australia, Location indonesia) 
        : base(locationName, locationDescription, reputationReq)
        {
            SetDestinations(sriLanka, australia, indonesia);
        }

        public void SetDestinations(Location? sriLanka, Location? australia, Location? indonesia)
        {
            SetExit("sriLanka", sriLanka);
            SetExit("australia", australia);
            SetExit("indonesia", indonesia);
        }
        public void SetExit(string direction, Location? neighbor)
        {
            if (neighbor != null)
                Destinations[direction] = neighbor;
        }
    }

    public class SriLanka : Location
    {
        public SriLanka(string locationName, string locationDescription, int reputationReq) : base(locationName, locationDescription, reputationReq)
        {
            CreateRooms();
        }
        public void CreateRooms()
        {
            Room? srilanka_port = new("Port", "You are at the central port. To the east is a very dirty and polluted beach, to the north there is a recycling station, and to the west is the Town Hall.");
            Room? srilanka_beach = new("Polluted Beach", "You are walking through piles of trash. Supposedly you are on a beach.");
            Room? srilanka_townhall = new("Town Hall", "You are at the Town Hall, you can see the mayor's office but the door is closed and you're not allowed in, he doesn't speak to no names.");
            Room? srilanka_recycling_station = new("Recycling Station", "You are at the Recycling Station, currently it is not working.");

            srilanka_port.SetExits(srilanka_recycling_station, srilanka_beach, srilanka_townhall, null);
            srilanka_beach.SetExit("west", srilanka_port);
            srilanka_recycling_station.SetExit("south", srilanka_port);
            srilanka_townhall.SetExit("east", srilanka_port);

            Rooms["port"] = srilanka_port;
            Rooms["beach"] = srilanka_beach;
            Rooms["townhall"] = srilanka_townhall;
            Rooms["station"] = srilanka_recycling_station;
        }
    }

    public class Australia : Location
    {
        public Australia(string locationName, string locationDescription, int reputationReq) : base(locationName, locationDescription, reputationReq)
        {
            CreateRooms();
        }
        public void CreateRooms()
        {
            //Awaiting discussion
        }
    }

    public class Indonesia : Location
    {
        public Indonesia(string locationName, string locationDescription, int reputationReq) : base(locationName, locationDescription, reputationReq)
        {
            CreateRooms();
        }
        public void CreateRooms()
        {
            //Awaiting discussion
        }
    }
}
