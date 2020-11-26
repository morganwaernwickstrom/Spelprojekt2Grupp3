using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [Tooltip("Check one of them: 'GoOnX or GoOnZ', don't check both boxes.")]
    public bool GoOnX;
    [Tooltip("Check one of them: 'GoOnX or GoOnZ', don't check both boxes.")]
    public bool GoOnZ;

    private Vector3 myDesiredPosition;
    private Vector3 myCurrentPosition;
    private float mySpeed = 5f;
    private Coord myCoords;
    
    // Start is called before the first frame update
    void Start()
    { 
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed*Time.deltaTime);

        myCurrentPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));

        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }

    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        // findgameobject rail
        // desired position finns en rail
        
        if (myCoords == aPlayerCurrentPos)
        {

            if (GoOnX)
            {
                if (aPlayerPreviousPos.x == myCoords.x - 1)
                {
                    Move(new Coord(1, 0));
                }
                if (aPlayerPreviousPos.x == myCoords.x + 1)
                {
                    Move(new Coord(-1, 0));
                }
            }
            else
            { 
                if (aPlayerPreviousPos.y == myCoords.y - 1)
                {
                    Move(new Coord(0, 1));
                }
                if (aPlayerPreviousPos.y == myCoords.y + 1)
                {
                    Move(new Coord(0, -1));
                }
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

        Rail[] otherRails = FindObjectsOfType<Rail>();

        RockMovement[] otherRocks = FindObjectsOfType<RockMovement>();
        Door[] otherDoors = FindObjectsOfType<Door>();
        Impassable[] otherWalls = FindObjectsOfType<Impassable>();
        SlidingRockMovement[] otherSlidingRocks = FindObjectsOfType<SlidingRockMovement>();
        HoleBlocking[] otherHoles = FindObjectsOfType<HoleBlocking>();
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
        foreach (var slideRock in otherSlidingRocks)
        {
            if ((myCoords + aDirection) == slideRock.GetCoords())
            {
                return;
            }
        }
        foreach (var hole in otherHoles)
        {
            if ((myCoords + aDirection) == hole.GetCoords())
            {
                return;
            }
        }


        bool TakeAStep = true;
        foreach (var Rail in otherRails)
        {
            
            if ((myCoords + aDirection) == Rail.GetCoords() && TakeAStep)
            {
                myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
                myCoords += aDirection;
                TakeAStep = false;
            }
        }


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
