using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.LivingRoom;

public class Pipe : Item
{
    public Pipe()
        : base("Pipe", "A briar pipe. It's your most beloved pipe.", associatedKnowledge: KnowledgeType.HavePipe)
    {
        CanBeUsed = true;
        SetDisallowedLocations(NightOfTheCrabs.World.World.LocationType.Underwater);
    }

    public override string Use()
    {
        if (!Init())
            return "";
        
        var currentLocation = World?.GetCurrentLocation();
        if (World?.GetInventory() != null && (!World.GetInventory().HasItem("tobacco"))!)
            TypeWriteLine("You really want to smoke your pipe, but you are missing a vital ingredient. You are out of tobacco!");
        else if (currentLocation != null && DisallowedLocations.Contains(currentLocation.LocationType))
            TypeWriteLine(
                "You really want to light your pipe to calm your nerves, but you simply do not how you can possibly do so while being submerged underwater.");
        else
            TypeWriteLine(
                "You fill your pipe, packing the tobacco down tightly in the bowl, and proceed to light it. You immediatly feel relaxed.");

        return "";
    }

    public override string GetLocationDescription(World.World.LocationType locationType)
    {
        return locationType switch
        {
            NightOfTheCrabs.World.World.LocationType.House => "Your pipe on a wooden table",
            NightOfTheCrabs.World.World.LocationType.Kitchen => "Your pipe on the kitchen counter",
            NightOfTheCrabs.World.World.LocationType.Garden => "Your pipe on a garden bench",
            NightOfTheCrabs.World.World.LocationType.Outside => "Your pipe on the ground",
            _ => "Your pipe"
        };
    }
}