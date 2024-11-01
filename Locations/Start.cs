namespace EcoQuest
{
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
}