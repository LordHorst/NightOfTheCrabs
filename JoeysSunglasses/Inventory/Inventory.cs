using JoeysSunglasses.Inventory.Items;
using static JoeysSunglasses.Output;
namespace JoeysSunglasses.Inventory;

public class Inventory
{
    private List<Item?> items = new();
    private World.World _world;
    
    public Inventory(World.World world)
    {
        _world = world;
    }

    public bool AddItem(Item? item)
    {
        if (item == null)
            return false;
            
        item.SetGameState(_world, this);
        items.Add(item);
        return true;
    }

    public bool RemoveItem(Item? item)
    {
        var itemPresent = items.Contains(item);
        if (itemPresent)
            return items.Remove(item);
        
        return false;
    }
    
    public bool RemoveItem(string itemName)
    {
        var itemPresent = items.FirstOrDefault(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (itemPresent != null)
            return items.Remove(itemPresent);
        
        return false;
    }

    public bool GetItem(Item? item)
    {
        return items.Contains(item);
    }
    public Item? GetItem(string itemName)
    {
        var itemPresent = items.FirstOrDefault(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (itemPresent != null)
            return itemPresent;
        
        return null;
    }

    public bool HasItem(Item? item)
    {
        return items.Contains(item);
    }
    public bool HasItem(string itemName)
    {
        return items.Any(i => i != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    public void ListItems()
    {
        InventoryItems(items);
    }
}