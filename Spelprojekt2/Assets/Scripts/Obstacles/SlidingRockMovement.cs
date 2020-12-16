using System.Collections;
using UnityEngine;

public class SlidingRockMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myCurrentPosition;
    private float mySpeed = 0.1f;
    private float myLerpSpeed = 0.1f;
    private Coord myCoords;
    private Coord myPreviousCoords;
    private bool myFallingDown = false;
    private Animator myAnimator;
    private float myPercentage;

    private Stack myPreviousMoves;

    private int myMoves = 0;
    private int myFellDownAt = 0;

    private void Start()
    {
        myPreviousMoves = new Stack();

        myCoords = myPreviousCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
        myAnimator = GetComponentInChildren<Animator>();
        myPercentage = 0.0f;
    }

    private void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        HandleLerpLogic();

        myCurrentPosition = new Vector3(Round(transform.position.x, 1), transform.position.y, Round(transform.position.z, 1));

        float distance = Vector3.Distance(transform.position, myDesiredPosition);

        if(distance > 1.0f) 
        {
            myAnimator.SetBool("Walk", true);
        }
        else 
        {
            myAnimator.SetBool("Walk", false);
        }

        if (myCurrentPosition == myDesiredPosition && myFallingDown)
        {
            myFellDownAt = myMoves;
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
            myDesiredPosition += new Vector3(0, -0.7f, 0);
            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
            myFallingDown = false;
        }

        if (myFallingDown)
        {
            TileMap.Instance.Set(myCoords, eTileType.Empty);
            EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        }
    }

    private void HandleLerpLogic() 
    {
        if(myPercentage > 1.0f) 
        {
            transform.position = myDesiredPosition;
        }
        else 
        {
            myPercentage += Time.deltaTime * myLerpSpeed;

            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, myPercentage);
        }
    }

    private void OnRewind()
    {
        if (myPreviousMoves.Count > 0)
        {
            var moveInfo = (MoveInfo)myPreviousMoves.Peek();
            myPreviousCoords = myCoords;
            myCoords = moveInfo.coord;
            myDesiredPosition = moveInfo.position;
            myPreviousMoves.Pop();

            if (myFellDownAt == myMoves && myFellDownAt != 0)
            {
                myFellDownAt = 0;
                EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
                EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
                TileMap.Instance.Set(myPreviousCoords, eTileType.Hole);
            }
            else
            {
                TileMap.Instance.Set(myPreviousCoords, eTileType.Empty);
            }

            if (myPreviousCoords == myCoords)
            {
                TileMap.Instance.Set(myCoords, eTileType.Sliding);
            }
        }
        if (myMoves > 0) myMoves--;
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        CreateMove();
        if (myCoords == aPlayerCurrentPos)
        {
            PlaySoundEffect();
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

    private void PlaySoundEffect() 
    {
        if(SoundManager.myInstance != null) 
        {
            SoundManager.myInstance.PlaySlidingSound();
        }
    }

    private void Move(Coord aDirection)
    {
        int clampMin = 3, clampMax = 8;
        Quaternion myRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
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
            TileMap.Instance.Get(desiredTile) == eTileType.Finish)
            return;
        
        // Use direction to get the right direction with the GetDistance Function
        if (aDirection.x > 0)
        {
            aDirection.x = TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            mySpeed = Mathf.Clamp(8 - aDirection.x, clampMin, clampMax);
            myRotation = Quaternion.Euler(0, 90, 0);
            gameObject.transform.rotation = myRotation;
        }
        if (aDirection.x < 0)
        {
            aDirection.x = -TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            mySpeed = Mathf.Clamp(8 + aDirection.x, clampMin, clampMax);
            myRotation = Quaternion.Euler(0, -90, 0);
            gameObject.transform.rotation = myRotation;
        }
        if (aDirection.y > 0)
        {
            aDirection.y = TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            mySpeed = Mathf.Clamp(8 - aDirection.y, clampMin, clampMax);
            myRotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.rotation = myRotation;
        }
        if (aDirection.y < 0)
        {
            aDirection.y = -TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            mySpeed = Mathf.Clamp(8 + aDirection.y, clampMin, clampMax);
            myRotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.rotation = myRotation;
        }

        myPercentage = 0.0f;
        myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
        myCoords += aDirection;

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

    private void CreateMove()
    {
        myMoves++;
        var temp = new MoveInfo();
        temp.coord = myCoords;
        temp.position = myCurrentPosition;
        myPreviousMoves.Push(temp);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}
