using UnityEngine;

public class Button : MonoBehaviour
{
    private Coord myCoords;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerInteract, OnPlayerInteract);
        EventHandler.current.Subscribe(eEventType.RockInteract, OnRockInteract);
    }

    private bool OnPlayerInteract(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
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

    private void ActivateButton()
    {
        EventHandler.current.ButtonPressedEvent();
    }

    private void DeactivateButton()
    {
        EventHandler.current.ButtonUpEvent();
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerInteract, OnPlayerInteract);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockInteract);
    }
}
