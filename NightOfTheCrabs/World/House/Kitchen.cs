using NightOfTheCrabs.Inventory.Items.House.Kitchen;
using static NightOfTheCrabs.World.World;

namespace NightOfTheCrabs.World.House;

public class Kitchen() : Location("Kitchen",
    "A cozy kitchen with modern appliances. The counter is spotless and something smells delicious.",
    World.LocationType.Kitchen)
{
    protected override void InitializeItems()
    {
        _items.Add(new Knife());
        _items.Add(new Cookie());
        _items.Add(new Tobacco());
        _items.Add(new Blender());
    }
}