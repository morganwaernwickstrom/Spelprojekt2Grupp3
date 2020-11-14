using System;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    public event Func<Coord, bool> MoveEvent;
    private Vector3 myDesiredPosition;
    private float mySpeed = 0.1f;
    private Coord myCoords;

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        FindObjectOfType<PlayerMovement>().MoveEvent += OnPlayerMove;
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
        RockMovement[] otherRocks = FindObjectsOfType<RockMovement>();
        foreach (RockMovement rock in otherRocks)
        {
            if ((myCoords + aDirection) == rock.GetCoords())
            {
                return;
            }
        }
        myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
        myCoords += aDirection;

        if (MoveEvent != null)
        {
            foreach (Func<Coord, bool> f in MoveEvent.GetInvocationList())
            {
                if (f(myCoords))
                {
                    myDesiredPosition += new Vector3(0, -1f, 0);
                }
            }
        }
    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void OnDestroy()
    {
        if (FindObjectOfType<PlayerMovement>())
        {
            FindObjectOfType<PlayerMovement>().MoveEvent -= OnPlayerMove;
        }
    }
}
