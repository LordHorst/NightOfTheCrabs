using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.LivingRoom.Bookshelf;

public class CrabBook : Item
{
    public CrabBook() : base("Crustacea: An Anatomical and Behavioral Study - Dr. H. Malden, 1946",
        @"The standard work in the field of crustacean life. Of course you have read it before — decades ago as a young researcher.
But something compelled you to pull it down again now. The pages smell of dust and salt, the kind of mustiness only time and damp libraries can conjure.
Ink diagrams of crab morphology cover entire chapters: carapace variations, claw structure, mating rituals, molting phases.

One chapter, toward the end, is dog-eared. You must have marked it years ago.

> Chapter XVII – Unclassified Specimens and Mythological Reports

It’s a curious inclusion for a scientific text. It speaks of accounts from fishermen in the Orkneys, the Hebrides, even West Africa—tales
of crabs larger than boats, claws strong enough to shear wood. Most marine biologists dismissed such reports as folklore. Dr. Malden did not.

You read:

""It is not beyond reason to suggest that certain abyssal species, rarely seen and poorly understood, may reach sizes far exceeding known specimens.
Were such creatures to ascend to shallower waters… their interaction with coastal ecosystems would be unpredictable and possibly catastrophic.""

There’s a pencil margin note in your own hand beside that paragraph:

""Absurd? Or simply premature?""",
        alternateNames: ["book", "crabbook", "crabs book"],
        canBePickedUp: false)
    {
        CanBeUsed = true;
        SetDisallowedLocations(NightOfTheCrabs.World.World.LocationType.Underwater);
    }

    public override string GetLocationDescription(World.World.LocationType locationType)
    {
        if (World != null && World.GetCharacterKnowledge().HasDiscovered(KnowledgeType.ExaminedBookshelf))
        {
            CanBePickedUp = true;
        }
        
        // Only show the book if the bookshelf has been examined
        return World?.GetCharacterKnowledge().HasDiscovered(KnowledgeType.ExaminedBookshelf) == true
            ? "A book about crabs catches your eye among the scientific volumes"
            : string.Empty; // Return empty string to hide the item from the location description
    }
    
    public override string GetCantPickUpReason()
    {
        if (World?.GetCharacterKnowledge().HasDiscovered(KnowledgeType.ExaminedBookshelf) == true)
        {
            CanBePickedUp = true; // Allow picking up once bookshelf is examined
            return string.Empty;
        }

        return "There are many books in this room. Be more specific.";
    }

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
}