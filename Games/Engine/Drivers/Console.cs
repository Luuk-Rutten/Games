namespace Gamernoid.Engine.Drivers;
public class ConsoleSprite : Sprite
{
    public readonly char Character;
    public readonly ConsoleColor? Foreground;
    public readonly ConsoleColor? Background;

    public ConsoleSprite(char character,
                         ConsoleColor foreground,
                         ConsoleColor background)
    {
        Character = character;
        Foreground = foreground;
        Background = background;
    }

    public ConsoleSprite(char character, ConsoleColor foreground)
    {
        Character = character;
        Foreground = foreground;
    }

    public ConsoleSprite(char character)
    {
        Character = character;
    }

}

public class ConsoleController : Controller<ConsoleSprite>
{
    public ConsoleController(Graphics<ConsoleSprite> graphics, Input input)
        : base(graphics, input)
    {
    }
}


class ConsoleInput : Input
{
    public override bool ButtonPressed()
    {
        return Console.KeyAvailable;
    }

    public override Input.Button GetButton()
    {
        var consoleKey = Console.ReadKey(true);

        switch (consoleKey.Key)
        {
            case ConsoleKey.Q:
                return Button.Quit;

            case ConsoleKey.LeftArrow:
                return Button.Left;

            case ConsoleKey.RightArrow:
                return Button.Right;

            case ConsoleKey.UpArrow:
                return Button.Up;

            case ConsoleKey.DownArrow:
                return Button.Down;
        }

        return Button.Unsupported;
    }
}


abstract class ConsoleGraphics : Graphics<ConsoleSprite>
{
    public sealed override void InitHardware()
    {
        ClearScreen();
    }


    public override void ClearScreen()
    {
        Console.ResetColor();

        Console.Clear();

        Console.CursorVisible = false;
    }


    public override void DrawSprite(int x, int y, int spriteId)
    {
        var sprite = GetSprite(spriteId);

        Console.SetCursorPosition(x, y);

        Console.ResetColor();

        if (sprite.Background != null)
            Console.BackgroundColor = (ConsoleColor) sprite.Background;

        if (sprite.Foreground != null)
            Console.ForegroundColor = (ConsoleColor) sprite.Foreground;

        Console.Write(sprite.Character);
    }
}
