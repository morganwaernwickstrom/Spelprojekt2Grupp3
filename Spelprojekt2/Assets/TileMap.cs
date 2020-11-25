using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    // TODO: find tiletype at all times it is changed

    const int myColumns = 7; // x
    const int myRows = 10;   // y

    private _Tile[,] myTileMap = new _Tile[myColumns, myRows];
    Tile[] myTiles;

    public static TileMap Instance = null;

    private void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }

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

        SetAllTiles();
    }

    void SetAllTiles()
    {
        // MIGHT NEED REFACTORING
        RockMovement[] allRocks = FindObjectsOfType<RockMovement>();
        SlidingRockMovement[] allSlidingRocks = FindObjectsOfType<SlidingRockMovement>();
        Impassable[] allImpassables = FindObjectsOfType<Impassable>();
        HoleBlocking[] allHoles = FindObjectsOfType<HoleBlocking>();
        Door[] allDoors = FindObjectsOfType<Door>();
        FinishTrigger[] allGoals = FindObjectsOfType<FinishTrigger>();
        Button[] allButtons = FindObjectsOfType<Button>();
        LaserEmitterScript[] allEmitters = FindObjectsOfType<LaserEmitterScript>();
        ReflectorScript[] allReflectors = FindObjectsOfType<ReflectorScript>();
        ReceiverScript[] allReceivers = FindObjectsOfType<ReceiverScript>();
        Laser[] allLasers = FindObjectsOfType<Laser>();
        PlayerMovement[] allPlayer = FindObjectsOfType<PlayerMovement>();

        for (int row = 0; row < myRows; ++row)
        {
            for (int column = 0; column < myColumns; ++column)
            {
                foreach (var i in allRocks)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Rock;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allSlidingRocks)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Sliding;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allImpassables)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Impassable;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allHoles)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Hole;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allDoors)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Door;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allGoals)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Finish;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allButtons)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Button;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allEmitters)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Emitter;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allReflectors)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Reflector;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allReceivers)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Receiver;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allLasers)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Laser;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allPlayer)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Player;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }
            }
        }
    }

    public eTileType Get(Coord aCoord)
    {
        return myTileMap[aCoord.x, aCoord.y].type;
    }

    public int GetDistance(Coord aPosition, Coord aDirection, bool isLaser = true)
    {
        int distance = 0;
        bool hasFound = false;
        //List<eTileType> targets = new List<eTileType>();
        Coord current = aPosition;

        if (isLaser)
        {
            eTileType[] laserTargets = { eTileType.Emitter, eTileType.Door, eTileType.Impassable, eTileType.Player, eTileType.Receiver, eTileType.Reflector, eTileType.Rock, eTileType.Sliding };
            //targets.AddRange(laserTargets);

            // --- Used for the for-loop to work without going out of index bounds, is allways the right size based on direction --- //
            int maxRange = 0;

            if (aDirection.x != 0)
            {
                maxRange = 6;
            }
            else if (aDirection.y != 0)
            {
                maxRange = 9;
            }

            // --- Check for all tiles in the direction --- //
            for (int i = 0; i < maxRange; ++i)
            {
                current += aDirection;

                foreach (eTileType target in laserTargets)
                {
                    // --- If a tile that will stop the laser has been found, break the loop and continue to return distance --- //
                    if (myTileMap[current.x, current.y].type == target)
                    {
                        hasFound = true;
                        break;
                    }
                }

                // --- If a target has been found, then break from loop and return distance --- //
                if (hasFound)
                {
                    break;
                }
                else
                {
                    ++distance;
                }
            }
        }

        return distance;
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

        if (myTileMap[aRow, aColumn].type == eTileType.Laser)
            tileName = "Laser";

        int x = myTileMap[aRow, aColumn].coord.x;
        int z = myTileMap[aRow, aColumn].coord.y;

        Debug.Log("Type: " + tileName + " - Coord: (" + x + ", " + z + ")");
    }
}
