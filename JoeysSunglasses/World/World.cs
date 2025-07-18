using JoeysSunglasses.Inventory.Items;
using JoeysSunglasses.Inventory.Items.House;
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
        Kitchen
    }
    
    private Dictionary<string, Location> _locations;
    private Location _currentLocation;

    public World()
    {
        _locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        // Create locations
        var startingRoom = new StartingRoom();
        var kitchen = new Kitchen();
        
        // Add exits to locations
        startingRoom.AddExit("north", kitchen);
        kitchen.AddExit("south", startingRoom);

        // Add locations to world
        _locations.Add("startingroom", startingRoom);
        _locations.Add("kitchen", kitchen);

        // Set starting location
        _currentLocation = startingRoom;

    }

    public bool TryMove(string direction)
    {
        var newLocation = _currentLocation.GetExit(direction);
        if (newLocation == null)
        {
            TypeWriteLine($"You can't go {direction}.");
            return false;
        }

        _currentLocation = newLocation;
        _currentLocation.DescribeLocation();
        return true;
    }

    public Location GetCurrentLocation() => _currentLocation;

    public void DescribeCurrentLocation()
    {
        _currentLocation.DescribeLocation();
    }
}