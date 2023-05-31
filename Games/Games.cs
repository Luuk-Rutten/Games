using Gamernoid.Engine.Drivers;

using Gamernoid.Games.Arkanoid;
using Gamernoid.Games.BoulderDash;
using Gamernoid.Games.Pacman;
using Gamernoid.Games.Snake;
using Gamernoid.Games.Sokoban;

namespace Gamernoid;

public class MainClass
{
        public static void Arkanoid()
    {
        var arkanoidGraphics = new ConsoleArkanoidGraphics();

        var input = new ConsoleInput();

        var controller = new ConsoleController(arkanoidGraphics, input);

        var arkanoidGame = new ArkanoidGame<ConsoleSprite>(controller);

        var gn = new Core<ConsoleController, ConsoleSprite>(controller, arkanoidGame);

        gn.Run();

        Thread.Sleep(3000);

        arkanoidGraphics.ClearScreen();
    }


    public static void BoulderDash()
    {
        var boulderDashGraphics = new ConsoleBoulderDashGraphics();

        var input = new ConsoleInput();

        var controller = new ConsoleController(boulderDashGraphics, input);

        var boulderDashGame = new ArkanoidGame<ConsoleSprite>(controller);

        var gn = new Core<ConsoleController, ConsoleSprite>(controller, boulderDashGame);

        gn.Run();

        Thread.Sleep(3000);

        boulderDashGraphics.ClearScreen();
    }


    public static void Pacman()
    {
        var pacmanGraphics = new ConsolePacmanGraphics();

        var input = new ConsoleInput();

        var controller = new ConsoleController(pacmanGraphics, input);

        var pacmanGame = new PacmanGame<ConsoleSprite>(controller);

        var gn = new Core<ConsoleController, ConsoleSprite>(controller, pacmanGame);

        gn.Run();

        Thread.Sleep(3000);

        pacmanGraphics.ClearScreen();
    }


    public static void Snake()
    {
        var snakeGraphics = new ConsoleSnakeGraphics();

        var input = new ConsoleInput();

        var controller = new ConsoleController(snakeGraphics, input);

        var snakeGame = new SnakeGame<ConsoleSprite>(controller);

        var gn = new Core<ConsoleController, ConsoleSprite>(controller, snakeGame);

        gn.Run();

        Thread.Sleep(3000);

        snakeGraphics.ClearScreen();        
    }

    public static void Sokoban()
    {
        var sokobanGraphics = new ConsoleSokobanGraphics();

        var input = new ConsoleInput();

        var controller = new ConsoleController(sokobanGraphics, input);

        var sokobanGame = new SokobanGame<ConsoleSprite>(controller);

        var gn = new Core<ConsoleController, ConsoleSprite>(controller, sokobanGame);

        gn.Run();

        Thread.Sleep(3000);

        sokobanGraphics.ClearScreen();
    }


    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Geef de naam van het spel op via het 1e argument");

            return;
        }

        switch (args[0].ToLower())
        {
            case "arkanoid":
                Arkanoid(); break;

            case "boulderdash":
                BoulderDash(); break;

            case "pacman":
                Pacman(); break;

            case "snake":
                Snake(); break;

            case "sokoban":
                Sokoban(); break;

            default:
                Console.WriteLine("onbekend spel");
                break;
        }            
    }    
}
