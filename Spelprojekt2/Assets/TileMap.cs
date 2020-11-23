using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    const int myColumns = 7;
    const int myRows = 10;

    public _Tile[,] tileMap = new _Tile[myRows, myColumns];
    //public Tile[,] tileMap = new Tile[myColumns, myRows];

    private void Start()
    {
        Tile[] allTiles;
        allTiles = FindObjectsOfType<Tile>();
        int amount = allTiles.Length;

        // Sort tiles after how they were added to the scene
        for (int i = 0; i < amount; ++i)
        {
            for (int j = i + 1; j < amount; ++j)
            {

                Tile temp = allTiles[j];
                allTiles[j] = allTiles[i];
                allTiles[i] = temp;

            }
        }

        // Sort tiles after (row - column) instead of (column - tile)
        for (int i = 0; i < amount; ++i)
        {
            for (int j = i + 1; j < amount; ++j)
            {
                if (allTiles[j].transform.position.x < allTiles[i].transform.position.x || allTiles[j].transform.position.z < allTiles[i].transform.position.z)
                {
                    Tile temp = allTiles[i];
                    allTiles[i] = allTiles[j];
                    allTiles[j] = temp;
                }
            }
        }

        int counter = 0;

        for (int column = 0; column < myColumns; ++column)
        {
            for (int row = 0; row < myRows; ++row)
            {
                tileMap[column, row] = allTiles[counter++];
            }
        }
    }
}
