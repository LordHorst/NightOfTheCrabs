using NightOfTheCrabs.Inventory.Items;
using NightOfTheCrabs.Inventory.Items.House.LivingRoom;
using static NightOfTheCrabs.World.World;

namespace NightOfTheCrabs.World.House;

public class LivingRoom() : Location("Living Room",
    @"
The living room bears the quiet dignity of a man who lives alone and thinks deeply. Shelves of hardbound books line the walls—marine biology, natural history, a smattering of Latin texts, and journals with yellowed spines. The wallpaper is a muted, olive-toned damask, slightly faded by years of sunlight. A brass floor lamp casts a warm, honeyed glow over a worn leather armchair and the walnut side table beside it.
A framed map of the British Isles hangs above the mantel, its edges curled from age. Below it, the fireplace gives off a modest heat, crackling gently with coal embers.
The room smells faintly of old paper, pipe smoke, and furniture polish—orderly, masculine, and faintly melancholic.

A reel-to-reel tape recorder sits beside a stack of notebooks, some half-filled with sketches of crustaceans and marginalia in your neat, angular script.
The only sign of recent disruption is the telegram resting beside the whisky glass on the table — its message folded, read, and reread.

It is a room made for quiet thought, not comfort. A place to think, to plan. A sanctuary from the irrational.",
    World.LocationType.House)
{
    protected override void InitializeItems()
    {
        AddItem(new Pipe());
        AddItem(new WhiskeyGlass());
    }
}