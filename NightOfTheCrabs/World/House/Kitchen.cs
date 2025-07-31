using NightOfTheCrabs.Inventory.Items.House.Kitchen;
using static NightOfTheCrabs.World.World;

namespace NightOfTheCrabs.World.House;

public class Kitchen(World? world) : Location(world,
    "Kitchen",
    "A cozy kitchen with modern appliances. The counter is spotless and something smells delicious.",
    WorldLocationType.London,
    LocationType.Kitchen)
{
    protected override void InitializeItems()
    {
        AddItem(new Knife());
        AddItem(new Cookie());
        AddItem(new Tobacco());
        AddItem(new Blender());
    }
}