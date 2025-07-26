using static NightOfTheCrabs.World.World;
namespace NightOfTheCrabs.World;

public class TravelDestination
{
    public string Command { get; }
    public string[] Aliases { get; }
    public WorldLocationType WorldDestination { get; }
    private readonly Location _initialLocationTemplate;
    public string TravelMessage { get; }
    public bool ConfirmationRequired { get; }

    public TravelDestination(string command, string[] aliases, WorldLocationType worldDestination, 
        Location initialLocation, string travelMessage, bool confirmationRequired = false)
    {
        Command = command;
        Aliases = aliases;
        WorldDestination = worldDestination;
        _initialLocationTemplate = initialLocation;
        TravelMessage = travelMessage;
        ConfirmationRequired = confirmationRequired;
    }

    public Location InitialLocation => WorldDestination.ToLocation();
}