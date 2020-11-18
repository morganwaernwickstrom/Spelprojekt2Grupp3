using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private float mySpeed = 0.1f;
    private Coord myCoords;

    private void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.ButtonPressed, OnButtonPressed);
        EventHandler.current.Subscribe(eEventType.ButtonUp, OnButtonUp);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);
    }

    private bool OnButtonPressed()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        myDesiredPosition += new Vector3(0, -1f, 0);
        return true;
    }

    private bool OnButtonUp()
    {
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        myDesiredPosition += new Vector3(0, 1f, 0);
        return true;
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (myCoords == aPlayerCurrentPos);
    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.ButtonPressed, OnButtonPressed);
        EventHandler.current.UnSubscribe(eEventType.ButtonUp, OnButtonUp);
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }
}
