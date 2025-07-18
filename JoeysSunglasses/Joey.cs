using static JoeysSunglasses.Output;
using myInv = JoeysSunglasses.Inventory;
using JoeysSunglasses.Inventory.Items;
using JoeysSunglasses.World;

namespace JoeysSunglasses;

public class Joey
{
    private myInv.Inventory _inv;
    private World.World _world;

    private string[] _direction = new[]
    {
        "north", "n",
        "south", "s",
        "east", "e",
        "west", "w",
        "up", "u",
        "down", "d"
    };

    public Joey()
    {
        _world = new World.World();
        _inv = new myInv.Inventory(_world);
    }

    private bool _gameBeat = false;

    public void StartGame()
    {
        TypeWriteLine("Joey's Sunglasses");
        // Force full description of starting location
        _world.DescribeCurrentLocation(forceFullDescription: true);

        TypeWriteLine(@"Joey says 'Gimme my sunglasses'.");
        TypeWriteLine();
        TypeWriteLine("What do you do?");
        while (true)
        {
            TypeWrite("? ");

            var userAction = Console.ReadLine();
            if (userAction == null)
            {
                TypeWriteLine("Something went wrong here, I'm sorry!");
                break;
            }

            HandleCommand(userAction);
        }

        if (_gameBeat)
            TypeWriteLine("You beat the game!");
        else
        {
            TypeWriteLine("You lost.");
        }
    }

    private void HandleCommand(string userAction)
    {
        userAction = userAction.ToLower().Trim();
        if(userAction.Equals("quit") || userAction.Equals("exit"))
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
        
        if (userAction.StartsWith("take") || userAction.StartsWith("pick up"))
        {
            var userItemSplit = userAction.Split(" ");
            if (userItemSplit.Length > 1)
                TakeItem(userItemSplit[^1]);
            else
                TypeWriteLine("I don't know how to take that.");
        }
        else if (userAction.StartsWith("drop") || userAction.StartsWith("put down"))
        {
            var userItemSplit = userAction.Split(" ");
            if (userItemSplit.Length > 1)
                DropItem(userItemSplit[^1]);
            else
                TypeWriteLine("I don't know how to drop that.");
        }
        else if (userAction.StartsWith("go") || userAction.StartsWith("move") || _direction.Contains(userAction))
        {
            foreach (var direction in _direction)
            {
                if (userAction.Contains(direction))
                {
                    _world.TryMove(direction);
                    return;
                }
            }
        }
        else if (userAction.StartsWith("use"))
        {
            var userItemSplit = userAction.Split(" ");
            Item? item = GetItem(userItemSplit);
            if (item != null)
                UseItem(item);
            else
                TypeWriteLine("I found no item to use.");
        }
        else if (userAction.Equals("examine") || userAction.Equals("look")) //nur Welt beschreiben
        {
            _world.DescribeCurrentLocation(forceFullDescription: true);
        }
        else if (userAction.StartsWith("examine") && !userAction.Equals("examine")) //Item beschreiben
        {
            var userItemSplit = userAction.Split(" ");
            Item? item = GetItem(userItemSplit);
            if (item != null)
                TypeWriteLine(item.Description);
            else
                TypeWriteLine("I found no item to examine.");
        }
        else if (userAction.Equals("list") || userAction.Equals("inventory") || userAction.Equals("inv"))
        {
            _inv.ListItems();
        }
        else
            TypeWriteLine("I'm sorry, I didn't understand that.");
        // ... handle other commands
    }

    private bool TakeItem(string itemName)
    {
        var currentLocation = _world.GetCurrentLocation();
        if (currentLocation == null)
        {
            TypeWriteLine("Something went wrong with the current location.");
            return false;
        }

        if (!currentLocation.HasItem(itemName))
        {
            TypeWriteLine($"There is no {itemName} here to take.");
            return false;
        }

        var item = currentLocation.RemoveItem(itemName);
        if (item == null)
        {
            TypeWriteLine($"Failed to take the {itemName}.");
            return false;
        }

        if (_inv.AddItem(item))
        {
            TypeWriteLine($"You pick up the {itemName}.");
            return true;
        }
        else
        {
            // If inventory addition fails, put the item back in the location
            currentLocation.AddItem(item);
            TypeWriteLine($"You couldn't pick up the {itemName}.");
            return false;
        }
    }

    private bool DropItem(string itemName)
    {
        var currentLocation = _world.GetCurrentLocation();
        if (currentLocation == null)
        {
            TypeWriteLine("Something went wrong with the current location.");
            return false;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            TypeWriteLine("You need to specify an item to drop.");
            return false;
        }
        if (!_inv.HasItem(itemName))
        {
            TypeWriteLine($"You don't have {itemName} to drop.");
            return false;
        }

        var item = _inv.GetItem(itemName);
        if (item == null)
        {
            TypeWriteLine($"Failed to get the {itemName} from your inventory.");
            return false;
        }

        if (_inv.RemoveItem(itemName))
        {
            currentLocation.AddItem(item);
            TypeWriteLine(currentLocation.GetDropDescription(itemName));
            return true;
        }
        else
        {
            TypeWriteLine($"Failed to drop the {itemName}.");
            return false;
        }
    }

    private Item? GetItem(string[] userItemSplit)
    {
        Item? item;
        foreach (var possItem in userItemSplit)
        {
            item = _inv.GetItem(possItem);
            if (item != null)
                return item;
        }

        return null;
    }

    private void UseItem(Item? item)
    {
        var hasItem = _inv.GetItem(item);
        if (!hasItem)
        {
            TypeWriteLine($"You don't have {item?.Name}.");
            return;
        }

        TypeWriteLine(item?.Use());
    }
}