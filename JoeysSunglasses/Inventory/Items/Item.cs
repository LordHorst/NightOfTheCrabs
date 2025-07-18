using JoeysSunglasses.World;
using static JoeysSunglasses.World.World;

namespace JoeysSunglasses.Inventory.Items;

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanBePickedUp { get; set; } = true;
    public bool CanBeUsed { get; set; } = false;
    
    protected List<LocationType> AllowedLocations { get; set; }
    protected List<LocationType> DisallowedLocations { get; set; }
    protected bool RemoveAfterUse { get; set; } = false;
    
    private Inventory? _inventory;
    protected World.World? _world;


    public Item(string name, string description)
    {
        Name = name;
        Description = description;
        AllowedLocations = new List<LocationType>();
        DisallowedLocations = new List<LocationType>();
    }
    
    public void SetGameState(World.World world, Inventory inventory)
    {
        _world = world;
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
    
    protected void SetAllowedLocations(params LocationType[] locations)
    {
        AllowedLocations.Clear();
        AllowedLocations.AddRange(locations);
    }

    protected void SetDisallowedLocations(params LocationType[] locations)
    {
        DisallowedLocations.Clear();
        DisallowedLocations.AddRange(locations);
    }
    public virtual string GetLocationDescription(LocationType locationType)
    {
        // Default implementation just returns the item name
        return Name;
    }
}