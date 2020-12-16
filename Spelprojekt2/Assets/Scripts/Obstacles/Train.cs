using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myDestinationRot;
    
    private float mySpeed = 4f;
    //private float myRotationLerpSpeed = 0.05f;
    private Coord myCoords;
    private Coord myPreviousCoords;

    private Stack myPreviousMoves;
    [SerializeField] private Animator mySnailAnimator = null;
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
        if (gameObject != null)
        {
            myDestinationRot = transform.eulerAngles;
        }
    }

    private void Update()
    {
        myIsMoving = (transform.position != myDesiredPosition);

        if (ComparePositions(transform.position, myDesiredPosition, 0.02f))
        {
            transform.position = myDesiredPosition;
        }
        else
        {
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
        //transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, myDestinationRot, myRotationLerpSpeed);
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
            TileMap.Instance.Set(myCoords, eTileType.Train);
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
            TileMap.Instance.Get(desiredTile) == eTileType.Train)
            return;
        if (aDirection.x > 0/* && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail*/)
        {
            //myDestinationRot = new Vector3(0, 0, 0);
            myRotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.rotation = myRotation;
        }
        else if (aDirection.x < 0/* && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail*/)
        {
            //myDestinationRot = new Vector3(0, 180, 0);
            myRotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.rotation = myRotation;
        }
        else if (aDirection.y > 0/* && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail*/)
        {
            //myDestinationRot = new Vector3(0, 270, 0);
            myRotation = Quaternion.Euler(0, 90, 0);
            gameObject.transform.rotation = myRotation;
        }
        else if (aDirection.y < 0/* && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail*/)
        {
            //myDestinationRot = new Vector3(0, 90, 0);
            myRotation = Quaternion.Euler(0, -90, 0);
            gameObject.transform.rotation = myRotation;
        }
        // TODO: Add Lookup map of to check if tile is empty!
        if (TileMap.Instance.Get(desiredTile) == eTileType.Rail)
        {
            myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
            myCoords += aDirection;
            TileMap.Instance.Set(previousCoords, eTileType.Rail);
        }
        EventHandler.current.RockMoveEvent(myCoords);

    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void CreateMove()
    {
        var temp = new MoveInfo();
        temp.coord = myCoords;
        temp.position = transform.position;
        myPreviousMoves.Push(temp);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }
}
