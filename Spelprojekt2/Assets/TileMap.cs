using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    // TODO: find tiletype at all times it is changed

    const int myRows = 10;
    const int myColumns = 7;

    private _Tile[,] myTileMap = new _Tile[myRows, myColumns];

    Tile[] myTiles;

    private void Start()
    {
        myTiles = FindObjectsOfType<Tile>();
        int amount = myTiles.Length;

        // Sort tiles after how they were added to the scene
        for (int i = 0; i < amount; ++i)
        {
            for (int j = i + 1; j < amount; ++j)
            {
                Tile temp = myTiles[j];
                myTiles[j] = myTiles[i];
                myTiles[i] = temp;
            }
        }

        // Sort tiles after (row - column) instead of (column - tile)
        for (int i = 0; i < amount; ++i)
        {
            for (int j = i + 1; j < amount; ++j)
            {
                if (myTiles[j].transform.position.x < myTiles[i].transform.position.x || myTiles[j].transform.position.z < myTiles[i].transform.position.z)
                {
                    Tile temp = myTiles[i];
                    myTiles[i] = myTiles[j];
                    myTiles[j] = temp;
                }
            }
        }

        ReDefineMap();
    }
    private void Update()  // Change to event driven
    {
        ReDefineMap();
    }
    private void LateUpdate()
    {
        PrintTileInfo(0, 0);
        //PrintTileInfo(1, 0);
        //PrintTileInfo(0, 1);
        //PrintTileInfo(1, 1);
    }

    void ReDefineMap()
    {
        int counter = 0;

        for (int row = 0; row < myRows; ++row)
        {
            for (int column = 0; column < myColumns; ++column)
            {
                myTileMap[row, column] = myTiles[counter++].GetTile();
            }
        }
    }

    void PrintTileInfo(int aRow, int aColumn)
    {
        string tileName = "Empty";

        if (myTileMap[aRow, aColumn].type == eTileType.Rock)
            tileName = "Rock";

        if (myTileMap[aRow, aColumn].type == eTileType.Impassable)       
            tileName = "Impassable";

        if (myTileMap[aRow, aColumn].type == eTileType.Sliding)
            tileName = "Sliding";

        if (myTileMap[aRow, aColumn].type == eTileType.Hole)
            tileName = "Hole";

        if (myTileMap[aRow, aColumn].type == eTileType.Finish)
            tileName = "Finish";

        if (myTileMap[aRow, aColumn].type == eTileType.Button)
            tileName = "Button";

        if (myTileMap[aRow, aColumn].type == eTileType.Door)
            tileName = "Door";

        if (myTileMap[aRow, aColumn].type == eTileType.Emitter)
            tileName = "Emitter";

        if (myTileMap[aRow, aColumn].type == eTileType.Reflector)
            tileName = "Reflector";

        if (myTileMap[aRow, aColumn].type == eTileType.Receiver)
            tileName = "Receiver";

        if (myTileMap[aRow, aColumn].type == eTileType.Player)
            tileName = "Player";

        int x = myTileMap[aRow, aColumn].coord.x;
        int z = myTileMap[aRow, aColumn].coord.y;

        Debug.Log("Type: " + tileName + " - Coord: (" + x + ", " + z + ")");
    }
}
