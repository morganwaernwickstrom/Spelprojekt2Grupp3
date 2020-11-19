using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    const int myColumns = 10;
    const int myRows = 7;

    //public _Tile[,] tileMap = new _Tile[myRows, myColumns];
    public Tile[,] tileMap = new Tile[myColumns, myRows];
    Tile[] allTiles;

    public int amount = 0;

    public int i = 0;
    public int j = 0;

    private void Start()
    {
        allTiles = FindObjectsOfType<Tile>();
        amount = allTiles.Length;

        for (i = 0; i < myColumns; ++i)
        {
            for (j = 0; j < myRows; ++j)
            {
                int index = amount - 1;
                //tileMap[j, i] = new _Tile(new Coord(i, j), allTiles[counter - 1].GetTileType());
                tileMap[i, j] = allTiles[index];
                amount -= 1;
            }
        }

        //StartCoroutine(CycleTrough());

        //float x = tileMap[1, 1].transform.position.x;
        //float z = tileMap[1, 1].transform.position.z;

        tileMap[0, 0].gameObject.SetActive(false);

        //Debug.Log("Last tile -  X: " + x + " Z: " + z);
    }

    IEnumerator CycleTrough()
    {
        // j = Column tile (x cord)
        // i = Row tile    (y/z cord)

        for (i = 0; i < myColumns; ++i)
        {
            for (j = 0; j < myRows; ++j)
            {
                int index = amount - 1;
                //tileMap[j, i] = new _Tile(new Coord(i, j), allTiles[counter - 1].GetTileType());
                tileMap[j, i] = allTiles[index];
                tileMap[j, i].gameObject.SetActive(false);
                amount -= 1;
                yield return new WaitForSeconds(0.0001f);
                
            }
        }
    }
}
