using JoeysSunglasses.World;
using static JoeysSunglasses.World.World;

namespace JoeysSunglasses.Inventory.Items.House;

public class StartingRoom: Location
{
    public StartingRoom() 
        : base("Starting Room", 
            "A small room with a wooden table in the center. Sunlight streams through a window.",
            LocationType.House )
    {
    }

    protected override void InitializeItems()
    {
        _items.Add(new Sunglasses());
    }
}