namespace Gamernoid.Games.Sokoban;

using Gamernoid.Engine.Drivers;

class SokobanSprites
{
    public const int LEEGTE = 0;
    public const int SPELER = 1;
}

class SokobanGame<TSprite> : Game<TSprite> where TSprite : Sprite
{
    public SokobanGame(Controller<TSprite> controller) : base(controller)
    {
    }

    public override void Draw()
    {
    }

    public override void Quit()
    {
    }
}

class ConsoleSokobanGraphics : ConsoleGraphics
{
    public override void InitGame()
    {
        var grond = new ConsoleSprite(' ');
        var speler = new ConsoleSprite('X', ConsoleColor.Magenta);

        RegisterSprite(SokobanSprites.LEEGTE, grond);
        RegisterSprite(SokobanSprites.SPELER, speler);
    }
}
