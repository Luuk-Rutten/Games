using Gamernoid.Engine.Drivers;
using Gamernoid.Games;

namespace Gamernoid;

public class MainClass
{
    public static void Snake()
    {
        var snakeGraphics = new ConsoleSnakeGraphics();

        var snakeInput = new ConsoleInput();

        var controller = new ConsoleController(snakeGraphics, snakeInput);

        var snakeGame = new SnakeGame<ConsoleSprite>(controller);

        var gn = new Core<ConsoleController, ConsoleSprite>(controller, snakeGame);

        gn.Run();

        Thread.Sleep(3000);

        snakeGraphics.ClearScreen();        
    }

    public static void Main(string[] args)
    {
        Snake();
    }    
}
