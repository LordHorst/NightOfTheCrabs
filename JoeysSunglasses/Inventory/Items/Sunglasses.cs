using static JoeysSunglasses.World.World;
using static JoeysSunglasses.Output;
namespace JoeysSunglasses.Inventory.Items;

public class Sunglasses : Item
{
    public Sunglasses() 
        : base("Sunglasses", "A pair of stylish sunglasses. They belong to Joey.")
    {
        CanBeUsed = true;
        SetDisallowedLocations(LocationType.House, LocationType.Kitchen); // Can't use inside
    }

    public override string Use(LocationType currentLocation )
    {
        if (DisallowedLocations.Contains(currentLocation))
            TypeWriteLine("You try to put on the sunglasses, but it feels silly to wear them inside.");
        else
            TypeWriteLine("You put on the sunglasses. Everything looks cooler now.");

        return "";
    }
}