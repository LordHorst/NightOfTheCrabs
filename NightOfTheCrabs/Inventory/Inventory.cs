using NightOfTheCrabs.Inventory.Items;
using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs.Inventory;

public class Inventory(World.World world)
{
    private readonly List<Item?> _items = [];

    public bool AddItem(Item? item)
    {
        if (item == null)
            return false;
            
        item.SetGameState(world, this);
        _items.Add(item);
        return true;
    }

    public bool RemoveItem(Item? item)
    {
        var itemPresent = _items.Contains(item);
        if (itemPresent)
            return _items.Remove(item);
        
        return false;
    }
    
    public bool RemoveItem(string itemName)
    {
        var itemPresent = _items.FirstOrDefault(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (itemPresent != null)
            return _items.Remove(itemPresent);
        
        return false;
    }

    public bool GetItem(Item? item)
    {
        return _items.Contains(item);
    }
    public Item? GetItem(string itemName)
    {
        var itemPresent = _items.FirstOrDefault(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (itemPresent != null)
            return itemPresent;
        
        return null;
    }

    public bool HasItem(Item? item)
    {
        return _items.Contains(item);
    }
    public bool HasItem(string itemName)
    {
        return _items.Any(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    public void ListItems()
    {
        InventoryItems(_items);
    }
}