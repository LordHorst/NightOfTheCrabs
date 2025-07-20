namespace NightOfTheCrabs.World;

public enum KnowledgeType
{
    HavePipe,
    HaveTobacco,
    InformedMilitary
}

public class CharacterKnowledge
{
    private readonly HashSet<KnowledgeType> _discoveredKnowledge = [];

    public void Discover(KnowledgeType knowledge)
    {
        _discoveredKnowledge.Add(knowledge);
    }

    public bool HasDiscovered(KnowledgeType knowledge)
    {
        return _discoveredKnowledge.Contains(knowledge);
    }

    public void Lost(KnowledgeType knowledge)
    {
        _discoveredKnowledge.Remove(knowledge);
    }
}