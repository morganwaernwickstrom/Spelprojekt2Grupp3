public enum eTileType
{
    Empty,
    Rock,
    Hole,
    Laser,
    Finish,
    Player
}

public struct Coord
{
    public int x;
    public int y;

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static bool operator ==(Coord c1, Coord c2)
    {
        return c1.x == c2.x && c1.y == c2.y;
    }

    public static bool operator !=(Coord c1, Coord c2)
    {
        return !(c1 == c2);
    }

    public static Coord operator +(Coord c1, Coord c2)
    {
        return new Coord(c1.x + c2.x, c1.y + c2.y);
    }
}

public struct _Tile
{
    public Coord coord;
    public eTileType type;

    public _Tile(Coord _coord, eTileType _type)
    {
        coord = _coord;
        type = _type;
    }
}
