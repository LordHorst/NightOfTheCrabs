using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.Kitchen;

public class Tobacco : Item
{
    public Tobacco() : base("Tobacco", "A pouch of tobacco", associatedKnowledge: KnowledgeType.HaveTobacco)
    {
        CanBeUsed = true;
        SetDisallowedLocations(NightOfTheCrabs.World.World.LocationType.Underwater);
    }
    
    public override string Use()
    {
        if(!Init())
            return "";
        
        var currentLocation = World?.GetCurrentLocation();
        if (Inventory != null && !Inventory.HasItem("pipe"))
            TypeWriteLine("You consider chewing on your tobacco for a second, but decide against it.");
        else if (currentLocation != null && DisallowedLocations.Contains(currentLocation.LocationType))
            TypeWriteLine(
                "Using your tobbacco here would make it quite wet.");
        else
            TypeWriteLine(
                "You fill your pipe, packing the tobacco down tightly in the bowl, and proceed to light it. You immediatly feel relaxed.");

        return "";
    }

    public override string GetLocationDescription(World.World.LocationType locationType)
    {
        return locationType switch
        {
            NightOfTheCrabs.World.World.LocationType.House => "Tobacco on a wooden table",
            NightOfTheCrabs.World.World.LocationType.Kitchen => "Tobacco on the kitchen counter",
            NightOfTheCrabs.World.World.LocationType.Garden => "Tobacco on a garden bench",
            NightOfTheCrabs.World.World.LocationType.Outside => "Tobacco on the ground",
            _ => "Tobacco"
        };
    }
}