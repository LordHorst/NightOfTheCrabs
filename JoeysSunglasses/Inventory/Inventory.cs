using JoeysSunglasses.Inventory.Items;
using static JoeysSunglasses.Output;
namespace JoeysSunglasses.Inventory;

public class Inventory
{
    private List<Item> items = new();

    public bool AddItem(Item item)
    {
        if (item == null)
            return false;
            
        items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item)
    {
        var itemPresent = items.Contains(item);
        if (itemPresent)
            return items.Remove(item);
        
        return false;
    }

    public bool GetItem(Item item)
    {
        return items.Contains(item);
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }
    public bool HasItem(string itemName)
    {
        return items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    public void ListItems()
    {
        InventoryItems(items);
    }
}