using static NightOfTheCrabs.World.World;
namespace NightOfTheCrabs.World;

public class MovementRestriction
{
    public WorldLocationType FromLocation { get; }
    public WorldLocationType ToLocation { get; }
    public LocationRequirements Requirements { get; }
    public string RestrictionMessage { get; }

    public MovementRestriction(
        WorldLocationType fromLocation, 
        WorldLocationType toLocation, 
        LocationRequirements requirements, 
        string restrictionMessage)
    {
        FromLocation = fromLocation;
        ToLocation = toLocation;
        Requirements = requirements;
        RestrictionMessage = restrictionMessage;
    }
}