using System.Collections.Generic;
using UnityEngine;

public class ReflectorScript : MonoBehaviour
{
    enum Direction
    {
        Null,
        Left,
        Right
    }

    // --- Laser Object pool --- //
    public GameObject myLaser;
    private List<GameObject> myLaserPool;
    private int myAmountOfLasers = 20;

    // --- Distances to draw laser with --- //
    private float myPreviousLaserDistance = 0;
    private float myLaserDistance = 0;

    // --- Origin points for lasers --- //
    [SerializeField] private Transform myOrigin = null;            // The moving origin point from which lasers instantiate. Both myOrigin and myFirstOrigin depend on myLeftOrigin & myRighOrigin
    [SerializeField] private Transform myFirstOrigin = null;       // Is at the same position as myOrigin at the first laser object, used to see where laser should start at Re-draw
    [SerializeField] private Transform myLeftOrigin = null;        // The static/not moving origin point for laser going left
    [SerializeField] private Transform myRightOrigin = null;       // The static/not moving origin point for laser going right

    [SerializeField] private Transform myLeftLaserRotation = null;     // Rotation for instantiated laser objects going left
    [SerializeField] private Transform myRightLaserRotation = null;    // Rotation for instantiated laser objects going right
    private Transform myLaserRotation;                          // Is assigned to be either myLeftLaserRotation or myRighLaserRotation

    // --- Detection boxes to determine where the laser is coming from --- //
    [SerializeField] private LaserDetectionScript myLeftDetectionBox = null;
    [SerializeField] private LaserDetectionScript myRightDetectionBox = null;

    // --- Is the reflector hit by laser as well as which direction it should take --- //
    private bool myIsHit = false;
    private Direction myDirection = Direction.Null;

    // Coordinates to use for collision checking
    private Coord myCoords;

    private Vector3 myDesiredPosition;
    private float mySpeed = 1f;

    void Start()
    {
        myLaserPool = new List<GameObject>();

        for (int i = 0; i < myAmountOfLasers; ++i)
        {
            GameObject temp = Instantiate(myLaser);
            temp.SetActive(false);
            myLaserPool.Add(temp);
        }

        UpdateLaser();

        myDesiredPosition = transform.position;
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
    }

    private void OnEnable()
    {
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);
    }

    private void UpdateLaser()
    {
        bool leftHit = myLeftDetectionBox.myIsHit;
        bool rightHit = myRightDetectionBox.myIsHit;

        myLeftDetectionBox.CheckIfExited();
        myRightDetectionBox.CheckIfExited();

        myIsHit = (leftHit || rightHit);

        myPreviousLaserDistance = myLaserDistance;

        if (myIsHit)
        {
            if (myLeftDetectionBox.myIsHit)
            {
                myFirstOrigin.position = myRightOrigin.position;
                myFirstOrigin.rotation = myRightOrigin.rotation;
                myLaserRotation = myRightLaserRotation;
                myDirection = Direction.Right;
            }
            else if (myRightDetectionBox.myIsHit)
            {
                myFirstOrigin.position = myLeftOrigin.position;
                myFirstOrigin.rotation = myLeftOrigin.rotation;
                myLaserRotation = myLeftLaserRotation;
                myDirection = Direction.Left;
            }

            myOrigin.position = myFirstOrigin.position;
            myOrigin.rotation = myFirstOrigin.rotation;

            CheckDistance();

            if (myPreviousLaserDistance != myLaserDistance)     // Only draw laser if the distance has changed
            {
                DrawLaser();
            }
        }
        else
        {
            CheckDistance();
            myDirection = Direction.Null;
            ClearLaser();
        }
    }

    // --- Draws the laser based on information gathered from Raycast and more in Update function --- //
    private void DrawLaser()
    {
        ClearLaser();
        int amount = Mathf.RoundToInt(myLaserDistance);

        if (myLaserRotation == myLeftLaserRotation || myLaserRotation == myRightLaserRotation)
        {
            for (int count = 0; count < amount; ++count)
            {
                myLaserPool[count].SetActive(true);
                myLaserPool[count].transform.position = myOrigin.position;
                myLaserPool[count].transform.rotation = myLaserRotation.rotation;
                myOrigin.Translate(Vector3.forward * 1, Space.Self);
            }
        }
    }

    private void ClearLaser()
    {
        foreach (GameObject laser in myLaserPool)
        {
            laser.SetActive(false);
        }
    }

    private void CheckDistance()
    {
        // --- If hit by laser, decide the direction the laser is being drawn towards, based on object rotation --- //
        if (myIsHit)
        {
            Coord direction = new Coord(0, 0);

            if (myDirection == Direction.Left)
            {
                if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 0)
                {
                    direction.x = 0;
                    direction.y = 1;
                }
                else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 90)
                {
                    direction.x = 1;
                    direction.y = 0;
                }
                else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == -180 || Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 180)
                {
                    direction.x = 0;
                    direction.y = -1;
                }
                else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == -90 || Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 270)
                {
                    direction.x = -1;
                    direction.y = 0;
                }
            }
            else if (myDirection == Direction.Right)
            {
                if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 0)
                {
                    direction.x = 1;
                    direction.y = 0;
                }
                else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 90)
                {
                    direction.x = 0;
                    direction.y = -1;
                }
                else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == -180 || Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 180)
                {
                    direction.x = -1;
                    direction.y = 0;
                }
                else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == -90 || Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 270)
                {
                    direction.x = 0;
                    direction.y = 1;
                }
            }

            myLaserDistance = TileMap.Instance.GetDistance(myCoords, direction);
        }
        else
        {
            myLaserDistance = 0f;
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        UpdateLaser();
        if (myCoords == aPlayerCurrentPos)
        {
            if (aPlayerPreviousPos.x == myCoords.x - 1)
            {
                Move(new Coord(1, 0));
            }
            if (aPlayerPreviousPos.x == myCoords.x + 1)
            {
                Move(new Coord(-1, 0));
            }
            if (aPlayerPreviousPos.y == myCoords.y - 1)
            {
                Move(new Coord(0, 1));
            }
            if (aPlayerPreviousPos.y == myCoords.y + 1)
            {
                Move(new Coord(0, -1));
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    private bool OnRockMove(Coord aRockPos)
    {
        if (TileMap.Instance.Get(aRockPos) == eTileType.Laser || TileMap.Instance.Get(aRockPos) == eTileType.Hole)
        {
            UpdateLaser();
        }
        UpdateLaser();
        return false;
    }

    private void Move(Coord aDirection)
    {
        ClearLaser();
        Coord previousCoords = myCoords;
        Coord desiredTile = myCoords + aDirection;
        if (TileMap.Instance.Get(desiredTile) == eTileType.Rock ||
            TileMap.Instance.Get(desiredTile) == eTileType.Door ||
            TileMap.Instance.Get(desiredTile) == eTileType.Hole ||
            TileMap.Instance.Get(desiredTile) == eTileType.Emitter ||
            TileMap.Instance.Get(desiredTile) == eTileType.Reflector ||
            TileMap.Instance.Get(desiredTile) == eTileType.Receiver ||
            TileMap.Instance.Get(desiredTile) == eTileType.Impassable ||
            TileMap.Instance.Get(desiredTile) == eTileType.Sliding ||
            TileMap.Instance.Get(desiredTile) == eTileType.Train ||
            TileMap.Instance.Get(desiredTile) == eTileType.Null ||
            TileMap.Instance.Get(desiredTile) == eTileType.Finish)
            return;

        myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
        myCoords += aDirection;

        if (EventHandler.current.RockMoveEvent(myCoords))
        {
            myDesiredPosition += new Vector3(0, -1f, 0);
        }
        EventHandler.current.RockInteractEvent(myCoords, previousCoords);
        TileMap.Instance.Set(previousCoords, eTileType.Empty);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
    }
    public Coord GetCoords()
    {
        return myCoords;
    }
}
