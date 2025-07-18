using static JoeysSunglasses.Output;
using myInv = JoeysSunglasses.Inventory;
using JoeysSunglasses.Inventory.Items;

namespace JoeysSunglasses;

public class Joey
{
    private myInv.Inventory _inv;
    private World.World _world;
    public Joey()
    {
        _inv = new myInv.Inventory();
        _world = new World.World();
    }

    public void StartGame()
    {
        TypeWriteLine("Joey's Sunglasses");
        var sunglasses = new Sunglasses();
        _world.GetCurrentLocation().AddItem(sunglasses);

        TypeWriteLine(
            "You stand in a room. Joey says 'Gimme my sunglasses'. You see the sunglasses lying on the table in front of you.");
        TypeWriteLine();
        TypeWriteLine("What do you do?");
        while (!_inv.HasItem(sunglasses))
        {
            var userAction = Console.ReadLine();
            if (userAction == null)
                break;
            if (userAction.ToLower().Contains("pick") &&
                userAction.ToLower().Contains("sunglasses"))
            {
                TypeWriteLine("You pick up the sunglasses and go home. (thanks AI)");
                _inv.AddItem(sunglasses);
            }
            else if (userAction.ToLower().Contains("leave") ||
                     userAction.ToLower().Contains("exit"))
                break;
            else
                TypeWriteLine("You go back to the room and try again.");
        }

        if (_inv.HasItem(sunglasses))
        {
            UseItem(sunglasses);
            TypeWriteLine("You are happy and happy and happy.");
            TypeWriteLine("You've won the game! Congratulations!");
        }
        else
        {
            TypeWriteLine("You are sad and sad and sad.");
            TypeWriteLine("You've lost the game! Better luck next time.");
            TypeWriteLine("Joey is very disappointed in you.");
        }
    }
    
    public void HandleCommand(string userAction)
    {
        userAction = userAction.ToLower();
    
        if (userAction.Contains("take") || userAction.Contains("pick up"))
        {
            foreach (var itemName in new[] { "sunglasses", "knife", "cookie" })
            {
                if (userAction.Contains(itemName))
                {
                    var currentLocation = _world.GetCurrentLocation();
                    if (currentLocation.HasItem(itemName))
                    {
                        var item = currentLocation.RemoveItem(itemName);
                        if (item != null)
                        {
                            _inv.AddItem(item);
                            TypeWriteLine($"You pick up the {itemName}.");
                            return;
                        }
                    }
                    else
                    {
                        TypeWriteLine($"There is no {itemName} here to take.");
                        return;
                    }
                }
            }
        }
        else if (userAction.Contains("go") || userAction.Contains("move"))
        {
            foreach (var direction in new[] { "north", "south", "east", "west" })
            {
                if (userAction.Contains(direction))
                {
                    _world.TryMove(direction);
                    return;
                }
            }
        }
        // ... handle other commands
    }
    public void UseItem(Item item)
    {
        var has_item = _inv.GetItem(item);
        if (!has_item)
        {
            TypeWriteLine($"You don't have {item.Name}.");
            return;
        }

        var currentLocation = _world.GetCurrentLocation();
        TypeWriteLine(item.Use(currentLocation.LType));
    }

}