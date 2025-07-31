using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.Kitchen;

public class Blender : Item
{
    public Blender() : base("Blender",
        "A Moulinex Robot-Marie blender. One of the flagship products of its time and an essential item in every kitchen.")
    {
        CanBeUsed = true;
        CanBePickedUp = false;
        SetDisallowedLocations(NightOfTheCrabs.World.World.LocationType.Outside);
    }
    
    public override string Use()
    {
        var currentLocation = World?.GetCurrentLocation();
        if (currentLocation is { LocationType: NightOfTheCrabs.World.World.LocationType.Underwater })
            TypeWriteLine(
                "You really should not be messing around with electrical devices while being underwater. Besides, there is no power outlet to be found anywhere.");
        else if (currentLocation != null && DisallowedLocations.Contains(currentLocation.LocationType))
            TypeWriteLine("You are outside, far from any power outlet. Wireless power has not been invented yet.");
        else
            TypeWriteLine(
                "You toy with the idea of using the blender, but you have to admit that you don't have anything you want to blend right now.");

        return "";
    }
        
    public override string GetCantPickUpReason()
    {
        return "Unfortunately, this blender doesn’t run on GPU cores or creative ambition. It runs on electricity and hope, and right now, it hopes you’ll stop yanking its cord like a caveman discovering fire.";
    }
}