using static NightOfTheCrabs.World.World;
using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs.Inventory.Items;

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanBePickedUp { get; set; }
    public bool CanBeUsed { get; set; } = false;
    public bool QuestItem { get; set; } = false;
    
    protected List<World.World.LocationType> AllowedLocations { get; set; }
    protected List<World.World.LocationType> DisallowedLocations { get; set; }
    protected bool RemoveAfterUse { get; set; } = false;
    
    protected Inventory? _inventory;
    protected World.World? _world;


    public Item(string name, string description, bool canBePickedUp = true)
    {
        Name = name;
        Description = description;
        CanBePickedUp = canBePickedUp;
        AllowedLocations = new List<LocationType>();
        DisallowedLocations = new List<LocationType>();
    }
    
    protected bool Init()
    {
        if (_world == null)
        {
            TypeWriteLine("Error: World not properly initialized");
            return false;
        }

        if (_inventory == null)
        {
            TypeWriteLine("Error: Inventory not properly initialized");
            return false;
        }

        return true;
    }
    
    public void SetGameState(World.World world, Inventory inventory)
    {
        if (world != null && _world != world)
            _world = world;
        if(inventory != null && _inventory != inventory)
            _inventory = inventory;
    }

    protected bool RemoveFromInventory()
    {
        if (_inventory == null) return false;
        return _inventory.RemoveItem(Name);
    }

    public virtual string Examine()
    {
        return Description;
    }

    public virtual string Use()
    {
        if (_world == null)
            return "Error: Item not properly initialized";
        
        var currentLocation = _world.GetCurrentLocation();
        if (currentLocation != null)
        {
            var locationType = currentLocation.LType;

            if (!CanBeUsed)
                return $"You can't use the {Name}.";
        
            if (DisallowedLocations.Contains(locationType))
                return $"You can't use the {Name} here.";

            if (AllowedLocations.Any() && !AllowedLocations.Contains(locationType))
                return $"You can't use the {Name} here.";
        }

        string result = $"You use the {Name}.";
        
        if (RemoveAfterUse)
        {
            RemoveFromInventory();
        }
            
        return result;
    }
    
    protected void SetAllowedLocations(params World.World.LocationType[] locations)
    {
        AllowedLocations.Clear();
        AllowedLocations.AddRange(locations);
    }

    protected void SetDisallowedLocations(params World.World.LocationType[] locations)
    {
        DisallowedLocations.Clear();
        DisallowedLocations.AddRange(locations);
    }
    public virtual string GetLocationDescription(World.World.LocationType locationType)
    {
        // Default implementation just returns the item name
        return Name;
    }
    public virtual string GetCantPickUpReason()
    {
        return CanBePickedUp ? string.Empty : $"The {Name} is too heavy to carry.";
    }
}