using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.LivingRoom.Bookshelf;

public class Bookshelf() : Item("Bookshelf",
    @"Your life's work, arranged alphabetically.
Marine anatomy, crustacean behavior, ancient sea legends from Cornwall and beyond. One volume sticks out slightly."
    , canBePickedUp: false
    , cantPickupMessage: "The bookshelf is too heavy to carry."
    , associatedKnowledge: KnowledgeType.ExaminedBookshelf)
{
    public override string Examine()
    {
        if (World != null && !World.GetCharacterKnowledge().HasDiscovered(KnowledgeType.ExaminedBookshelf))
        {
            World.GetCharacterKnowledge().Discover(KnowledgeType.ExaminedBookshelf);
            TypeWriteLine("You examine the bookshelf carefully. Among the various marine biology texts, " +
                          "a particular book about crabs catches your attention.");
        }
        else
        {
            TypeWriteLine("The bookshelf contains various scientific volumes. You've already noticed the book about crabs.");
        }
        
        return string.Empty;
    }
    public override string GetLocationDescription(World.World.LocationType locationType)
    {
        return "A large wooden bookshelf dominates one wall, filled with scientific volumes";
    }

}