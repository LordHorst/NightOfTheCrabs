using static NightOfTheCrabs.World.World;
namespace NightOfTheCrabs.World;

public class TravelDestination
{
    public string Command { get; }
    public string[] Aliases { get; }
    public WorldLocationType WorldDestination { get; }
    public Location? InitialLocation { get; set; }
    public string TravelMessage { get; }

    public TravelDestination(string command, string[] aliases, WorldLocationType worldDestination, Location initialLocation, string travelMessage)
    {
        Command = command;
        Aliases = aliases;
        WorldDestination = worldDestination;
        InitialLocation = initialLocation;
        TravelMessage = travelMessage;
    }
}