using UnityEngine;

public class SlidingRockMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myCurrentPosition;
    private float mySpeed = 3f;
    private float myFallingSpeed = 0.0000001f;
    private int myLimitedChecks = 10;
    private Coord myCoords;
    private bool myHitObstacle = false;
    private bool myHitHole = false;
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
            myDesiredPosition += new Vector3 ( 0, -1, 0 );
            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, myFallingSpeed* Time.deltaTime);
            myFallingDown = false;
            myHitHole = false;
        }

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
        SlidingRockMovement[] otherSlidingRocks = FindObjectsOfType<SlidingRockMovement>();
        HoleBlocking[] otherHoles = FindObjectsOfType<HoleBlocking>();
        Train[] otherTrain = FindObjectsOfType<Train>();
        ReflectorScript[] otherReflectors = FindObjectsOfType<ReflectorScript>();
        LaserEmitterScript[] otherEmittors = FindObjectsOfType<LaserEmitterScript>();
        ReceiverScript[] otherReceivers = FindObjectsOfType<ReceiverScript>();
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
            if ((myCoords + aDirection) == door.GetCoords() && !door.Open())
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
        foreach (var slideRock in otherSlidingRocks)
        {
            if ((myCoords + aDirection) == slideRock.GetCoords())
            {
                return;
            }
        }
        foreach (var train in otherTrain)
        {
            if ((myCoords + aDirection) == train.GetCoords())
            {
                return;
            }
        }
        foreach (var emittor in otherEmittors)
        {
            if ((myCoords + aDirection) == emittor.GetCoords())
            {
                return;
            }
        }
        foreach (var reflector in otherReflectors)
        {
            if ((myCoords + aDirection) == reflector.GetCoords())
            {
                return;
            }
        }
        foreach (var receiver in otherReceivers)
        {
            if ((myCoords + aDirection) == receiver.GetCoords())
            {
                return;
            }
        }

        while (!myHitObstacle && myLimitedChecks > 0)
        {
            if (aDirection.x > 0)
            {
                aDirection.x += 1;
            }
            if (aDirection.x < 0)
            {
                aDirection.x -= 1;
            }
            if (aDirection.y > 0)
            {
                aDirection.y += 1;
            }
            if (aDirection.y < 0)
            {
                aDirection.y -= 1;
            }

            foreach (var rock in otherRocks)
            {
                if ((myCoords + aDirection) == rock.GetCoords())
                {
                    myHitObstacle = true;
                }
            }
            foreach (var door in otherDoors)
            {
                if ((myCoords + aDirection) == door.GetCoords() && !door.Open())
                {
                    myHitObstacle = true;
                }
            }
            foreach (var wall in otherWalls)
            {
                if ((myCoords + aDirection) == wall.GetCoords())
                {
                    myHitObstacle = true;
                }
            }
            foreach (var slideRock in otherSlidingRocks)
            {
                if ((myCoords + aDirection) == slideRock.GetCoords())
                {
                    myHitObstacle = true;
                }
            }
            foreach (var hole in otherHoles)
            {
                if ((myCoords + aDirection) == hole.GetCoords())
                {
                    myHitHole = true;
                    myHitObstacle = true;
                }
            }
            foreach (var emittor in otherEmittors)
            {
                if ((myCoords + aDirection) == emittor.GetCoords())
                {
                    myHitObstacle = true;
                }
            }
            foreach (var reflector in otherReflectors)
            {
                if ((myCoords + aDirection) == reflector.GetCoords())
                {
                    myHitObstacle = true;
                }
            }
            foreach (var train in otherTrain)
            {
                if ((myCoords + aDirection) == train.GetCoords())
                {
                    myHitObstacle = true;
                }
            }
            foreach (var receiver in otherReceivers)
            {
                if ((myCoords + aDirection) == receiver.GetCoords())
                {
                    myHitObstacle = true;
                }
            }
            if (myHitObstacle && !myHitHole)
            {
                if (aDirection.x > 0)
                {
                    aDirection.x -= 1;
                }
                if (aDirection.x < 0)
                {
                    aDirection.x += 1;
                }
                if (aDirection.y > 0)
                {
                    aDirection.y -= 1;
                }
                if (aDirection.y < 0)
                {
                    aDirection.y += 1;
                }
            }
            myLimitedChecks--;
        }

        myLimitedChecks = 10;
        myHitObstacle = false;

        myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
        myCoords += aDirection;

        if (EventHandler.current.RockMoveEvent(myCoords))
        {
            myFallingDown = true;
            myDesiredPosition = new Vector3(Mathf.RoundToInt(myDesiredPosition.x), myDesiredPosition.y, Mathf.RoundToInt(myDesiredPosition.z));
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
