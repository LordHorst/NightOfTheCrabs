using NightOfTheCrabs.Inventory.Items;
using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs.Inventory;

public class Inventory
{
    //private World.World World;
    private readonly List<Item?> _items = [];

    public bool AddItem(Item? item)
    {
        if (item == null)
            return false;
            
        //item.SetGameState(World, this);
        if(item.Description.Equals(new Dummy().Description) )
            return false;
        _items.Add(item);
        return true;
    }

    public bool RemoveItem(Item? item)
    {
        var itemPresent = _items.Contains(item);
        return itemPresent && _items.Remove(item);
    }
    
    public bool RemoveItem(string itemName)
    {
        var itemPresent = _items.FirstOrDefault(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        return itemPresent != null && _items.Remove(itemPresent);
    }

    public bool GetItem(Item? item)
    {
        return _items.Contains(item);
    }
    public Item? GetItem(string itemName)
    {
        var itemPresent = _items.FirstOrDefault(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        return itemPresent ?? null;
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