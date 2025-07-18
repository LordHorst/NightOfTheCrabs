using JoeysSunglasses.Inventory.Items;

namespace JoeysSunglasses;

public static class Output
{
    internal static void TypeWrite(string? text = null)
    {
        if (string.IsNullOrEmpty(text))
        {
            Console.Write("");
            return;
        }
#if DEBUG
        Console.Write(text);
#else
        foreach (char c in text)
        {
            Console.Write(c)
            Thread.Sleep(30); // 30ms delay between each character
        }
#endif

        Thread.Sleep(10); // 1 second delay after each line
    }

    internal static string TypeWriteLine(string? text = null)
    {
        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine();
            return "";
        }
#if DEBUG
        Console.Write(text);
#else
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(30); // 30ms delay between each character
        }
#endif

        Console.WriteLine();
        Thread.Sleep(10); // 1 second delay after each line
        return "";
    }

    internal static void InventoryItems(List<Item?> items)
    {
        Console.WriteLine();
        if (!items.Any())
        {
            Console.WriteLine("Your inventory is empty.");
            return;
        }

        Console.WriteLine("In your inventory you have:");
        foreach (var item in items)
        {
            Console.WriteLine($"- {item?.Name}");
        }

        Console.WriteLine();
    }
}