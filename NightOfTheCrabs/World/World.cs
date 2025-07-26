using NightOfTheCrabs.World.House;
using NightOfTheCrabs.World.Llanbedr;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.World;

public class World
{
    public enum WorldLocationType
    {
        London,
        Llanbedr
    }
    public enum LocationType
    {
        House,
        Outside,
        Garden,
        Kitchen,
        Underwater,
        HotelLlanbedr,
        HotelDiningRoom,
        HotelRoomCliff,
        HotelRoomPat
    }
    [Flags]
    public enum LocationRequirements
    {
        None = 0,
        HasPipe = 1,
        HasTobacco = 2,
        HasTicket = 4,
        HasBackpack = 8,
        HasInformedMilitary = 256
        // Add more requirements as needed
    }
    
    private Location? _currentLocation;
    private readonly CharacterKnowledge _characterKnowledge = new();
    private readonly List<MovementRestriction> _movementRestrictions = new();
    private readonly Dictionary<string, TravelDestination> _travelDestinations = new();

    public World()
    {
        InitializeWorld();
        InitializeMovementRestrictions();
    }
    public void AddTravelDestination(TravelDestination destination)
    {
        _travelDestinations[destination.Command] = destination;
        foreach (var alias in destination.Aliases)
        {
            _travelDestinations[alias] = destination;
        }
    }
    public bool TryTravelTo(string destination)
    {
        if (!_travelDestinations.TryGetValue(destination.ToLower(), out var travelDest))
        {
            return false;
        }

        if (_currentLocation == null || !CanMove(_currentLocation.WorldLocationType!, travelDest.WorldDestination))
        {
            return false;
        }

        TypeWriteLine(travelDest.TravelMessage);
        SetCurrentLocation(destination);
        if (_currentLocation == null)
        {
            TypeWriteLine($"Something went wrong while trying to travel to the destination {destination}.");
            return false;
        }

        _currentLocation.DescribeLocation();
        return true;
    }

    private void SetCurrentLocation(string destination)
    {
        _travelDestinations.TryGetValue(destination, out var travelDest);
        _currentLocation = travelDest?.InitialLocation;
    }
    private void InitializeWorld()
    {
        // Create locations
        var kitchen = new Kitchen();
        var livingRoom = new LivingRoom();
        
        // Add exits to locations
        kitchen.AddExit("south", livingRoom);
        livingRoom.AddExit("north", kitchen);

        // Set starting location
        _currentLocation = livingRoom;
        
        // Add travel destinations
        InitTravelLocations(livingRoom);
        // Add movement restrictions
        InitializeMovementRestrictions();
    }

    private void InitTravelLocations(Location livingRoom)
    {
        var hotel = new Hotel();
        Console.WriteLine(hotel.WorldLocationType);
        AddTravelDestination(new TravelDestination(
            "llanbedr",
            ["hotel", "go llanbedr", "go hotel"],
            WorldLocationType.Llanbedr,
            hotel,
            "After a long journey in your car, you arrive at the Llanbedr Hotel. The salty breeze carries whispers of something unnatural."
        ));

        AddTravelDestination(new TravelDestination(
            "home",
            ["house", "go home", "return home"],
            WorldLocationType.London,
            livingRoom,
            "The mysteries of Llanbedr still weigh heavily on your mind. You still decide you have done what you could. You return home.",
            confirmationRequired: true
        ));
    }
    private void InitializeMovementRestrictions()
    {
        // Add movement restrictions
        _movementRestrictions.Add(new MovementRestriction(
            WorldLocationType.London,
            WorldLocationType.Llanbedr,
            LocationRequirements.HasPipe | LocationRequirements.HasTobacco,
            "You can't leave the house without your pipe and tobacco."
        ));

        // Add one-way restriction (can't return to house)
        _movementRestrictions.Add(new MovementRestriction(
            WorldLocationType.Llanbedr,
            WorldLocationType.London,
            LocationRequirements.HasInformedMilitary,
            "You can't return to the house now. Your mission awaits in Llanbedr.",
            confirmationRequired: true
        ));
    }
    private bool CanMove(WorldLocationType from, WorldLocationType to)
    {
        MovementRestriction? restriction = null;
        foreach (var r in _movementRestrictions)
        {
            if (r.FromLocation == from && r.ToLocation == to)
            {
                restriction = r;
                break;
            }
        }

        if (restriction == null)
            return true;

        if (restriction.ConfirmationRequired)
        {
            TypeWriteLine("You need to confirm your action. Do you want to go home? (Y/N)");
            var res = Console.ReadKey();
            if (res.Key != ConsoleKey.Y)
            {
                Console.WriteLine();
                TypeWriteLine("You cannot bear the thought of not solving this mystery. You decide to stay.");
                return false;
            }
        }

        // Check if all required items/conditions are met
        var requirements = restriction.Requirements;
        
        if (requirements == LocationRequirements.None)
            return false; // This is a hard restriction (like not being able to return to house)

        var hasRequirements = true;
        
        if (requirements.HasFlag(LocationRequirements.HasPipe))
            hasRequirements &= _characterKnowledge.HasDiscovered(KnowledgeType.HavePipe);
        
        if (requirements.HasFlag(LocationRequirements.HasTobacco))
            hasRequirements &= _characterKnowledge.HasDiscovered(KnowledgeType.HaveTobacco);

        // Add more requirement checks as needed

        if (!hasRequirements)
        {
            TypeWriteLine(restriction.RestrictionMessage);
            return false;
        }

        return true;
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
            
            if (!CanMove(_currentLocation.WorldLocationType, newLocation.WorldLocationType))
                return false;

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
    
    public string? GetCurrentLocationDebug()
    {
        var loc = _currentLocation?.LocationType + " - " + _currentLocation?.WorldLocationType;
        return loc;
    }

    public void DescribeCurrentLocation()
    {
        _currentLocation?.DescribeLocation();
    }
    
    public CharacterKnowledge GetCharacterKnowledge() => _characterKnowledge;
}