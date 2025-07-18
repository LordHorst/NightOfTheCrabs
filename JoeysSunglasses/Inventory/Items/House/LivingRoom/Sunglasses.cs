using JoeysSunglasses.World;
using static JoeysSunglasses.World.World;
using static JoeysSunglasses.Output;
namespace JoeysSunglasses.Inventory.Items.House.LivingRoom;

public class Sunglasses : Item
{
    public Sunglasses() 
        : base("Sunglasses", "A pair of stylish sunglasses. They belong to Joey.")
    {
        CanBeUsed = true;
        SetDisallowedLocations(LocationType.House, LocationType.Kitchen); // Can't use inside; maybe set certain locations to have a flag "inside"?
    }

    public override string Use()
    {
        if (_world == null)
        {
            TypeWriteLine("Error: Item not properly initialized");
            return "";
        }

        var currentLocation = _world.GetCurrentLocation();
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
            LocationType.House => "Joey's sunglasses on a wooden table",
            LocationType.Kitchen => "Joey's sunglasses on the kitchen counter",
            LocationType.Garden => "Joey's sunglasses on a garden bench",
            LocationType.Outside => "Joey's sunglasses on the ground",
            _ => "Joey's sunglasses"
        };
    }
}