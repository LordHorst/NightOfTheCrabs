using NightOfTheCrabs.Inventory.Items.House.Kitchen;
using static NightOfTheCrabs.World.World;

namespace NightOfTheCrabs.World.House;

public class Kitchen() : Location("Kitchen",
    "A cozy kitchen with modern appliances. The counter is spotless and something smells delicious.",
    World.LocationType.Kitchen)
{
    protected override void InitializeItems()
    {
        Items.Add(new Knife());
        Items.Add(new Cookie());
        Items.Add(new Tobacco());
        Items.Add(new Blender());
    }
}