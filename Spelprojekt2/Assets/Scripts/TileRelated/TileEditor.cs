using System.Collections.Generic;
using UnityEngine;

public enum eTileType
{
    Empty,
    Rock,
    Hole,
    Laser,
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


public class TileEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject myTile = null;
    [SerializeField]
    private Material myFirstMat = null;
    [SerializeField]
    private Material mySecondMat = null;

    private int _myWidth;
    private int _myHeight;

    private List<GameObject> myTileContainer;
    private List<_Tile> myTiles;
    private Vector2 myMapSize;

    public int myHeight { get => _myHeight; set => _myHeight = value; }
    public int myWidth { get => _myWidth; set => _myWidth = value; }

    private void OnValidate()
    {
        if (FindObjectOfType<Tile>() != null)
        {
            myTileContainer = new List<GameObject>(FindObjectsOfType<Tile>().Length);
            myTiles = new List<_Tile>();
            Tile[] temp = FindObjectsOfType<Tile>();

            for (int i = 0; i < temp.Length; i++)
            {
                myTileContainer.Add(temp[i].gameObject);
            }
        }
        else
        {
            myTileContainer = new List<GameObject>(_myHeight * _myWidth);
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < myTileContainer.Count - 1; i++)
        {
            Destroy(myTileContainer[i].gameObject);
        }
    }
    public void GenerateTiles()
    {
        ClearTiles();
        for (int i = 0; i < _myHeight; i++)
        {
            for (int j = 0; j < _myWidth; j++)
            {
                if (i % 2 != 0 && j % 2 != 0)
                {
                    myTile.gameObject.GetComponent<Renderer>().material = myFirstMat;
                }
                else if (i % 2 == 0 && j % 2 != 0)
                {
                    myTile.gameObject.GetComponent<Renderer>().material = mySecondMat;
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    myTile.gameObject.GetComponent<Renderer>().material = myFirstMat;
                }
                else
                {
                    myTile.gameObject.GetComponent<Renderer>().material = mySecondMat;
                }
                Vector3 pos = new Vector3(i, 0, j);
                myTiles.Add(new _Tile(new Coord(i, j), myTile.GetComponent<Tile>().GetTileType()));
                myTileContainer.Add(Instantiate(myTile, pos, transform.rotation, transform));
            }
        }
    }

    public void ClearTiles()
    {
        foreach (GameObject tile in myTileContainer)
        {
            DestroyImmediate(tile.gameObject);
            myTiles.Clear();
        }
    }

    public void SetWidth(float aWidth)
    {
        myMapSize.x = aWidth;
    }

    public void SetHeight(float aHeight)
    {
        myMapSize.y = aHeight;
    }
}
