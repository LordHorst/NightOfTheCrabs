using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs;

using Inventory_Inventory = Inventory.Inventory;

public class MainGameClass
{
    private readonly World.World _world;
    private readonly CommandHandler _commandHandler;

    private string[] _direction = new[]
    {
        "north", "n",
        "south", "s",
        "east", "e",
        "west", "w",
        "up", "u",
        "down", "d"
    };

    public MainGameClass()
    {
        _world = new World.World();
        var inv = new Inventory_Inventory(_world);
        _commandHandler = new CommandHandler(_world, inv, _direction);
    }

    private bool _gameBeat = false;

    public void StartGame()
    {
#if !DEBUG
        Story.TitleSequence();
        Console.ReadLine();
        Story.IntroText();
        Console.ReadLine();
        Story.PostIntro();
        Console.ReadLine();
#endif
        // Force full description of starting location
        _world.DescribeCurrentLocation(forceFullDescription: true);
        TypeWriteLine("What do you do?");
        while (true)
        {
            TypeWrite("? ");

            var userAction = Console.ReadLine();
            if (userAction == null)
            {
                TypeWriteLine("Something went wrong here, I'm sorry!");
                break;
            }
            _commandHandler.HandleCommand(userAction);
        }

        if (_gameBeat)
            TypeWriteLine("You beat the game!");
        else
            TypeWriteLine("You lost.");
    }
}