namespace Gamernoid.Games.BoulderDash;

using Gamernoid.Engine.Drivers;

class BoulderDashSprites
{
    public const int LEEGTE = 0;
    public const int SPELER = 1;
}

class BoulderDashGame<TSprite> : Game<TSprite> where TSprite : Sprite
{
    public BoulderDashGame(Controller<TSprite> controller) : base(controller)
    {
    }

    public override void Draw()
    {
    }

    public override void Quit()
    {
    }
}

class ConsoleBoulderDashGraphics : ConsoleGraphics
{
    public override void InitGame()
    {
        var grond = new ConsoleSprite(' ');
        var speler = new ConsoleSprite('X', ConsoleColor.Magenta);

        RegisterSprite(BoulderDashSprites.LEEGTE, grond);
        RegisterSprite(BoulderDashSprites.SPELER, speler);
    }
}


