using UnityEngine;
using System.Collections;

public class RockMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myCurrentPosition;

    private float mySpeed = 0;
    private float myBaseSpeed = 5f;
    private Coord myCoords;
    private Coord myPreviousCoords;

    private Stack myPreviousMoves;

    private bool myFallingDown = false;
    private bool myPlayFallingSound;

    private int myMoves = 0;
    private int myFellDownAt = -1;

    private bool myHasSubscribed = false;
    private bool myShouldMoveInY = false;
    private bool myIsRewinding = false;

    private float myRewindTimerMax;
    private float myRewindTimer;

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
    private bool ComparePositions(Vector3 aPosition, Vector3 aDesiredPosition, float aDif)
    {
        bool xDist = (Mathf.Abs(aPosition.x - aDesiredPosition.x) < aDif);
        bool yDist = (Mathf.Abs(aPosition.y - aDesiredPosition.y) < aDif);
        bool zDist = (Mathf.Abs(aPosition.z - aDesiredPosition.z) < aDif);

        return (xDist && yDist && zDist);
    }

    private bool CompareFloat(float aMyFloat, float aMyDesired, float aDif)
    {
        bool yDist = (Mathf.Abs(aMyFloat - aMyDesired) < aDif);

        return yDist;
    }

    private void LerpXZ()
    {
        Vector3 temp = myDesiredPosition;
        temp.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, temp, mySpeed * Time.deltaTime);

        if (transform.position != myDesiredPosition)
        {
            if (CompareFloat(transform.position.x, myDesiredPosition.x, 0.1f) && CompareFloat(transform.position.z, myDesiredPosition.z, 0.1f))
            {
                transform.position = temp;
                myShouldMoveInY = !myShouldMoveInY;
            }
        }
    }

    private void LerpY()
    {
        Vector3 temp = myDesiredPosition;
        temp.x = transform.position.x;
        temp.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, temp, mySpeed * 5 * Time.deltaTime);

        if (transform.position != myDesiredPosition)
        {
            if (CompareFloat(transform.position.y, myDesiredPosition.y, 0.1f))
            {
                transform.position = temp;
                myShouldMoveInY = !myShouldMoveInY;
            }
        }
    }

    private void Update()
    {
        mySpeed = myBaseSpeed * EventHandler.speedMultiplier;

        if (transform.position == myDesiredPosition && myFallingDown)
        {
            TileMap.Instance.Set(myCoords, eTileType.Empty);
            myFallingDown = false;
        }

        if (!myHasSubscribed && myFallingDown)
        {
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
            myHasSubscribed = true;
        }

        if (myFallingDown)
        {
            EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
            if (myPlayFallingSound)
            {
                SoundManager.myInstance.PlayRockFallingSound();
            }
            myPlayFallingSound = false;
        }

        if (!myShouldMoveInY)
        {
            LerpXZ();
        }
        else
        {
            LerpY();
        }

        if (ComparePositions(transform.position, myDesiredPosition, 0.1f))
        {
            transform.position = myDesiredPosition;
            myIsRewinding = false;
        }

        if (Input.GetKeyDown(KeyCode.G)) Debug.LogError("Rock Moves: " + myMoves);
    }

    private void OnRewind()
    {
        myIsRewinding = true;
        myShouldMoveInY = true;

        if (myPreviousMoves.Count > 0)
        {
            var moveInfo = (MoveInfo)myPreviousMoves.Peek();
            myPreviousCoords = myCoords;
            myCoords = moveInfo.coord;
            myDesiredPosition = moveInfo.position;
            myRewindTimerMax = moveInfo.duration;
            myPreviousMoves.Pop();
           
            if (myCoords != myPreviousCoords)
            {
                TileMap.Instance.Set(myPreviousCoords, eTileType.Empty);
                TileMap.Instance.Set(myCoords, eTileType.Rock);
            }

            if (myFellDownAt == myMoves)
            {
                myFellDownAt = -1;
                myHasSubscribed = false;
                EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
                EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
                TileMap.Instance.Set(myPreviousCoords, eTileType.Hole);
                TileMap.Instance.Set(myCoords, eTileType.Rock);
            }
        }
        if (myMoves > 0) myMoves--;

        //TileMap.Instance.Set(myCoords, eTileType.Empty);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myShouldMoveInY = false;

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
        myCoords += aDirection;

        if (EventHandler.current.RockMoveEvent(myCoords))
        {
            myDesiredPosition = new Vector3(Mathf.RoundToInt(myDesiredPosition.x), myDesiredPosition.y - 0.7f, Mathf.RoundToInt(myDesiredPosition.z));
            myFallingDown = true;
            myFellDownAt = myMoves;
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
        myMoves++;
        var temp = new MoveInfo();
        temp.coord = myCoords;
        temp.position = myDesiredPosition;
        temp.duration = 2f;
        myPreviousMoves.Push(temp);
    }
}
