using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs.Inventory.Items.House.Kitchen;

public class Knife : Item
{
    public Knife() 
        : base("Knife", "A sharp kitchen knife. Be careful with it!")
    {
        CanBeUsed = true;
        SetAllowedLocations(NightOfTheCrabs.World.World.LocationType.Kitchen);
    }

    public override string Use()
    {
        if (World == null)
        {
            TypeWriteLine("This item cannot be used");
            return "";
        }

        if (AllowedLocations is { Count: > 0 } && AllowedLocations.Contains(World.GetCurrentLocation()!.LocationType))
        {
            TypeWriteLine("You use the knife to cut some cheese. Uh-hu.");   
        }
        else
        {
            TypeWriteLine("You probably shouldn't wave that knife around.");    
        }
        return "";
    }
}