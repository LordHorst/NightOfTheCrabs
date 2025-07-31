using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs.Inventory.Items;

public class Item
{
    public string Name { get; }
    public string[]? AlternateNames { get; }
    public string Description { get; protected set; }
    public bool CanBePickedUp { get; protected set; }
    internal bool CanBeUsed { get; init; }
    protected List<World.World.LocationType> AllowedLocations { get; } = [];
    protected List<World.World.LocationType> DisallowedLocations { get; } = [];
    internal bool RemoveAfterUse { get; init; }
    protected World.World? World;
    private KnowledgeType? AssociatedKnowledge { get; }
    private string? CantPickupMessage { get; }

    protected Item(string name,
        string description,
        string[]? alternateNames = null,
        bool canBePickedUp = true,
        KnowledgeType? associatedKnowledge = null,
        string? cantPickupMessage = null)
    {
        Name = name;
        Description = description;
        AlternateNames = alternateNames;
        CanBePickedUp = canBePickedUp;
        AssociatedKnowledge = associatedKnowledge;
        CantPickupMessage = cantPickupMessage;
    }

    protected bool Init()
    {
        if (World?.GetInventory() == null)
        {
            TypeWriteLine("Error: Inventory not properly initialized");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Because of design error, we _need_ to set World to the world object, or else the whole game breaks.
    /// </summary>
    /// <param name="world"></param>
    public void SetWorld(World.World? world)
    {
        World = world;
    }

    protected bool RemoveFromInventory()
    {
        return World?.GetInventory() != null && World.GetInventory().RemoveItem(Name);
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

            if (DisallowedLocations.Contains(locationType) || AllowedLocations.Count > 0 && !AllowedLocations.Contains(locationType))
                return $"You can't use the {Name} here.";
        }

        var result = $"You use the {Name}.";

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
        return CanBePickedUp ? string.Empty : CantPickupMessage ?? $"The {Name} is too heavy to carry.";
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