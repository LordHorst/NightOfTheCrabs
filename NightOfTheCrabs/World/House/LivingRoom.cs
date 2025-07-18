using NightOfTheCrabs.Inventory.Items;
using NightOfTheCrabs.Inventory.Items.House.LivingRoom;
using static NightOfTheCrabs.World.World;

namespace NightOfTheCrabs.World.House;

public class LivingRoom() : Location("Living Room",
    "You sit in your living room, your mind troubled by the fate of Pat and his fiancée Carol. " +
    "They were supposed to be camping near Shell Island, but you haven't heard from them in days. " +
    "The morning newspaper lies untouched on the coffee table, its headlines meaningless in light of your worry. " +
    "Before heading to Llanbedr to investigate, you'll need your pipe and tobacco - your constant companions " +
    "during times of stress.",
    World.LocationType.House)
{
    protected override void InitializeItems()
    {
        AddItem(new Pipe());
    }
}