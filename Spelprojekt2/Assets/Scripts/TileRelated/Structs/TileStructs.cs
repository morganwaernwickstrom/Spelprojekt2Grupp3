// All structs for tiles
public enum eTileType
{
    Null,
    Empty,
    Rock,
    Hole,
    Laser,
    Finish,
    Rail,
    Train,
    Button,
    Door,
    Emitter,
    Reflector,
    Receiver,
    Impassable,
    Sliding,
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
        return (c1.x != c2.x || c1.y != c2.y);
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
    public bool myIsAssigned;

    public _Tile(Coord _coord, eTileType _type, bool isActive = false)
    {
        coord = _coord;
        type = _type;
        myIsAssigned = isActive;
    }

    public static bool operator !(_Tile t1)
    {
        if (t1.type == eTileType.Null)
        {
            return true;
        }

        return false;
    }
}
