using UnityEngine;

public class ReceiverScript : MonoBehaviour
{
    public bool myIsActivated = false;
    private bool myHasOpenedDoor = false;
    private Collider myIncomingLaserCollider;

    public GameObject myLaserObject;
    [SerializeField] private GameObject myObject;
    private Animator myAnimator;

    // Coordinates to use for collision checking
    private Coord myCoords;

    private void Start()
    {
        myAnimator = myObject.GetComponent<Animator>();
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

        myLaserObject.SetActive(myIsActivated);
        myAnimator.SetBool("Hit", myIsActivated);
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
    public Coord GetCoords()
    {
        return myCoords;
    }
    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }
}
