using Gamernoid.Engine.Drivers;

namespace Gamernoid.Games;

class SnakeSprites
{
    public const int LEEGTE = 0;
    public const int SLANG_KOP = 1;
    public const int SLANG_SEGMENT = 2;
    public const int MUUR = 3;
    public const int PADDO = 4;
    public const int UITGANG = 5;
    public const int VERONGELUKT = 6;
}


struct Coordinaat
{
    public int x, y;

    public Coordinaat(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

enum Richting
{
    Noord = 1,
    Zuid  = 2,
    Oost  = 3,
    West  = 4
}

class SnakeGame<TSprite> : Game<TSprite> where TSprite : Sprite
{
    private Coordinaat kop;
    private List<Coordinaat> lijf;

    private int lengte = 25;
    private Richting richting = Richting.Oost;

    private int[,] veld;

    int frame = 0;

    private void veranderVeld(Coordinaat doel, int waarde)
    {
        veld[doel.x, doel.y] = waarde;
    }
    
    private void kaart1()
    {
        var breedte = veld.GetLength(0);
        var hoogte = veld.GetLength(1);
        
        for (int x = 0; x < breedte; x++)
        {
            veld[x, 0] = veld[x, hoogte - 1] = SnakeSprites.MUUR;
            
            for (int y = 1; y < hoogte - 1; y++)
            {
                veld[x, y] = SnakeSprites.LEEGTE;
            }
        }
        
        for (int y = 0; y < hoogte; y++)
        {
            veld[0, y] = veld[breedte - 1, y] = SnakeSprites.MUUR;
        }

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
    
    public SnakeGame(Controller<TSprite> controller) : base(controller)
    {
        kop = new Coordinaat(10, 10);

        lijf = new List<Coordinaat>();

        veld = new int[80, 24];

        kaart1();
    }

    public void verongelukt()
    {
        var vorige = lijf[lijf.Count - 1];
        
        Controller.DrawSprite(kop.x, kop.y, SnakeSprites.VERONGELUKT);
        
        Controller.stopRunning();
    }
    
    public override void Draw()
    {
        if (frame == 0)
            tekenGeheleVeld();

        
        veld[kop.x, kop.y] = SnakeSprites.SLANG_SEGMENT;
        
        Controller.DrawSprite(kop.x, kop.y, SnakeSprites.SLANG_SEGMENT);

        lijf.Add(kop);

        if (lijf.Count > lengte)
        {
            Controller.DrawSprite(lijf[0].x, lijf[0].y, SnakeSprites.LEEGTE);

            veld[lijf[0].x, lijf[0].y] = SnakeSprites.LEEGTE;
            
            lijf.RemoveAt(0);
        }

        switch (richting)
        {
            case Richting.Noord:
                kop.y--;
                break;
            
            case Richting.Zuid:
                kop.y++;
                break;
            
            case Richting.West:
                kop.x--;
                break;
            
            case Richting.Oost:
                kop.x++;
                break;
        }
        
        if ((kop.x >= veld.GetLength(0)) || kop.x < 0)
            verongelukt();
        
        else if ((kop.y >= veld.GetLength(1)) || kop.y < 0)
            verongelukt();

        else if (veld[kop.x, kop.y] != SnakeSprites.LEEGTE)
            verongelukt();

        else
        {
            veld[kop.x, kop.y] = SnakeSprites.SLANG_KOP;

            Controller.DrawSprite(kop.x, kop.y, SnakeSprites.SLANG_KOP);
            
            frame++;
        }
    }

    public override void ActionUp() =>
        richting = richting == Richting.Zuid ? Richting.Zuid : Richting.Noord;

    public override void ActionDown() =>
        richting = richting == Richting.Noord ? Richting.Noord : Richting.Zuid;

    public override void ActionRight() =>
        richting = richting == Richting.West ? Richting.West : Richting.Oost;

    public override void ActionLeft() =>
        richting = richting == Richting.Oost ? Richting.Oost : Richting.West;

    public override void Quit()
    {
        Controller.stopRunning();
    }
}

class ConsoleSnakeGraphics : ConsoleGraphics
{
    public override void InitGame()
    {
        var grond = new ConsoleSprite
            (' ');
        
        var slangKop = new ConsoleSprite
            ('@', ConsoleColor.Yellow);

        var slangSegment = new ConsoleSprite
            ('@', ConsoleColor.DarkGreen);

        var muur = new ConsoleSprite
            ('#', ConsoleColor.Red, ConsoleColor.DarkRed);

        var paddo = new ConsoleSprite
            ('O', ConsoleColor.White);

        var uitgang = new ConsoleSprite
            ('[', ConsoleColor.Magenta, ConsoleColor.Blue);

        var verongelukt = new ConsoleSprite
            ('X', ConsoleColor.Red, ConsoleColor.DarkGreen);
        
        RegisterSprite(SnakeSprites.LEEGTE, grond);
        RegisterSprite(SnakeSprites.SLANG_KOP, slangKop);
        RegisterSprite(SnakeSprites.SLANG_SEGMENT, slangSegment);
        RegisterSprite(SnakeSprites.MUUR, muur);
        RegisterSprite(SnakeSprites.PADDO, paddo);
        RegisterSprite(SnakeSprites.UITGANG, uitgang);
        RegisterSprite(SnakeSprites.VERONGELUKT, verongelukt);
    }
}
