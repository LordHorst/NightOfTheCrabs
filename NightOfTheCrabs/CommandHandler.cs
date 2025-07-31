using NightOfTheCrabs.Inventory.Items;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs;

public enum CommandType
{
    Exit,
    Take,
    Drop,
    Move,
    Use,
    Examine,
    ExamineItem,
    Inventory,
    Unknown
}

public class CommandHandler
{
    private readonly World.World _world;
    //private readonly Inventory_Inventory _inv;
    private readonly string[] _direction;

    private static readonly Dictionary<string, CommandType> Commands = new(StringComparer.OrdinalIgnoreCase)
    {
        // Single word commands
        { "quit", CommandType.Exit },
        { "exit", CommandType.Exit },
        { "inventory", CommandType.Inventory },
        { "inv", CommandType.Inventory },
        { "i", CommandType.Inventory },
        { "list", CommandType.Inventory }
    };

    private static readonly Dictionary<string, CommandType> PrefixCommands = new(StringComparer.OrdinalIgnoreCase)
    {
        { "take", CommandType.Take },
        { "pick up", CommandType.Take },
        { "drop", CommandType.Drop },
        { "put down", CommandType.Drop },
        { "use", CommandType.Use },
        { "examine", CommandType.ExamineItem },
        { "look", CommandType.ExamineItem }
    };

    private static readonly string[] MovementPrefixes = ["go", "move", "run", "travel", "head"];

    public CommandHandler(World.World world, /*Inventory_Inventory inv, */string[] direction)
    {
        _world = world;
        //_inv = inv;
        _direction = direction;
    }

    public void HandleCommand(string userAction)
    {
        var normalizedInput = userAction.ToLower().Trim();
#if DEBUG
        if (normalizedInput.Equals("cl"))
        {
            Console.WriteLine(_world.GetCurrentLocationDebug());
            return;
        }
#endif
        var (commandType, remainingText) = DetermineCommandTypeAndRemainingText(normalizedInput);

        switch (commandType)
        {
            case CommandType.Exit:
                HandleExit();
                break;
            case CommandType.Take:
                HandleItemAction(remainingText, TakeItem, "take");
                break;
            case CommandType.Drop:
                HandleItemAction(remainingText, DropItem, "drop");
                break;
            case CommandType.Move:
                HandleMoveCommand(normalizedInput);
                break;
            case CommandType.Use:
                HandleItemAction(remainingText, UseItem, "use");
                break;
            case CommandType.Examine:
                _world.DescribeCurrentLocation(forceFullDescription: true);
                break;
            case CommandType.ExamineItem:
                HandleItemAction(remainingText,
                    item => TypeWriteLine(item?.Examine() ?? "I found no item to examine."),
                    "examine");
                break;
            case CommandType.Inventory:
                _world.GetInventory().ListItems();
                break;
            case CommandType.Unknown:
            default:
                TypeWriteLine("I'm sorry, I didn't understand that.");
                break;
        }
    }

    private (CommandType type, string remainingText) DetermineCommandTypeAndRemainingText(string input)
    {
        if (Commands.TryGetValue(input, out var commandType))
            return (commandType, string.Empty);
        
        if (input.StartsWith("look around") 
            || input.StartsWith("examine room")
            || input.StartsWith("examine area")
            || input.StartsWith("examine location")
            || input.Equals("look")
            || input.Equals("examine")
            || input.Equals("search"))
            return (CommandType.Examine, string.Empty);

        foreach (var (prefix, type) in PrefixCommands)
        {
            if (input.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return (type, input[prefix.Length..].Trim());
        }

        if (MovementPrefixes.Any(prefix => input.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            || _direction.Contains(input))
            return (CommandType.Move, input);

        return (CommandType.Unknown, input);
    }
    
    private void HandleItemAction(string itemName, Action<Item?> action, string actionName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            TypeWriteLine($"I don't know what to {actionName}.");
            return;
        }

        action(GetItem(itemName));
    }

    private static void HandleExit()
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }

    private void HandleMoveCommand(string input)
    {
        if (MovementPrefixes.Any(cmd => input.StartsWith(cmd, StringComparison.OrdinalIgnoreCase)))
        {
            var destination = input.Split(' ', 2)[1];
            _world.TryTravelTo(destination);
            return;
        }

        if (_world.TryTravelTo(input))
            return;

        foreach (var direction in _direction)
        {
            if (input.Contains(direction))
            {
                _world.TryMove(direction);
                return;
            }
        }
    }
    /* special item handlers */
    private void TakeItem(Item? item)
    {
        if (item == null)
        {
            TypeWriteLine("That item doesn't exist.");
            return;
        }

        var currentLocation = _world.GetCurrentLocation();
        if (currentLocation == null || !currentLocation.HasItem(item.Name))
        {
            TypeWriteLine($"There is no {item.Name} here to take.");
            return;
        }

        if (!item.CanBePickedUp)
        {
            var reason = item.GetCantPickUpReason();

            if (!string.IsNullOrEmpty(reason)) // we can pick it up after all
            {
                TypeWriteLine(reason);
                return;
            }
        }

        if (currentLocation.RemoveItem(item.Name) is { } removedItem && _world.GetInventory().AddItem(removedItem))
        {
            item.OnPickUp();
            TypeWriteLine($"You pick up the {item.Name}.");
            return;
        }

        TypeWriteLine($"You couldn't pick up the {item.Name}.");
    }

    private void DropItem(Item? item)
    {
        if (item == null)
        {
            TypeWriteLine("That item doesn't exist.");
            return;
        }

        var currentLocation = _world.GetCurrentLocation();
        if (currentLocation == null)
        {
            TypeWriteLine("Something went wrong with the current location.");
            return;
        }

        if (!_world.GetInventory().HasItem(item.Name))
        {
            TypeWriteLine($"You don't have {item.Name} to drop.");
            return;
        }

        if (_world.GetInventory().RemoveItem(item.Name))
        {
            item.OnDrop();
            currentLocation.AddItem(item);
            TypeWriteLine(currentLocation.GetDropDescription(item.Name));
            return;
        }

        TypeWriteLine($"Failed to drop the {item.Name}.");
    }

    private Item? GetItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName)) 
            return null;

        return _world.GetInventory().GetItem(itemName) ?? _world.GetCurrentLocation()?.GetItem(itemName);
    }

    private void UseItem(Item? item)
    {
        if (item == null)
        {
            TypeWriteLine("That item doesn't exist.");
            return;
        }

        var locationHasItem = _world.GetCurrentLocation()?.HasItem(item.Name) ?? false;

        if (!_world.GetInventory().HasItem(item.Name) && !locationHasItem)
        {
            TypeWriteLine($"You don't have a/the {item.Name} and you can't find it here.");
            return;
        }

        TypeWriteLine(item.Use());
    }
}