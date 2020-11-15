using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private float mySpeed = 0.1f;
    private Coord myCoords;

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.ButtonPressed, OnButtonPressed);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    private bool OnButtonPressed()
    {
        myDesiredPosition += new Vector3(0, -1f, 0);
        return true;
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (myCoords == aPlayerCurrentPos);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.ButtonPressed, OnButtonPressed);
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }
}
