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
        var commandType = DetermineCommandType(normalizedInput);
        var commandArgs = normalizedInput.Split(" ");

        switch (commandType)
        {
            case CommandType.Exit:
                HandleExit();
                break;
            case CommandType.Take:
                HandleTakeCommand(commandArgs);
                break;
            case CommandType.Drop:
                HandleDropCommand(commandArgs);
                break;
            case CommandType.Move:
                HandleMoveCommand(normalizedInput);
                break;
            case CommandType.Use:
                HandleUseCommand(commandArgs);
                break;
            case CommandType.Examine:
                world.DescribeCurrentLocation(forceFullDescription: true);
                break;
            case CommandType.ExamineItem:
                HandleExamineItemCommand(commandArgs);
                break;
            case CommandType.Inventory:
                inv.ListItems();
                break;
            default:
                TypeWriteLine("I'm sorry, I didn't understand that.");
                break;
        }
    }

    private CommandType DetermineCommandType(string input)
    {
        if (ExitCommands.Any(input.Equals)) return CommandType.Exit;
        if (TakeCommands.Any(input.StartsWith)) return CommandType.Take;
        if (DropCommands.Any(input.StartsWith)) return CommandType.Drop;
        if (MoveCommands.Any(input.StartsWith) || direction.Contains(input)) return CommandType.Move;
        if (input.StartsWith("use")) return CommandType.Use;
        if (ExamineCommands.Any(input.Equals)) return CommandType.Examine;
        if (ExamineCommands.Any(input.StartsWith) && input.Split(" ").Count() > 1) return CommandType.ExamineItem;
        
        return InventoryCommands.Any(input.Equals) ? CommandType.Inventory : CommandType.Unknown;
    }

    private void HandleExit()
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }

    private void HandleTakeCommand(string[] args)
    {
        if (args.Length > 1)
            TakeItem(args[^1]);
        else
            TypeWriteLine("I don't know how to take that.");
    }

    private void HandleDropCommand(string[] args)
    {
        if (args.Length > 1)
            DropItem(args[^1]);
        else
            TypeWriteLine("I don't know how to drop that.");
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

    private void HandleUseCommand(string[] args)
    {
        var item = GetItem(args);
        if (item != null)
            UseItem(item);
        else
            TypeWriteLine("I found no item to use.");
    }

    private void HandleExamineItemCommand(string[] args)
    {
        var item = GetItem(args);
        if (item != null)
            TypeWriteLine(item.Description);
        else
            TypeWriteLine("I found no item to examine.");
    }

    /*
     * Spezielle Itemhandler
     *
     * Sind die eigentlich wirklich nötig?
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
            TypeWriteLine($"Failed to take the {itemName}.");
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
            TypeWriteLine($"Failed to take the {itemName}.");
            return;
        }

        if (inv.AddItem(item))
        {
            TypeWriteLine($"You pick up the {itemName}.");
            return;
        }

        // If inventory addition fails, put the item back in the location
        currentLocation.AddItem(item);
        TypeWriteLine($"You couldn't pick up the {itemName}.");
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

    private Item? GetItem(string[] userItemSplit)
    {
        var currentLocation = world.GetCurrentLocation();
        foreach(var possItem in userItemSplit)
        {
            var item = inv.GetItem(possItem);
            if(item != null) return item;
            
            if(currentLocation?.HasItem(possItem) == true)
                return currentLocation.GetItem(possItem);
        }
        return null;
    }

    private void UseItem(Item? item)
    {
        // world ist sonst null, wenn wir Item nicht voher an uns nehmen...
        item.SetGameState(world,inv);
        var locationHasItem = world.GetCurrentLocation()?.HasItem(item?.Name) ?? false;
        var hasItem = inv.GetItem(item);
        if (!hasItem && !locationHasItem)
        {
            TypeWriteLine($"You don't have a/the {item?.Name} and you can't find it here.");
            return;
        }

        TypeWriteLine(item?.Use());
    }
}