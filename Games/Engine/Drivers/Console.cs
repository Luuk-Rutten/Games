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
    public ConsoleController(Graphics<ConsoleSprite> graphics) : base(graphics)
    {
    }
}

abstract class ConsoleGraphics : Graphics<ConsoleSprite>
{
    public override void ClearScreen()
    {
        Console.Clear();
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
