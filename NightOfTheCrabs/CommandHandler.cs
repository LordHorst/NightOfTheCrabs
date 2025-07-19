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

public class CommandHandler(World.World world, Inventory_Inventory inv, string[] direction)
{
    private static readonly string[] ExitCommands = ["quit", "exit"];
    private static readonly string[] TakeCommands = ["take", "pick up"];
    private static readonly string[] DropCommands = ["drop", "put down"];
    private static readonly string[] MoveCommands = ["go", "move", "run"];
    private static readonly string[] ExamineCommands = ["examine", "look"];
    private static readonly string[] InventoryCommands = ["list", "inventory", "inv", "i"];

    public void HandleCommand(string userAction)
    {
        var normalizedInput = userAction.ToLower().Trim();
        var (commandType, remainingText) = DetermineCommandTypeAndRemainingText(normalizedInput);

        switch (commandType)
        {
            case CommandType.Exit:
                HandleExit();
                break;
            case CommandType.Take:
                HandleTakeCommand(remainingText);
                break;
            case CommandType.Drop:
                HandleDropCommand(remainingText);
                break;
            case CommandType.Move:
                HandleMoveCommand(normalizedInput);
                break;
            case CommandType.Use:
                HandleUseCommand(remainingText);
                break;
            case CommandType.Examine:
                world.DescribeCurrentLocation(forceFullDescription: true);
                break;
            case CommandType.ExamineItem:
                HandleExamineItemCommand(remainingText);
                break;
            case CommandType.Inventory:
                inv.ListItems();
                break;
            case CommandType.Unknown:
            default:
                TypeWriteLine("I'm sorry, I didn't understand that.");
                break;
        }
    }
    
    private (CommandType type, string remainingText) DetermineCommandTypeAndRemainingText(string input)
    {
        // Check exact matches first
        if (ExitCommands.Any(cmd => input.Equals(cmd)))
            return (CommandType.Exit, string.Empty);
        if (ExamineCommands.Any(cmd => input.Equals(cmd)))
            return (CommandType.Examine, string.Empty);
        if (InventoryCommands.Any(cmd => input.Equals(cmd)))
            return (CommandType.Inventory, string.Empty);

        // Check for compound commands
        foreach (var takeCmd in TakeCommands)
        {
            if (input.StartsWith(takeCmd))
                return (CommandType.Take, input[takeCmd.Length..].Trim());
        }

        foreach (var dropCmd in DropCommands)
        {
            if (input.StartsWith(dropCmd))
                return (CommandType.Drop, input[dropCmd.Length..].Trim());
        }

        foreach (var moveCmd in MoveCommands)
        {
            if (input.StartsWith(moveCmd))
                return (CommandType.Move, input);
        }

        if (input.StartsWith("use"))
            return (CommandType.Use, input[3..].Trim());

        foreach (var examineCmd in ExamineCommands)
        {
            if (input.StartsWith(examineCmd) && input.Length > examineCmd.Length)
                return (CommandType.ExamineItem, input[examineCmd.Length..].Trim());
        }

        if (direction.Contains(input))
            return (CommandType.Move, input);

        if (string.IsNullOrEmpty(input))
            input = string.Empty;

        return (CommandType.Unknown, input);
    }

    private static void HandleExit()
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }

    private void HandleTakeCommand(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            TypeWriteLine("I don't know how to take that.");
        else
            TakeItem(itemName);
    }

    private void HandleDropCommand(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            TypeWriteLine("I don't know how to drop that.");
        else
            DropItem(itemName);

    }
    
    private void HandleExamineItemCommand(string itemName)
    {
        var item = GetItem(itemName);
        TypeWriteLine(item != null ? item.Description : "I found no item to examine.");
    }

    private void HandleUseCommand(string itemName)
    {
        var item = GetItem(itemName);
        if (item != null)
            UseItem(item);
        else
            TypeWriteLine("I found no item to use.");
    }
    
    private void HandleMoveCommand(string input)
    {
        foreach (var direction1 in direction)
        {
            if (input.Contains(direction1))
            {
                world.TryMove(direction1);
                return;
            }
        }
    }

    /*
     * special item handlers
     */
    private void TakeItem(string itemName)
    {
        var currentLocation = world.GetCurrentLocation();
        if (currentLocation == null)
        {
            TypeWriteLine("Something went wrong with the current location.");
            return;
        }
        
        if (!currentLocation.HasItem(itemName))
        {
            TypeWriteLine($"There is no {itemName} here to take.");
            return;
        }

        var item = currentLocation.GetItem(itemName);
        if (item == null)
        {
            TypeWriteLine($"Failed to take the {item?.Name}.");
            return;
        }

        // Check if item can be picked up before attempting to take it
        if (!item.CanBePickedUp)
        {
            TypeWriteLine(item.GetCantPickUpReason());
            return;
        }

        item = currentLocation.RemoveItem(itemName);
        if (item == null)
        {
            TypeWriteLine($"Failed to take the {item?.Name}.");
            return;
        }

        if (inv.AddItem(item))
        {
            TypeWriteLine($"You pick up the {item.Name}.");
            return;
        }

        // If inventory addition fails, put the item back in the location
        currentLocation.AddItem(item);
        TypeWriteLine($"You couldn't pick up the {item.Name}.");
    }

    private void DropItem(string itemName)
    {
        var currentLocation = world.GetCurrentLocation();
        if (currentLocation == null)
        {
            TypeWriteLine("Something went wrong with the current location.");
            return;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            TypeWriteLine("You need to specify an item to drop.");
            return;
        }

        if (!inv.HasItem(itemName))
        {
            TypeWriteLine($"You don't have {itemName} to drop.");
            return;
        }

        var item = inv.GetItem(itemName);
        if (item == null)
        {
            TypeWriteLine($"Failed to get the {itemName} from your inventory.");
            return;
        }

        if (inv.RemoveItem(itemName))
        {
            currentLocation.AddItem(item);
            TypeWriteLine(currentLocation.GetDropDescription(itemName));
            return;
        }

        TypeWriteLine($"Failed to drop the {itemName}.");
    }

    private Item? GetItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName)) return null;

        var currentLocation = world.GetCurrentLocation();
        
        // Try to find item in inventory first
        var item = inv.GetItem(itemName);
        if (item != null) return item;

        // Then try to find in current location
        if (currentLocation?.HasItem(itemName) == true)
            return currentLocation.GetItem(itemName);

        return null;
    }

    private void UseItem(Item? item)
    {
        if (item is null)
        {
            TypeWriteLine("Something went wrong with the item, you cannot use it at the moment.");
            return;
        }

        // making sure 'world' isn't NULL
        item.SetGameState(world, inv);
        var locationHasItem = world.GetCurrentLocation()?.HasItem(item.Name) ?? false;
        var hasItem = inv.GetItem(item);
        if (!hasItem && !locationHasItem)
        {
            TypeWriteLine($"You don't have a/the {item.Name} and you can't find it here.");
            return;
        }

        TypeWriteLine(item.Use());
    }
}