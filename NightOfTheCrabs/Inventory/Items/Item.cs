using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs.Inventory.Items;

public class Item(string name, string description, bool canBePickedUp = true, KnowledgeType? associatedKnowledge = null)
{
    public string Name { get; set; } = name;
    public string Description { get; protected set; } = description;
    public bool CanBePickedUp { get; protected init; } = canBePickedUp;
    protected bool CanBeUsed { get; init; }
    private List<World.World.LocationType> AllowedLocations { get; } = [];
    protected List<World.World.LocationType> DisallowedLocations { get; } = [];
    protected bool RemoveAfterUse { get; init; }
    protected Inventory? Inventory;
    protected World.World? World;
    protected KnowledgeType? AssociatedKnowledge { get; init; } = associatedKnowledge;

    protected bool Init()
    {
        if (World == null)
        {
            TypeWriteLine("Error: World not properly initialized");
            return false;
        }

        if (Inventory == null)
        {
            TypeWriteLine("Error: Inventory not properly initialized");
            return false;
        }

        return true;
    }
    
    public void SetGameState(World.World world, Inventory inventory)
    {
        if (world != null && World != world)
            World = world;
        if(inventory != null && Inventory != inventory)
            Inventory = inventory;
    }

    protected bool RemoveFromInventory()
    {
        return Inventory != null && Inventory.RemoveItem(Name);
    }

    public virtual string Examine()
    {
        return Description;
    }

    public virtual string Use()
    {
        if (World == null)
            return "Error: Item not properly initialized";
        
        var currentLocation = World.GetCurrentLocation();
        if (currentLocation != null)
        {
            var locationType = currentLocation.LocationType;

            if (!CanBeUsed)
                return $"You can't use the {Name}.";
        
            if (DisallowedLocations.Contains(locationType))
                return $"You can't use the {Name} here.";

            if (AllowedLocations.Any() && !AllowedLocations.Contains(locationType))
                return $"You can't use the {Name} here.";
        }

        string result = $"You use the {Name}.";
        
        if (RemoveAfterUse)
        {
            RemoveFromInventory();
        }
            
        return result;
    }
    
    protected void SetAllowedLocations(params World.World.LocationType[] locations)
    {
        AllowedLocations.Clear();
        AllowedLocations.AddRange(locations);
    }

    protected void SetDisallowedLocations(params World.World.LocationType[] locations)
    {
        DisallowedLocations.Clear();
        DisallowedLocations.AddRange(locations);
    }
    public virtual string GetLocationDescription(World.World.LocationType locationType)
    {
        // Default implementation just returns the item name
        return Name;
    }
    public virtual string GetCantPickUpReason()
    {
        return CanBePickedUp ? string.Empty : $"The {Name} is too heavy to carry.";
    }
    public void OnPickUp()
    {
        if (AssociatedKnowledge.HasValue && World?.GetCharacterKnowledge() != null)
        {
            World.GetCharacterKnowledge().Discover(AssociatedKnowledge.Value);
        }
    }

    public void OnDrop()
    {
        if (AssociatedKnowledge.HasValue && World?.GetCharacterKnowledge() != null)
        {
            World.GetCharacterKnowledge().Lost(AssociatedKnowledge.Value);
        }
    }
}