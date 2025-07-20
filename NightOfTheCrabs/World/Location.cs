using NightOfTheCrabs.Inventory.Items;
using static NightOfTheCrabs.Output;
using static NightOfTheCrabs.World.World;

namespace NightOfTheCrabs.World;

public abstract class Location
{
    private string Name { get; }
    private string Description { get; }
    public WorldLocationType WorldLocationType { get; }
    public LocationType LocationType { get; }
    protected readonly List<Item> Items;
    private readonly Dictionary<string, Location?> _exits;
    private bool _hasBeenVisited;

    protected Location(string name, string description, WorldLocationType worldLocationType, LocationType locationType)
    {
        Name = name;
        Description = description;
        LocationType = locationType;
        Items = [];
        _exits = new Dictionary<string, Location?>();
        _hasBeenVisited = false; // Initialize as not visited
        // ReSharper disable once VirtualMemberCallInConstructor
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
        Items.Add(item);
    }

    public Item? RemoveItem(string itemName)
    {
        var item = Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
            Items.Remove(item);

        return item;
    }

    public bool HasItem(string itemName)
    {
        return Items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    public Location? GetExit(string direction)
    {
        return _exits.GetValueOrDefault(direction.ToLower());
    }

    public void DescribeLocation(bool forceFullDescription = false)
    {
        TypeWriteLine($"\n{Name}");
        if (!_hasBeenVisited || forceFullDescription)
        {
            TypeWriteLine(Description);
            _hasBeenVisited = true;
        }

        if (Items.Any())
        {
            TypeWriteLine("\nYou can see:");
            foreach (var item in Items.ToList())
            {
                TypeWriteLine($"- {item.GetLocationDescription(LocationType)}");
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
        return LocationType switch
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
            Items.FirstOrDefault(i => i != null && i.Name.Equals(possItem, StringComparison.OrdinalIgnoreCase));
        if (itemPresent != null)
            return itemPresent;

        return null;
    }
}