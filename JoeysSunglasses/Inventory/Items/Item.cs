using JoeysSunglasses.World;
using static JoeysSunglasses.World.World;

namespace JoeysSunglasses.Inventory.Items;

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanBePickedUp { get; set; } = true;
    public bool CanBeUsed { get; set; } = false;
    
    protected List<LocationType> AllowedLocations { get; set; }
    protected List<LocationType> DisallowedLocations { get; set; }


    public Item(string name, string description)
    {
        Name = name;
        Description = description;
        AllowedLocations = new List<LocationType>();
        DisallowedLocations = new List<LocationType>();

    }

    public virtual string Examine()
    {
        return Description;
    }

    public virtual string Use(LocationType currentLocation)
    {
        if (!CanBeUsed)
            return $"You can't use the {Name}.";
        
        if (DisallowedLocations.Contains(currentLocation))
            return $"You can't use the {Name} here.";

        if (AllowedLocations.Any() && !AllowedLocations.Contains(currentLocation))
            return $"You can't use the {Name} here.";

            
        return $"You use the {Name}.";
    }
    
    protected void SetAllowedLocations(params LocationType[] locations)
    {
        AllowedLocations.Clear();
        AllowedLocations.AddRange(locations);
    }

    protected void SetDisallowedLocations(params LocationType[] locations)
    {
        DisallowedLocations.Clear();
        DisallowedLocations.AddRange(locations);
    }

}