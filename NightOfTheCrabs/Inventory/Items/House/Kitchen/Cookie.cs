using static NightOfTheCrabs.World.World;
using static NightOfTheCrabs.Output;
using NightOfTheCrabs.Inventory;
using NightOfTheCrabs.World;

namespace NightOfTheCrabs.Inventory.Items.House.Kitchen;

public class Cookie : Item
{
    public Cookie() 
        : base("Cookie", "A delicious chocolate chip cookie.")
    {
        CanBeUsed = true;
        RemoveAfterUse = true;
        //cannot be used under water
        SetDisallowedLocations(World.World.LocationType.Underwater);
    }

    public override string Use()
    {
        if (!Init())
            return "";

        var currentLocation = _world?.GetCurrentLocation();
        
        if(currentLocation?.LType == World.World.LocationType.Underwater)
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