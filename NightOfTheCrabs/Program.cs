using NightOfTheCrabs;

try
{
    var notc = new MainGameClass(gameBeat: false);
    notc.StartGame();
}
catch (Exception e)
{
    Console.WriteLine("An error occurred while running the game.");
    Console.WriteLine(e);
    Console.ReadLine();
}