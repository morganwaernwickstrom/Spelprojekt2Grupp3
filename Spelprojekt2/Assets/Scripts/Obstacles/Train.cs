using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{

    private Vector3 myDesiredPosition;
    private float mySpeed = 5f;
    private Coord myCoords;
    private Coord myPreviousCoords;

    private Stack myPreviousMoves;
    [SerializeField] private Animator mySnailAnimator;
    bool myIsMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        myPreviousMoves = new Stack();
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myPreviousCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
    }

    private void Update()
    {
        //if (transform.position != myDesiredPosition)
        //{
        //    myIsMoving = true;
        //    transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        //}

        if (ComparePositions(transform.position, myDesiredPosition, 0.01f))
        {
            transform.position = myDesiredPosition;
            myIsMoving = false;
        }
        else if (!ComparePositions(transform.position, myDesiredPosition, 0.01f))
        {
            myIsMoving = true;
            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        }

        mySnailAnimator.SetBool("Moving", myIsMoving);
    }

    private bool ComparePositions(Vector3 aPosition, Vector3 aDesiredPosition, float aDif)
    {
        bool xDist = (Mathf.Abs(aPosition.x - aDesiredPosition.x) < aDif);
        bool yDist = (Mathf.Abs(aPosition.y - aDesiredPosition.y) < aDif);
        bool zDist = (Mathf.Abs(aPosition.z - aDesiredPosition.z) < aDif);

        return (xDist && yDist && zDist);
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

            TileMap.Instance.Set(myPreviousCoords, eTileType.Rail);
            TileMap.Instance.Set(myCoords, eTileType.Rock);
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        CreateMove();

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
            TileMap.Instance.Get(desiredTile) == eTileType.Train)
            return;
        
        if (TileMap.Instance.Get(desiredTile) == eTileType.Rail)
        {
            myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
            myCoords += aDirection;
            TileMap.Instance.Set(previousCoords, eTileType.Rail);
        }
        EventHandler.current.RockMoveEvent(myCoords);
    }

    private void CreateMove()
    {
        var temp = new MoveInfo();
        temp.coord = myCoords;
        temp.position = transform.position;
        //temp.rotation = myRotation;
        myPreviousMoves.Push(temp);
    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.Rewind, OnRewind);
    }
}
