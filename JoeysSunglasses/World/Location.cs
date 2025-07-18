using JoeysSunglasses.Inventory.Items;
using static JoeysSunglasses.Output;
using static JoeysSunglasses.World.World;

namespace JoeysSunglasses.World;

public abstract class Location
{
    public string Name { get; }
    public string Description { get; }
    public LocationType LType { get; }
    public List<Item> _items;
    private Dictionary<string, Location> _exits;

    public Location(string name, string description, LocationType lType)
    {
        Name = name;
        Description = description;
        LType = lType;
        _items = new List<Item>();
        _exits = new Dictionary<string, Location>();
        InitializeItems(); // Called during construction
    }

    // Abstract method that derived classes must implement to set up their initial items
    protected abstract void InitializeItems();

    public void AddExit(string direction, Location destination)
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
        {
            _items.Remove(item);
        }

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

    public void DescribeLocation()
    {
        TypeWriteLine($"\n{Name}");
        TypeWriteLine(Description);

        if (_items.Any())
        {
            TypeWriteLine("\nYou can see:");
            foreach (var item in _items)
            {
                TypeWriteLine($"- {item.Name}");
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
}