using NightOfTheCrabs.Inventory.Items;
using static NightOfTheCrabs.Output;
using static NightOfTheCrabs.World.World;

namespace NightOfTheCrabs.World;

public abstract class Location
{
    public string Name { get; }
    public string Description { get; }
    public World.LocationType LType { get; }
    public List<Item> _items;
    private Dictionary<string, Location?> _exits;
    private bool _hasBeenVisited;

    public Location(string name, string description, World.LocationType lType)
    {
        Name = name;
        Description = description;
        LType = lType;
        _items = new List<Item>();
        _exits = new Dictionary<string, Location?>();
        _hasBeenVisited = false; // Initialize as not visited
        InitializeItems(); // Called during construction
    }

    // Abstract method that derived classes must implement to set up their initial items
    protected abstract void InitializeItems();

    public void AddExit(string direction, Location? destination)
    {
        _exits[direction.ToLower()] = destination;
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public Item? RemoveItem(string itemName)
    {
        var item = _items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
            _items.Remove(item);

        return item;
    }

    public bool HasItem(string itemName)
    {
        return _items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    public Location? GetExit(string direction)
    {
        return _exits.TryGetValue(direction.ToLower(), out var location) ? location : null;
    }

    public void DescribeLocation(bool forceFullDescription = false)
    {
        TypeWriteLine($"\n{Name}");
        if (!_hasBeenVisited || forceFullDescription)
        {
            TypeWriteLine(Description);
            _hasBeenVisited = true;
        }

        if (_items.Any())
        {
            TypeWriteLine("\nYou can see:");
            foreach (var item in _items.ToList())
            {
                TypeWriteLine($"- {item.GetLocationDescription(LType)}");
            }
        }

        if (_exits.Any())
        {
            TypeWriteLine("\nExits:");
            foreach (var exit in _exits)
            {
                TypeWriteLine($"- {exit.Key}");
            }
        }
    }

    public virtual string GetDropDescription(string itemName)
    {
        return LType switch
        {
            LocationType.Kitchen => $"You put the {itemName} on the kitchen counter",
            LocationType.House => $"You put the {itemName} on the table",
            LocationType.Garden => $"You put the {itemName} on the garden bench",
            LocationType.Outside => $"You put the {itemName} on the ground",
            LocationType.Underwater => $"You release the {itemName} into the water",
            _ => $"You drop the {itemName}"
        };
    }

    public Item? GetItem(string possItem)
    {
        var itemPresent =
            _items.FirstOrDefault(i => i != null && i.Name.Equals(possItem, StringComparison.OrdinalIgnoreCase));
        if (itemPresent != null)
            return itemPresent;

        return null;
    }
}