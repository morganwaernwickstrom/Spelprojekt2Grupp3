using UnityEngine;

public class Button : MonoBehaviour
{
    private Coord myCoords;

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        if (aPlayerCurrentPos == myCoords) ActivateButton();
        return (aPlayerCurrentPos == myCoords);
    }

    private bool OnRockMove(Coord aRockCurrentPos)
    {
        if (aRockCurrentPos == myCoords) ActivateButton();
        return (aRockCurrentPos == myCoords);
    }

    private void ActivateButton()
    {
        EventHandler.current.ButtonPressedEvent();
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
    }
}
