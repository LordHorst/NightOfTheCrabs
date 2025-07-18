using static JoeysSunglasses.World.World;
using static JoeysSunglasses.Output;
using JoeysSunglasses.Inventory;

namespace JoeysSunglasses.Inventory.Items.House.Kitchen;

public class Cookie : Item
{
    public Cookie() 
        : base("Cookie", "A delicious chocolate chip cookie.")
    {
        CanBeUsed = true;
    }

    public override string Use(LocationType currentLocation)
    {
        //RemoveItem(this);
        return TypeWriteLine("You eat the cookie. Yum!");
    }
}