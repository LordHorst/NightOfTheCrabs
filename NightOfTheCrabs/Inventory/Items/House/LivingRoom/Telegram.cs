namespace NightOfTheCrabs.Inventory.Items.House.LivingRoom;

public class Telegram() : Item("Telegram",
    "The folded paper is creased from being handled too many times." +
    "\n“REGRET TO INFORM YOU IAN BELL AND JULIE MISSING STOP LAST SEEN NEAR BARMOUTH STOP SEARCH CONTINUES STOP”" +
    "\n\nBrief. Official. It offers nothing. But you see what they’re trying to hide in what they don’t say."
    , canBePickedUp: false
    , cantPickupMessage: "You have read the telegram multiple times. You see no need to carry it around.")
{ }