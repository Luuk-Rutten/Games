using System.ComponentModel;
using System.Net.Mail;
using Gamernoid.Engine.Drivers;
using Gamernoid.Games;

namespace Gamernoid;

public class Control
{
 /* Instanties van deze klasse geven controle over het verloop van het spel. */

    public bool isRunning = true;
    public int speed = 100;
}

public interface Sprite
{
 /* Een sprite stelt een tekening of een plaatje (van een spel) voor.

    Deze interface is leeg (dus: dwingt geen methodes af die geimplementeerd dienen te worden).
  */
}

abstract public class Graphics<TSprite> where TSprite : Sprite
{
 /* Deze abstracte klasse representeert "alles met graphics".

    Implementaties van deze klasse zijn verantwoordelijk voor "apparaat-specifieke"
    (of: grafische techniek-specifieke) functionaliteit.
 */

    private Dictionary<int, TSprite> sprites = new Dictionary<int, TSprite>();

    protected TSprite GetSprite(int id)
    {
        TSprite sprite;

        if (!sprites.TryGetValue(id, out sprite))
            throw new ControllerException($"onbekende sprite '{id}' opgevraagd!");

        return sprite;
    }

    protected void RegisterSprite(int id, TSprite sprite)
    {
        if (sprites.ContainsKey(id))
        {
            throw new ControllerException("sprites moeten uniek zijn!");
        }

        sprites.Add(id, sprite);
    }

    virtual public void InitHardware()
    {
    }

    abstract public void InitGame();

    abstract public void ClearScreen();

    abstract public void DrawSprite(int x, int y, int spriteId);
}

public class ControllerException : Exception
{
    public ControllerException(string message) : base(message)
    {
    }
}

public abstract class Controller<TSprite> where TSprite : Sprite
{
    private Control control;

    private Graphics<TSprite> graphics;
    private Input input;

    public Controller(Graphics<TSprite> graphics, Input input)
    {
        this.graphics = graphics;
        this.input = input;
    }

    public void AssignControl(Control control)
    {
        this.control = control;
    }

    public void stopRunning()
    {
        control.isRunning = false;
    }

    public void Init()
    {
        graphics.InitHardware();

        input.InitHardware();

        graphics.InitGame();
    }

    public void DrawSprite(int x, int y, int spriteId)
    {
        graphics.DrawSprite(x, y, spriteId);
    }

    public bool ButtonPressed()
    {
        return input.ButtonPressed();
    }

    public Input.Button GetButton()
    {
        return input.GetButton();
    }
}


public abstract class Input
{
    public enum Button
    {
        Up, Down, Left, Right, A, B, Quit, Unsupported
    }

    virtual public void InitHardware()
    {
    }

    abstract public bool ButtonPressed();

    abstract public Button GetButton();
}


public abstract class Game<TSprite> where TSprite : Sprite
{
    protected Controller<TSprite> Controller;

    public Game(Controller<TSprite> controller)
    {
        Controller = controller;
    }

    public abstract void Draw();

    public virtual void ActionUp()
    {
    }

    public virtual void ActionDown()
    {
    }

    public virtual void ActionLeft()
    {
    }

    public virtual void ActionRight()
    {
    }

    public virtual void ActionA()
    {
    }

    public virtual void ActionB()
    {
    }

    public abstract void Quit();
}

public class Core<TController, TSprite> where TSprite : Sprite
                                        where TController : Controller<TSprite>
{
    private Control control = new Control();

    private Game<TSprite> game;
    private TController controller;

    public Core(TController controller, Game<TSprite> game)
    {
        this.controller = controller;
        this.game = game;
    }

    public void Run()
    {
        controller.Init();

        controller.AssignControl(control);

        while (control.isRunning)
        {
            game.Draw();

            Thread.Sleep(control.speed);

            if (controller.ButtonPressed())
            {
                var button = controller.GetButton();

                switch (button)
                {
                    case Input.Button.Quit:
                        game.Quit();
                        break;

                    case Input.Button.Left:
                        game.ActionLeft();
                        break;

                    case Input.Button.Right:
                        game.ActionRight();
                        break;

                    case Input.Button.Up:
                        game.ActionUp();
                        break;

                    case Input.Button.Down:
                        game.ActionDown();
                        break;
                }
            }
        }
    }
}
