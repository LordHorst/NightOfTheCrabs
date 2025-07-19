using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.LivingRoom;

public class WhiskeyGlass : Item
{
    private bool _isFull = true;

    public WhiskeyGlass()
        : base("Whiskey Glass", "A whiskey glass. It is full.")
    {
        CanBeUsed = true;
        SetDisallowedLocations(World.World.LocationType.Underwater);
    }

    public override string Use()
    {
        if (!Init())
            return "";

        if (_isFull)
        {
            var currentLocation = _world?.GetCurrentLocation();
            if (_inventory != null && !_inventory.HasItem("tobacco"))
                TypeWriteLine(
                    "You really want to smoke your pipe, but you are missing a vital ingredient. You are out of tobacco!");
            else if (currentLocation != null && DisallowedLocations.Contains(currentLocation.LType))
                TypeWriteLine(
                    "You cannot drink whiskey while being underwater.");
            else
            {
                TypeWriteLine(
                    "You swallow the whiskey in one go. The strong alcohol sends waves of warmess through your body.");
                _isFull = false;
                Description = "A whiskey glass. It's empty.";
            }
        }
        else
        {
            TypeWriteLine("The glass is empty");
        }

        return "";
    }

    public override string GetLocationDescription(World.World.LocationType locationType)
    {
        var returnText = locationType switch
        {
            World.World.LocationType.House => "A whiskey glass on a table besides the arm-chair",
            World.World.LocationType.Kitchen => "A whiskey glass on the kitchen counter",
            World.World.LocationType.Garden => "A whiskey glass on a garden bench",
            World.World.LocationType.Outside => "A whiskey glass on the ground",
            _ => "A whiskey glass"
        };

        return returnText + (_isFull ? ". It is full." : ". It is empty");
    }
}