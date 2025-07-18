using static JoeysSunglasses.World.World;
using static JoeysSunglasses.Output;
using JoeysSunglasses.Inventory;
using JoeysSunglasses.World;

namespace JoeysSunglasses.Inventory.Items.House.Kitchen;

public class Cookie : Item
{
    public Cookie() 
        : base("Cookie", "A delicious chocolate chip cookie.")
    {
        CanBeUsed = true;
        RemoveAfterUse = true;
        //cannot be used under water
        SetDisallowedLocations(LocationType.Underwater);
    }

    public override string Use()
    {
        if (_world == null)
        {
            TypeWriteLine("Error: Item not properly initialized");
            return "";
        }

        var currentLocation = _world.GetCurrentLocation();
        
        if(currentLocation?.LType == LocationType.Underwater)
            TypeWriteLine("The cookie would taste all watery!");
        else if (currentLocation != null && DisallowedLocations.Contains(currentLocation.LType))
            TypeWriteLine("You cannot eat the cookie here!");
        else if (RemoveFromInventory())
            TypeWriteLine("You eat the cookie. Yum!");
        else
            TypeWriteLine("Something went wrong while trying to eat the cookie.");
        
        return "";
    }
}