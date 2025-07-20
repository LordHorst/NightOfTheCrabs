using static NightOfTheCrabs.World.World;
namespace NightOfTheCrabs.World.Llanbedr;

public class Hotel() : Location(
    "Llanbedr Hotel",
    "Your favorite place to stay in Llanbedr. A hotel run by Mrs. Jones, whom you call 'Mum'",
    WorldLocationType.Llanbedr,
    LocationType.HotelLlanbedr)
{
    protected override void InitializeItems()
    {
        
    }
}