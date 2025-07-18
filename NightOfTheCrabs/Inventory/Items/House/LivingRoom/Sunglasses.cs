using static NightOfTheCrabs.World.World;
using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs.Inventory.Items.House.LivingRoom;

public class Sunglasses : Item
{
    public Sunglasses() 
        : base("Sunglasses", "A pair of stylish sunglasses.")
    {
        CanBeUsed = true;
        SetDisallowedLocations(LocationType.House, LocationType.Kitchen); // Can't use inside; maybe set certain locations to have a flag "inside"?
    }

    public override string Use()
    {
        if (!Init())
            return "";

        var currentLocation = _world?.GetCurrentLocation();
        if (currentLocation != null && DisallowedLocations.Contains(currentLocation.LType))
            TypeWriteLine("You try to put on the sunglasses, but it feels silly to wear them inside.");
        else
            TypeWriteLine("You put on the sunglasses. Everything looks cooler now.");

        return "";
    }    
    public override string GetLocationDescription(LocationType locationType)
    {
        return locationType switch
        {
            LocationType.House => "MainGameClass's sunglasses on a wooden table",
            LocationType.Kitchen => "MainGameClass's sunglasses on the kitchen counter",
            LocationType.Garden => "MainGameClass's sunglasses on a garden bench",
            LocationType.Outside => "MainGameClass's sunglasses on the ground",
            _ => "MainGameClass's sunglasses"
        };
    }
}