using UnityEngine;

public class RockMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private float mySpeed = 0.1f;
    private Coord myCoords;

    private void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
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
        RockMovement[] otherRocks = FindObjectsOfType<RockMovement>();
        Door[] otherDoors = FindObjectsOfType<Door>();
        Impassable[] otherWalls = FindObjectsOfType<Impassable>();
        // TODO: Add Lookup map of to check if tile is empty!
        foreach (var rock in otherRocks)
        {
            if ((myCoords + aDirection) == rock.GetCoords())
            {
                return;
            }
        }
        foreach (var door in otherDoors)
        {
            if ((myCoords + aDirection) == door.GetCoords())
            {
                return;
            }
        }
        foreach (var wall in otherWalls)
        {
            if ((myCoords + aDirection) == wall.GetCoords())
            {
                return;
            }
        }

        myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
        myCoords += aDirection;

        if (EventHandler.current.RockMoveEvent(myCoords))
        {
            myDesiredPosition += new Vector3(0, -1f, 0);
        }
        EventHandler.current.RockInteractEvent(myCoords, previousCoords);
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
