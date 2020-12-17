using UnityEngine;

public class HoleBlocking : MonoBehaviour
{
    private Coord myCoords;
    private bool myIsFilled;
    private int myGotFilledAt = -1;
    private int myMoveCounter = 0;
    private bool myShouldIncrement = true;

    private void Start()
    {
        myIsFilled = false;
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) Debug.LogError("Hole movecounter: " + myMoveCounter);
        if (Input.GetKeyDown(KeyCode.G)) Debug.LogError("Got Filled at: " + myGotFilledAt);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        if (myShouldIncrement) myMoveCounter++;
        myShouldIncrement = true;
        return (aPlayerCurrentPos == myCoords);
    }

    private bool OnPlayerMoveInHole(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myMoveCounter++;
        myShouldIncrement = false;
        return false;
    }

    private bool OnRockMove(Coord aRockCurrentPos)
    {
        if (this != null)
        {
            if (aRockCurrentPos == myCoords)
            {
                myIsFilled = true;
                EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
                EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
                EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
                myGotFilledAt = myMoveCounter;
                myShouldIncrement = false;
                return true;
            }
        }  
        return false;
    }

    private void OnRewind()
    {
        if (myGotFilledAt == myMoveCounter)
        {
            myIsFilled = false;
            Debug.LogError("DRR");
            TileMap.Instance.Set(myCoords, eTileType.Hole);
            EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
            EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
            myGotFilledAt = -1;
        }
        if (myMoveCounter > 0) myMoveCounter--;
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
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
        EventHandler.current.UnSubscribe(eEventType.Rewind, OnRewind);
    }
}
