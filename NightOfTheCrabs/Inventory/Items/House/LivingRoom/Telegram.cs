using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items.House.LivingRoom;

public class Telegram() : Item("Telegram",
    "The folded paper is creased from being handled too many times." +
    "\n“REGRET TO INFORM YOU IAN AND JULIE MISSING STOP LAST SEEN NEAR MOCHRAS STOP SEARCH CONTINUES STOP”" +
    "\n\nBrief. Official. It offers nothing. But you see what they’re trying to hide in what they don’t say."
    , canBePickedUp: false
    , cantPickupMessage: "You have read the telegram multiple times. You see no need to carry it around."
    , associatedKnowledge: KnowledgeType.ReadTelegram)
{
    public override string GetLocationDescription(World.World.LocationType locationType)
    {
        return World?.GetCharacterKnowledge().HasDiscovered(KnowledgeType.ReadTelegram) == true
            ? string.Empty
            : Name + " - sent by the police force of Gwynedd County";
    }
    public override string Examine()
    {
        if (World != null && !World.GetCharacterKnowledge().HasDiscovered(KnowledgeType.ReadTelegram))
        {
            World.GetCharacterKnowledge().Discover(KnowledgeType.ReadTelegram);
            TypeWriteLine(Description);
        }
        
        return string.Empty;
    }
}