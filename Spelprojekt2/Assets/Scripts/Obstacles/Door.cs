using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myOriginalPosition;
    private float mySpeed = 4f;
    private Coord myCoords;
    private bool myIsOpened = false;
    private bool myShouldClose = true;
    bool myHasSubscribed = false;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        myDesiredPosition = transform.position;
        myOriginalPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.ButtonPressed, OnButtonPressed);
        EventHandler.current.Subscribe(eEventType.ButtonUp, OnButtonUp);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
    }

    private void Update()
    {
        if (transform.position != myDesiredPosition)
        {
            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        }
    }

    public bool Open()
    {
        return myIsOpened;
    }

    private bool OnButtonPressed()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
        myDesiredPosition = new Vector3(transform.position.x, -2.0f, transform.position.z);
        TileMap.Instance.Set(myCoords, eTileType.Empty);
        myIsOpened = true;
        SoundManager.myInstance.PlayDoorOpenSound();
        myHasSubscribed = false;
        return true;
    }

    private bool OnButtonUp()
    {        
        if (!myHasSubscribed)
        {
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
            EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
        }


        TileMap.Instance.Set(myCoords, eTileType.Door);
        myHasSubscribed = true;
        if (myShouldClose)
        {
            myDesiredPosition = myOriginalPosition;
            myIsOpened = false;
        }
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
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
    }
}
