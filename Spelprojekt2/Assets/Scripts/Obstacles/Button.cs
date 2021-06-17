using UnityEngine;

public class Button : MonoBehaviour
{
    private Coord myCoords;
    int myCounter = 0;
    bool myIsActive = false;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerInteract, OnPlayerInteract);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockInteract, OnRockInteract);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
    }

    private bool OnPlayerInteract(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        if (myIsActive)
        {
            myCounter++;
        }

        if (aPlayerCurrentPos == myCoords && aPlayerPreviousPos != myCoords)
        {
            ActivateButton();
            return true;
        }
        if (aPlayerPreviousPos == myCoords && aPlayerCurrentPos != myCoords)
        {
            DeactivateButton();
            return true;
        }
        return false;
    }

    private bool OnRockInteract(Coord aRockCurrentPos, Coord aRockPreviousPosition)
    {
        if (aRockCurrentPos == myCoords && aRockPreviousPosition != myCoords)
        {
            ActivateButton();
            return true;
        }
        if (aRockPreviousPosition == myCoords && aRockCurrentPos != myCoords)
        {
            DeactivateButton();
            return true;
        }
        return false;
    }

    private void OnRewind()
    {
        if (myIsActive && myCounter > 0)
        {
            myCounter--;
        }

        if (myCounter == 0 && myIsActive)
        {
            DeactivateButton();
        }
    }

    private void ActivateButton()
    {
        EventHandler.current.ButtonPressedEvent();
        TileMap.Instance.SetAllTiles();
        myIsActive = true;
    }

    private void DeactivateButton()
    {
        EventHandler.current.ButtonUpEvent();
        TileMap.Instance.SetAllTiles();
        myIsActive = false;
    }

    private void OnPlayerMove()
    {
        if (myIsActive)
        {
            myCounter++;
        }
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerInteract, OnPlayerInteract);
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockInteract, OnRockInteract);
        EventHandler.current.UnSubscribe(eEventType.Rewind, OnRewind);
    }

    public Coord GetCoords()
    {
        return myCoords;
    }
}
