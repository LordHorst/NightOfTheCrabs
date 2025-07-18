using JoeysSunglasses.World.House;
using static JoeysSunglasses.Output;

namespace JoeysSunglasses.World;

public class World
{
    public enum LocationType
    {
        House,
        Outside,
        Garden,
        Kitchen,
        Underwater
    }
    
    private readonly Dictionary<string, Location?> _locations;
    private Location? _currentLocation;

    public World()
    {
        _locations = new Dictionary<string, Location?>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        // Create locations
        var kitchen = new Kitchen();
        var livingRoom = new LivingRoom();
        
        // Add exits to locations
        kitchen.AddExit("south", livingRoom);
        livingRoom.AddExit("north", kitchen);

        // Add locations to world
        _locations.Add("kitchen", kitchen);
        _locations.Add("livingroom", livingRoom);

        // Set starting location
        _currentLocation = livingRoom;
    }

    public bool TryMove(string direction)
    {
        if (_currentLocation != null)
        {
            if (direction.Trim().Length == 1)
                direction = Translate(direction);
            var newLocation = _currentLocation.GetExit(direction);
            if (newLocation == null)
            {
                TypeWriteLine($"You can't go {direction}.");
                return false;
            }

            _currentLocation = newLocation;
            _currentLocation.DescribeLocation();
        }
        
        return true;
    }

    private string Translate(string direction)
    {
        switch (direction)
        {
            case "n":
                return "north";
            case "e":
                return "e";
            case "w":
                return "west";
            case "s":
                return "south";
            case "u":
                return "up";
            case "d":
                return "down";
            default:
                return "";
        }
    }
    public void DescribeCurrentLocation(bool forceFullDescription = false)
    {
        _currentLocation?.DescribeLocation(forceFullDescription);
    }


    public Location? GetCurrentLocation() => _currentLocation;

    public void DescribeCurrentLocation()
    {
        _currentLocation?.DescribeLocation();
    }
}