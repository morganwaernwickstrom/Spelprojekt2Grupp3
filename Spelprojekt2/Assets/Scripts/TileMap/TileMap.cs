using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    const int myColumns = 7; // x
    const int myRows = 10;   // y
    private bool myHasUpdate = false;

    private _Tile[,] myTileMap = new _Tile[myColumns, myRows];
    Tile[] myTiles;

    public static TileMap Instance = null;

    private void Start()
    {
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
        InitializeTileMap();
    }

    private void OnEnable()
    {
        if (EventHandler.current != null)
        {
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
            EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
            EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
        }

        InitializeTileMap();
    }

    private void Update()
    {
        if (!myHasUpdate)
        {
            SetAllTiles();
            UpdateLaser();
            myHasUpdate = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            DebugTiles();
    }

    bool OnPlayerMove(Coord aPlayerPos, Coord aPreviousPos)
    {
        UpdateLaser();
        UpdateRail();

        if (!(Get(aPreviousPos) == eTileType.Rail))
        {
            Set(aPreviousPos, eTileType.Empty);
        }
        return false;
    }

    bool OnRockMove(Coord aRockPos)
    {
        UpdateLaser();
        UpdateRail();

        Set(aRockPos, eTileType.Rock);
        return false;
    }

    void OnRewind()
    {
        UpdateLaser();
        UpdateRail();
    }

    public void SetAllTiles()
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
        Train[] allTrains = FindObjectsOfType<Train>();
        Rail[] allRails = FindObjectsOfType<Rail>();

        for (int row = 0; row < myRows; ++row)
        {
            for (int column = 0; column < myColumns; ++column)
            {
                foreach (var i in myTiles)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Empty;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allButtons)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Button;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allLasers)
                {
                    int x = Mathf.Clamp(i.GetCoords().x, 0, 6);
                    int y = Mathf.Clamp(i.GetCoords().y, 0, 9);

                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Laser;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allPlayer)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Player;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

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


                foreach (var i in allRails)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Rail;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allTrains)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Train;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }

                foreach (var i in allHoles)
                {
                    myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Hole;
                    myTileMap[i.GetCoords().x, i.GetCoords().y].coord = i.GetCoords();
                }
            }
        }
    }

    public void UpdateLaser()
    {
        Laser[] allLasers = FindObjectsOfType<Laser>();
        HoleBlocking[] allHoles = FindObjectsOfType<HoleBlocking>();

        for (int row = 0; row < myRows; ++row)
        {
            for (int column = 0; column < myColumns; ++column)
            {
                Coord coord = new Coord(column, row);
                if (Get(coord) == eTileType.Laser) Set(coord, eTileType.Empty);
            }
        }

        foreach (var i in allLasers)
        {
            if (Get(i.GetCoords()) != eTileType.Reflector)
            {
                Set(i.GetCoords(), eTileType.Laser);
            }
        }

        foreach (var i in allHoles)
        {
            if (!i.IsFilled())
            {
                Set(i.GetCoords(), eTileType.Hole);
            }
        }
    }

    private void UpdateRail()
    {
        Rail[] allRails = FindObjectsOfType<Rail>();

        for (int row = 0; row < myRows; ++row)
        {
            for (int column = 0; column < myColumns; ++column)
            {
                Coord coord = new Coord(column, row);
                if (Get(coord) == eTileType.Rail) Set(coord, eTileType.Empty);
            }
        }

        foreach (var i in allRails)
        {
            if (myTileMap[i.GetCoords().x, i.GetCoords().y].type == eTileType.Empty)
            {
                myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Rail;
            }
            if (myTileMap[i.GetCoords().x, i.GetCoords().y].type == eTileType.Laser)
            {
                myTileMap[i.GetCoords().x, i.GetCoords().y].type = eTileType.Rail;
            }
        }
    }

    public void DebugTiles()
    {
        string map = "\n";
        for (int i = myRows - 1; i >= 0; --i)
        {
            for (int j = 0; j < myColumns; ++j)
            {
                if (myTileMap[j, i].type == eTileType.Rock)
                    map += " R ";

                else if (myTileMap[j, i].type == eTileType.Impassable)
                    map += " W ";

                else if (myTileMap[j, i].type == eTileType.Sliding)
                    map += " S ";

                else if (myTileMap[j, i].type == eTileType.Hole)
                    map += " O ";

                else if (myTileMap[j, i].type == eTileType.Finish)
                    map += " F ";

                else if (myTileMap[j, i].type == eTileType.Button)
                    map += " B ";

                else if (myTileMap[j, i].type == eTileType.Door)
                    map += " D ";

                else if (myTileMap[j, i].type == eTileType.Emitter)
                    map += " E ";

                else if (myTileMap[j, i].type == eTileType.Reflector)
                    map += " rf ";

                else if (myTileMap[j, i].type == eTileType.Receiver)
                    map += " re ";

                else if (myTileMap[j, i].type == eTileType.Player)
                    map += " p ";

                else if (myTileMap[j, i].type == eTileType.Laser)
                    map += " L ";

                else if (myTileMap[j, i].type == eTileType.Rail)
                    map += " ra ";

                else if (myTileMap[j, i].type == eTileType.Train)
                    map += " Tr ";

                else
                    map += " # ";
            }
            map += "\n";
        }
        Debug.Log(map);
    }

    private void InitializeTileMap()
    {
        if (Instance == null) Instance = this;

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

    public void Set(Coord aCoord, eTileType aType)
    {
        if (aCoord.x < 0 || aCoord.x > (myColumns - 1) || aCoord.y < 0 || aCoord.y > (myRows - 1))
        {
            return;
        }

        myTileMap[aCoord.x, aCoord.y].type = aType;
    }

    public eTileType Get(Coord aCoord)
    {
        if (aCoord.x < 0 || aCoord.x > (myColumns - 1) || aCoord.y < 0 || aCoord.y > (myRows - 1)) return eTileType.Null;
        return myTileMap[aCoord.x, aCoord.y].type;
    }

    public int GetDistance(Coord aPosition, Coord aDirection, bool isLaser = true)
    {
        int distance = 0;
        bool hasFound = false;
        List<eTileType> targets = new List<eTileType>();
        Coord current = aPosition;

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

        if (isLaser)
        {
            eTileType[] laserTargets = { eTileType.Emitter, eTileType.Door, eTileType.Impassable, eTileType.Receiver, eTileType.Reflector, eTileType.Rock, eTileType.Sliding, eTileType.Train, eTileType.Null };
            targets.AddRange(laserTargets);

            // --- Check for all tiles in the direction --- //
            for (int i = 0; i < maxRange; ++i)
            {
                current += aDirection;

                foreach (eTileType target in targets)
                {
                    if (current.x > 6 || current.y > 9 || current.x < 0 || current.y < 0)
                    {
                        hasFound = true;
                    }

                    int x = Mathf.Clamp(current.x, 0, 6);
                    int y = Mathf.Clamp(current.y, 0, 9);

                    // --- If a tile that will stop the laser has been found, break the loop and continue to return distance --- //
                    if (Get(new Coord(x, y)) == target)
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
        else
        {
            eTileType[] slidingTargets = { eTileType.Emitter, eTileType.Door, eTileType.Impassable, eTileType.Player, eTileType.Receiver, eTileType.Reflector, eTileType.Rock, eTileType.Sliding, eTileType.Train };
            targets.AddRange(slidingTargets);

            // --- Check for all tiles in the direction --- //
            for (int i = 0; i < maxRange; ++i)
            {
                current += aDirection;

                foreach (eTileType target in targets)
                {
                    int x = Mathf.Clamp(current.x, -1, 7);
                    int y = Mathf.Clamp(current.y, -1, 10);
                    // --- Should look outside of the bounds of the map

                    if (x == 7 || x == -1 || y == 10 || y == -1)
                    {
                        hasFound = true;
                        break;
                    }

                    // --- Special case for holes to stop earlier with --- //
                    if (myTileMap[x, y].type == eTileType.Hole)
                    {
                        hasFound = true;
                        ++distance;         // make distance match with hole position
                        break;
                    }
                    // --- If a tile that will stop the laser has been found, break the loop and continue to return distance --- //
                    else if (myTileMap[x, y].type == target)
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

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
        EventHandler.current.UnSubscribe(eEventType.Rewind, OnRewind);
    }
}
