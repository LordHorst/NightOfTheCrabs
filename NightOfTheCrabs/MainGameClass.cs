using NightOfTheCrabs.World;
using static NightOfTheCrabs.Output;
namespace NightOfTheCrabs;
using Inv = Inventory.Inventory;

public class MainGameClass
{
    private readonly World.World _world;
    private readonly CommandHandler _commandHandler;
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly Inv _inv;
    private readonly bool _gameBeat;
    private readonly string[] _direction =
    [
        "north", "n",
        "south", "s",
        "east", "e",
        "west", "w",
        "up", "u",
        "down", "d"
    ];

    public MainGameClass(bool gameBeat)
    {
        _gameBeat = gameBeat;
        _world = new World.World();
        _inv = new Inv(_world);
        _commandHandler = new CommandHandler(_world, _inv, _direction);
    }

    

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
        while (true && !_gameBeat)
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

        TypeWriteLine(_gameBeat ? "You beat the game!" : "You lost.");
    }
}