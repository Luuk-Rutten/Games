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
    public const int MUUR = 4;
    public const int VERONGELUKT = 5;
}

class SokobanGame<TSprite> : Game<TSprite> where TSprite : Sprite
{
    bool MagStapNemen = false;

    private Coordinaat speler;
    private Coordinaat steen;
    private Coordinaat kruis;

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
            Controller.DrawSprite(20,20, SokobanSprites.STEEN);
            steen.y = 20;
            steen.x = 20;

        }


    }

    private void maakkruis(int x, int y)

    {
        if (veld[x, y] == SokobanSprites.LEEGTE)
        {
            veld[x, y] = SokobanSprites.KRUIS;
            Controller.DrawSprite(x, y, SokobanSprites.KRUIS);
            //kruis.x = 60;
            //kruis.y = 20;
        }

    }


    public SokobanGame(Controller<TSprite> controller) : base(controller)
    {
        speler = new Coordinaat(10, 10);

        veld = new int[80, 24];
      
        kaart1();
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
         maakstenen();
         maakkruis(40, 16);
        Controller.DrawSprite(speler.x, speler.y, SokobanSprites.SPELER);

        if (MagStapNemen)
        {



            veld[speler.x, speler.y] = SokobanSprites.SPELER;


            int spelerposX = speler.x;
            int spelerposY = speler.y;
            int beweegX = 0;
            int beweegY = 0;   

            switch (richting)
            {
                case Richting.Noord:
                    speler.y--;
                    beweegY--; 
                    break;

                case Richting.Zuid:
                    speler.y++;
                    beweegY++;
                    break;

                case Richting.West:
                    speler.x--;
                    beweegX--;
                    break;

                case Richting.Oost:
                    speler.x++;
                    beweegX++;
                    break;
            }

            

            //zorgt ervoor dat speler niet buiten het veld raakt
            if (speler.x == 0 || speler.x == veld.GetLength(0) - 1 || speler.y == 0 || speler.y == veld.GetLength(1) - 1)
            {
                speler.x = spelerposX;
                speler.y = spelerposY;

            }
            else
            {
                Controller.DrawSprite(speler.x, speler.y, SokobanSprites.SPELER);
                Controller.DrawSprite(spelerposX, spelerposY, SokobanSprites.LEEGTE);


                MagStapNemen = false;
            }

            

            //zorgt ervoor dat ik steen naar rechts kan duwen
            if (speler.x == steen.x && speler.y == steen.y)
            {

                //check dat je steen niet buiten het veld duwt
                if (steen.x + beweegX == veld.GetLength(0) - 1 || steen.y + beweegY == veld.GetLength(1) - 1 || steen.x +beweegX == 0  || steen.y + beweegY == 0)


                {
                    speler.x = spelerposX;
                    speler.y = spelerposY;
                   // steen.x = steen.x + 1;
                    Controller.DrawSprite(steen.x , steen.y, SokobanSprites.STEEN);
                    Controller.DrawSprite(speler.x, speler.y, SokobanSprites.SPELER);


                }
                else
                {
                    Controller.DrawSprite(speler.x, speler.y, SokobanSprites.SPELER);
                    Controller.DrawSprite(speler.x + beweegX, speler.y + beweegY, SokobanSprites.STEEN);

                    steen.x = speler.x + beweegX;
                    steen.y = speler.y + beweegY;



                }
                //kan de steen naar het kruis verschuiven, winconditie
                if (steen.x == kruis.x && steen.y == kruis.y)
                {
                    Console.WriteLine("Gewonnen!");
                    Thread.Sleep(3000);
                    Quit();
                }

            }




            frame++;




        }

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
       // var verongelukt = new ConsoleSprite()

        RegisterSprite(SokobanSprites.LEEGTE, grond);
        RegisterSprite(SokobanSprites.SPELER, speler);
        RegisterSprite(SokobanSprites.MUUR, muur);
        RegisterSprite(SokobanSprites.KRUIS, kruis);
        RegisterSprite(SokobanSprites.STEEN, steen);
        //RegisterSprite(SokobanSprites.VERONGELUKT,verongelukt);


    }
}

