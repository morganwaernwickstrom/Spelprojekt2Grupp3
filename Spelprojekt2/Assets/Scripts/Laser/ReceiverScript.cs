using UnityEngine;

public class ReceiverScript : MonoBehaviour
{
    //public GameObject myConnectedObject;
    public bool myIsActivated = false;
    private bool myHasOpenedDoor = false;
    private Collider myIncomingLaserCollider;

    // Coordinates to use for collision checking
    private Coord myCoords;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    void OnTriggerEnter(Collider anOther)
    {
        if (anOther.CompareTag("Laser"))
        {
            myIsActivated = true;
            myHasOpenedDoor = false;
            myIncomingLaserCollider = anOther;
            EventHandler.current.ButtonPressedEvent();
        }
    }

    void Update()
    {
        if (CheckIfExited() && !myHasOpenedDoor)
        {
            EventHandler.current.ButtonUpEvent();
            myHasOpenedDoor = true;
        }
    }

    private bool CheckIfExited()
    {
        if (myIsActivated && !myIncomingLaserCollider.gameObject.activeInHierarchy)
        {
            myIsActivated = false;
            return true;
        }
        return false;
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (myCoords == aPlayerCurrentPos);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }
}
