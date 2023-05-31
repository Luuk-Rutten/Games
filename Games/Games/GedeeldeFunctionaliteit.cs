namespace Gamernoid.Games;

enum Richting
{
    Noord = 1,
    Zuid = 2,
    Oost = 3,
    West = 4
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
