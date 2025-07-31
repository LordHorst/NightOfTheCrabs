using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.Kitchen;

public class Cookie : Item
{
    public Cookie() 
        : base("Cookie", "A delicious chocolate chip cookie.")
    {
        CanBeUsed = true;
        RemoveAfterUse = true;
        //cannot be used under water
        SetDisallowedLocations(NightOfTheCrabs.World.World.LocationType.Underwater);
    }

    public override string Use()
    {
        var currentLocation = World?.GetCurrentLocation();
        
        if(currentLocation?.LocationType == NightOfTheCrabs.World.World.LocationType.Underwater)
            TypeWriteLine("The cookie would taste all watery!");
        else if (currentLocation != null && DisallowedLocations.Contains(currentLocation.LocationType))
            TypeWriteLine("You cannot eat the cookie here!");
        else if (RemoveFromInventory())
            TypeWriteLine("You eat the cookie. Yum!");
        else
            TypeWriteLine("Something went wrong while trying to eat the cookie.");
        
        return "";
    }
    public override string GetLocationDescription(World.World.LocationType locationType)
    {
        return locationType switch
        {
            NightOfTheCrabs.World.World.LocationType.House => "A cookie on a wooden table",
            NightOfTheCrabs.World.World.LocationType.Kitchen => "A cookie on the kitchen counter",
            NightOfTheCrabs.World.World.LocationType.Garden => "A cookie on a garden bench",
            NightOfTheCrabs.World.World.LocationType.Outside => "A cookie on the ground",
            _ => "A cookie"
        };
    }
}