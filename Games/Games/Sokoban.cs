namespace Gamernoid.Games.Sokoban;

using Gamernoid.Engine.Drivers;
using Gamernoid.Games.Pacman;
using Gamernoid.Games.Snake;

class SokobanSprites
{
    public const int LEEGTE = 0;
    public const int SPELER = 1;
    public const int KRUIS = 2;
    public const int STEEN = 3;
    public const int MUUR = 5;
}

class SokobanGame<TSprite> : Game<TSprite> where TSprite : Sprite
{
    bool MagStapNemen = false;

    private Coordinaat speler;

    private int[,] veld;
    private Richting richting = Richting.Oost;

    int frame = 0;


    private void veranderVeld(Coordinaat doel, int waarde)
    {
        veld[doel.x, doel.y] = waarde;
    }

    private void tekenGeheleVeld()
    {
        for (int x = 0; x < veld.GetLength(0); x++)
        {
            for (int y = 0; y < veld.GetLength(1); y++)
            {
                Controller.DrawSprite(x, y, veld[x, y]);
            }
        }
    }




    private void kaart1()
    {
        var breedte = veld.GetLength(0);
        var hoogte = veld.GetLength(1);

        for (int x = 0; x < breedte; x++)
        {
            veld[x, 0] = veld[x, hoogte - 1] = SokobanSprites.MUUR;

            for (int y = 1; y < hoogte - 1; y++)
            {
                veld[x, y] = SokobanSprites.LEEGTE;
            }
        }

        for (int y = 0; y < hoogte; y++)
        {
            veld[0, y] = veld[breedte - 1, y] = SokobanSprites.MUUR;
        }

    }

    private void maakstenen()
    {
        if (veld[20, 20] == SokobanSprites.LEEGTE)
        {
            veld[20, 20] = SokobanSprites.STEEN;
            Controller.DrawSprite(20, 20, SokobanSprites.STEEN);

        }


    }


    public SokobanGame(Controller<TSprite> controller) : base(controller)
    {
        speler = new Coordinaat(10, 10);

        veld = new int[80, 24];
      
        kaart1();
        maakstenen();

    }

    public override void ActionUp()  
    {
        MagStapNemen = true;
        richting = Richting.Noord;
    }

    public override void ActionDown()
    {
        MagStapNemen = true;

        richting = Richting.Zuid;
    }

    public override void ActionRight()
    {
        MagStapNemen = true;

        richting = Richting.Oost;
    }

    public override void ActionLeft()
    {
        MagStapNemen = true;

        richting = Richting.West;
    }

    public override void Quit()
    {
        Controller.stopRunning();
    }


    public override void Draw()  
    {

        if (frame == 0)
            tekenGeheleVeld();



        Controller.DrawSprite(speler.x, speler.y, SokobanSprites.SPELER);

        if (MagStapNemen)
        {



            veld[speler.x, speler.y] = SokobanSprites.SPELER;

            Controller.DrawSprite(speler.x, speler.y, SokobanSprites.LEEGTE);



            switch (richting)
            {
                case Richting.Noord:
                    speler.y--;
                    break;

                case Richting.Zuid:
                    speler.y++;
                    break;

                case Richting.West:
                    speler.x--;
                    break;

                case Richting.Oost:
                    speler.x++;
                    break;
            }

            Controller.DrawSprite(speler.x, speler.y, SokobanSprites.SPELER);
            MagStapNemen = false;
        }

        frame++;



    }

}

class ConsoleSokobanGraphics : ConsoleGraphics
{
    public override void InitGame()
    {
        var grond = new ConsoleSprite(' ');
        var speler = new ConsoleSprite('X', ConsoleColor.Magenta);
        var muur = new ConsoleSprite('#', ConsoleColor.Red, ConsoleColor.DarkRed);
        var kruis = new ConsoleSprite('X', ConsoleColor.DarkBlue);
        var steen = new ConsoleSprite('O', ConsoleColor.DarkBlue);


        RegisterSprite(SokobanSprites.LEEGTE, grond);
        RegisterSprite(SokobanSprites.SPELER, speler);
        RegisterSprite(SokobanSprites.MUUR, muur);
        RegisterSprite(SokobanSprites.KRUIS, kruis);
        RegisterSprite(SokobanSprites.STEEN, steen);


    }
}

