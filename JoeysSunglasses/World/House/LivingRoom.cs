using JoeysSunglasses.Inventory.Items.House.LivingRoom;
using static JoeysSunglasses.World.World;

namespace JoeysSunglasses.World.House;

public class LivingRoom() : Location("Living Room",
    "A warm and inviting space with dark wooden beams crossing the ceiling. " +
    "The aged floorboards creak gently underfoot, telling stories of generations past. " +
    "A well-worn leather armchair sits near a stone fireplace, while sunlight filters through " +
    "lace curtains, casting intricate shadows on the vintage wallpaper. " +
    "The air carries a subtle scent of cedar and old books.",
    LocationType.House)
{
    protected override void InitializeItems()
    {
        _items.Add(new Sunglasses());
    }
}