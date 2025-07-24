using NightOfTheCrabs.Inventory.Items;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs;

using Inventory_Inventory = Inventory.Inventory;

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
    private readonly Inventory_Inventory _inv;
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

    public CommandHandler(World.World world, Inventory_Inventory inv, string[] direction)
    {
        _world = world;
        _inv = inv;
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
                HandleItemAction(remainingText, item => UseItem(item), "use");
                break;
            case CommandType.Examine:
                _world.DescribeCurrentLocation(forceFullDescription: true);
                break;
            case CommandType.ExamineItem:
                HandleItemAction(remainingText,
                    item => TypeWriteLine(item?.Description ?? "I found no item to examine."),
                    "examine");
                break;
            case CommandType.Inventory:
                _inv.ListItems();
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

        foreach (var (prefix, type) in PrefixCommands)
        {
            if (input.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return (type, input[prefix.Length..].Trim());
        }

        if (MovementPrefixes.Any(prefix => input.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            || _direction.Contains(input))
            return (CommandType.Move, input);

        if (input.StartsWith("look around") || input.StartsWith("examine room"))
            return (CommandType.Examine, string.Empty);

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
            TypeWriteLine(item.GetCantPickUpReason());
            return;
        }

        if (currentLocation.RemoveItem(item.Name) is { } removedItem && _inv.AddItem(removedItem))
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

        if (!_inv.HasItem(item.Name))
        {
            TypeWriteLine($"You don't have {item.Name} to drop.");
            return;
        }

        if (_inv.RemoveItem(item.Name))
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

        return _inv.GetItem(itemName) ?? 
               _world.GetCurrentLocation()?.GetItem(itemName);
    }

    private void UseItem(Item? item)
    {
        if (item == null)
        {
            TypeWriteLine("That item doesn't exist.");
            return;
        }

        item.SetGameState(_world, _inv);
        var locationHasItem = _world.GetCurrentLocation()?.HasItem(item.Name) ?? false;

        if (!_inv.HasItem(item.Name) && !locationHasItem)
        {
            TypeWriteLine($"You don't have a/the {item.Name} and you can't find it here.");
            return;
        }

        TypeWriteLine(item.Use());
    }
}