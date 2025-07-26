using NightOfTheCrabs.World.House;
using NightOfTheCrabs.World.Llanbedr;

namespace NightOfTheCrabs.World;

public static class LocationExtensions
{
    public static Location ToLocation(this World.WorldLocationType worldLocationType) => worldLocationType switch
    {
        World.WorldLocationType.London => new LivingRoom(),
        World.WorldLocationType.Llanbedr => new Hotel(),
        _ => throw new ArgumentException($"Unknown world location type: {worldLocationType}")
    };
}