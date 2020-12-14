using UnityEngine;
using System.Collections;

public class RockMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myCurrentPosition;
    private Vector3 myPreviousPosition;

    private float mySpeed = 5f;
    private Coord myCoords;
    private Coord myPreviousCoords;

    private Stack myPreviousMoves;

    private bool myFallingDown;
    private bool myPlayFallingSound;
    private bool myHasMoved = false;

    private void Start()
    {
        myPreviousMoves = new Stack();

        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        myPreviousCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
        myPlayFallingSound = true;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        myCurrentPosition = new Vector3(Round(transform.position.x, 1), transform.position.y, Round(transform.position.z, 1));

        if (myCurrentPosition == myDesiredPosition && myFallingDown)
        {
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
            myDesiredPosition += new Vector3(0, -0.7f, 0);
            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * 5 * Time.deltaTime);
            myFallingDown = false;
        }

        if (myFallingDown)
        {
            TileMap.Instance.Set(myCoords, eTileType.Empty);
            EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
            if (myPlayFallingSound) 
            {
                myPlayFallingSound = false;
                SoundManager.myInstance.PlayRockFallingSound();
            }
            
        }
    }

    private void OnRewind()
    {
        //if (myHasMoved)
        //{
        //    if (transform.position.y < 0)
        //    {
        //        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        //        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
        //        TileMap.Instance.Set(myCoords, eTileType.Hole);
        //    }
        //    TileMap.Instance.Set(myCoords, eTileType.Empty);
        //    TileMap.Instance.Set(myPreviousCoords, eTileType.Rock);
        //    myCoords = myPreviousCoords;
        //    myDesiredPosition = myPreviousPosition;
        //}

        if (myPreviousMoves.Count > 0)
        {
            var moveInfo = (MoveInfo)myPreviousMoves.Peek();
            myPreviousCoords = myCoords;
            myCoords = moveInfo.coord;
            myDesiredPosition = moveInfo.position;
            myPreviousMoves.Pop();

            if (transform.position.y < 0)
            {
                EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
                EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
                TileMap.Instance.Set(myPreviousCoords, eTileType.Hole);
            }
            else
            {
                TileMap.Instance.Set(myPreviousCoords, eTileType.Empty);
            }

            TileMap.Instance.Set(myCoords, eTileType.Rock);
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myHasMoved = false;
        CreateMove();
        if (myCoords == aPlayerCurrentPos)
        {
            SoundManager.myInstance.PlayRockSound();

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

    private bool OnPlayerMoveInHole(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        CreateMove();
        myHasMoved = false;
        return false;
    }

    private void Move(Coord aDirection)
    {
        Coord previousCoords = myCoords;
        Coord desiredTile = myCoords + aDirection;
        if (TileMap.Instance.Get(desiredTile) == eTileType.Rock ||
            TileMap.Instance.Get(desiredTile) == eTileType.Door ||
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
        myPreviousCoords = myCoords;
        myPreviousPosition = transform.position;
        myCoords += aDirection;
        myHasMoved = true;

        if (EventHandler.current.RockMoveEvent(myCoords))
        {
            myDesiredPosition = new Vector3(Mathf.RoundToInt(myDesiredPosition.x), myDesiredPosition.y, Mathf.RoundToInt(myDesiredPosition.z));
            myFallingDown = true;
        }
        EventHandler.current.RockInteractEvent(myCoords, previousCoords);
        TileMap.Instance.Set(previousCoords, eTileType.Empty);
    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
        EventHandler.current.UnSubscribe(eEventType.Rewind, OnRewind);
    }
    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    private void CreateMove()
    {
        var temp = new MoveInfo();
        temp.coord = myCoords;
        temp.position = transform.position;
        myPreviousMoves.Push(temp);
    }
}
