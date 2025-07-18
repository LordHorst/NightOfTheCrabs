using JoeysSunglasses.Inventory.Items.House.Kitchen;
using static JoeysSunglasses.World.World;

namespace JoeysSunglasses.World.House;

public class Kitchen() : Location("Kitchen",
    "A cozy kitchen with modern appliances. The counter is spotless and something smells delicious.",
    LocationType.Kitchen)
{
    protected override void InitializeItems()
    {
        _items.Add(new Knife());
        _items.Add(new Cookie());
        // Add any other kitchen-specific items here
    }
}