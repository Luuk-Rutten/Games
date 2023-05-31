namespace Gamernoid.Games.Pacman;

using Gamernoid.Engine.Drivers;

class PacmanSprites
{
    public const int LEEGTE = 0;
    public const int SPELER = 1;
    public const int PLATGESLAGEN = 2;
}


class PacmanGame<TSprite> : Game<TSprite> where TSprite : Sprite
{
    int frame = 0;

    Coordinaat speler;

    Richting richting = Richting.Noord;

    public PacmanGame(Controller<TSprite> controller) : base(controller)
    {
        speler = new Coordinaat(40, 12);
    }

    public override void Draw()
    {
        Controller.DrawSprite(speler.x, speler.y, PacmanSprites.LEEGTE);

        var oudePositie = speler; /* let op: dit doet een 'value copy'!
                                     ..want 'Coordinaat' is een 'struct'! */
        switch (richting)
        {
            case Richting.Noord:
                speler.y--; break;

            case Richting.Zuid:
                speler.y++; break;

            case Richting.West:
                speler.x--; break;

            case Richting.Oost:
                speler.x++; break;


        }

        if (speler.x > 79 || speler.x < 0 ||
            speler.y > 24 || speler.y < 0)

            speler = oudePositie;

        Controller.DrawSprite(speler.x, speler.y, PacmanSprites.SPELER);
        
        frame++;
    }

    public override void ActionUp()
    {
        richting = Richting.Noord;
    }

    public override void ActionDown()
    {
        richting = Richting.Zuid;
    }

    public override void ActionRight()
    {
        richting = Richting.Oost;
    }

    public override void ActionLeft()
    {
        richting = Richting.West;
    }

    public override void Quit()
    {
        Controller.DrawSprite(speler.x, speler.y, PacmanSprites.PLATGESLAGEN);

        Controller.stopRunning();
    }
}

class ConsolePacmanGraphics : ConsoleGraphics
{
    public override void InitGame()
    {
        var grond = new ConsoleSprite(' ');
        var speler = new ConsoleSprite('O', ConsoleColor.Yellow);
        var platgeslagen = new ConsoleSprite('_', ConsoleColor.Red);

        RegisterSprite(PacmanSprites.LEEGTE, grond);
        RegisterSprite(PacmanSprites.SPELER, speler);
        RegisterSprite(PacmanSprites.PLATGESLAGEN, platgeslagen);
    }
}
