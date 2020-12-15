using UnityEngine;

public class HoleBlocking : MonoBehaviour
{
    private Coord myCoords;
    private bool myIsFilled;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) Debug.Log("movecounter: " + myMoveCounter);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myMoveCounter++;
        return (aPlayerCurrentPos == myCoords);
    }

    private bool OnPlayerMoveInHole(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myMoveCounter++;
        return false;
    }

    private bool OnRockMove(Coord aRockCurrentPos)
    {
        if (this != null)
        {
            if (aRockCurrentPos == myCoords)
            {
                myIsFilled = true;
                myGotFilledAt = myMoveCounter;
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
        if (myMoveCounter == myGotFilledAt && myGotFilledAt != 0)
        {
            myIsFilled = false;
            myGotFilledAt = 0;
            TileMap.Instance.Set(myCoords, eTileType.Hole);
            EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMoveInHole);
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
            EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
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
