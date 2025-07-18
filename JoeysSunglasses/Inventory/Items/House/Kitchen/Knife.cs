using static JoeysSunglasses.Output;
using static JoeysSunglasses.World.World;
namespace JoeysSunglasses.Inventory.Items.House.Kitchen;

public class Knife : Item
{
    public Knife() 
        : base("Knife", "A sharp kitchen knife. Be careful with it!")
    {
        CanBeUsed = true;
    }

    public override string Use()
    {
        if (_world == null)
        {
            TypeWriteLine("Error: Item not properly initialized");
            return "";
        }

        TypeWriteLine("You probably shouldn't wave that knife around.");
        return "";
    }
}