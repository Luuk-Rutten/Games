namespace Gamernoid.Games.Arkanoid;

using Gamernoid.Engine.Drivers;

class ArkanoidSprites
{
    public const int LEEGTE = 0;
    public const int SPELER = 1;
}

class ArkanoidGame<TSprite> : Game<TSprite> where TSprite : Sprite
{
    public ArkanoidGame(Controller<TSprite> controller) : base(controller)
    {
    }

    public override void Draw()
    {
    }

    public override void Quit()
    {
    }
}

class ConsoleArkanoidGraphics : ConsoleGraphics
{
    public override void InitGame()
    {
        var grond = new ConsoleSprite(' ');
        var speler = new ConsoleSprite('X', ConsoleColor.Magenta);

        RegisterSprite(ArkanoidSprites.LEEGTE, grond);
        RegisterSprite(ArkanoidSprites.SPELER, speler);
    }
}


