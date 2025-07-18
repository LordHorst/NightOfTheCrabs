using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs.Inventory.Items.House.Kitchen;

public class Knife : Item
{
    public Knife() 
        : base("Knife", "A sharp kitchen knife. Be careful with it!")
    {
        CanBeUsed = true;
    }

    public override string Use()
    {
        if (!Init())
            return "";

        TypeWriteLine("You probably shouldn't wave that knife around.");
        return "";
    }
}