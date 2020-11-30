using UnityEngine;

public class SlidingRockMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myCurrentPosition;
    private float mySpeed = 3f;
    private float myFallingSpeed = 0.0000001f;
    private Coord myCoords;
    private bool myFallingDown = false;
    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);

        myCurrentPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));


        if (myCurrentPosition == myDesiredPosition && myFallingDown)
        {
            myDesiredPosition += new Vector3(0, -1, 0);
            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, myFallingSpeed * Time.deltaTime);
            myFallingDown = false;
        }

        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
            TileMap.Instance.Set(myCoords, eTileType.Empty);
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
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
            TileMap.Instance.Get(desiredTile) == eTileType.Train ||
            TileMap.Instance.Get(desiredTile) == eTileType.Finish)
            return;
       
            if (aDirection.x > 0)
            {
                aDirection.x = TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            }
            if (aDirection.x < 0)
            {
                aDirection.x = -TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            }
            if (aDirection.y > 0)
            {
                aDirection.y = TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            }
            if (aDirection.y < 0)
            {
                aDirection.y = -TileMap.Instance.GetDistance(previousCoords, aDirection, false);
            }

        myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
        myCoords += aDirection;

        if (EventHandler.current.RockMoveEvent(myCoords))
        {
            myFallingDown = true;
            myDesiredPosition = new Vector3(Mathf.RoundToInt(myDesiredPosition.x), myDesiredPosition.y, Mathf.RoundToInt(myDesiredPosition.z));
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
    }
}
