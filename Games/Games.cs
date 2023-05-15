using Gamernoid.Engine.Drivers;
using Gamernoid.Games;

namespace Gamernoid;

public class MainClass
{
    public static void Main(string[] args)
    {
        var snakeGraphics = new ConsoleSnakeGraphics();

        var controller = new ConsoleController(snakeGraphics);
        
        var snakeGame = new SnakeGame<ConsoleSprite>(controller);
        
        var gn = new Core<ConsoleController, ConsoleSprite>(controller, snakeGame);

        gn.Run();
    }    
}
