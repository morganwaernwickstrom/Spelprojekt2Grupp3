using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private float mySpeed = 0.1f;
    private Coord myCoords;
    private bool myIsOpened = false;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.ButtonPressed, OnButtonPressed);
        EventHandler.current.Subscribe(eEventType.ButtonUp, OnButtonUp);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);
    }

    public bool Open()
    {
        return myIsOpened;
    }

    private bool OnButtonPressed()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        myDesiredPosition += new Vector3(0, -1f, 0);
        myIsOpened = true;
        return true;
    }

    private bool OnButtonUp()
    {
        myDesiredPosition += new Vector3(0, 1f, 0);
        myIsOpened = false;
        return true;
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (myCoords == aPlayerCurrentPos);
    }

    private bool OnRockMove(Coord aRockCurrentPos, Coord aRockPreviousPos)
    {
        return (myCoords == aRockCurrentPos);
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
