using UnityEngine;

public class ReceiverScript : MonoBehaviour
{
    //public GameObject myConnectedObject;
    public bool myIsActivated = false;
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
            //myConnectedObject.Open();
            myIsActivated = true;
            myIncomingLaserCollider = anOther;
            Debug.Log("OPENED :" + myIsActivated);
        }
    }

    void Update()
    {
        if (myIsActivated && !myIncomingLaserCollider)
        {
            myIsActivated = false;
            Debug.Log("CLOSED :" + myIsActivated);
        }
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
