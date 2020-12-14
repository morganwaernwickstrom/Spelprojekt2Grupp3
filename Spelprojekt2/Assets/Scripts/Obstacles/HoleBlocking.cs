using UnityEngine;

public class HoleBlocking : MonoBehaviour
{
    private Coord myCoords;
    private bool myIsFilled;
    private int myRewindCounter = 0;
    private int myGotFilledAt = 0;
    private int myMoveCounter = 0;
    private void Start()
    {
        myIsFilled = false;
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (aPlayerCurrentPos == myCoords);
    }

    private bool OnPlayerMoveInHole(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        if (myIsFilled) myGotFilledAt++;
        return false;
    }

    private bool OnRockMove(Coord aRockCurrentPos)
    {
        if (this != null)
        {
            if (aRockCurrentPos == myCoords)
            {
                Debug.Log("Filled!");
                myIsFilled = true;
                myGotFilledAt++;
                EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
                EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
                EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
                return true;
            }
        }  
        return false;
    }

    private void OnRewind()
    {
        myRewindCounter++;
        Debug.Log("Rewind Counter: " + myRewindCounter);
        Debug.Log("Filled Counter: " + myGotFilledAt);
        if (myRewindCounter == myGotFilledAt)
        {
            //Debug.Log("Called hole!");
            myIsFilled = false;
            TileMap.Instance.Set(myCoords, eTileType.Hole);
            EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
            EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
            myRewindCounter = 0;
            myGotFilledAt = 0;
        }
    }
    
    public bool IsFilled()
    {
        return myIsFilled;
    }
    public Coord GetCoords()
    {
        return myCoords;
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
        EventHandler.current.UnSubscribe(eEventType.Rewind, OnRewind);
    }
}
