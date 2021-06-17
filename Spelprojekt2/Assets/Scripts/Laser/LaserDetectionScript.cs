using UnityEngine;

public class LaserDetectionScript : MonoBehaviour
{
    public bool myIsHit = false;
    [SerializeField] private Collider myIncomingLaserCollider = null;
    [SerializeField] private Collider myIncomingCollider = null;
    [SerializeField] private LaserDetectionScript myOtherSide = null;

    Vector3 myPreviousPosition;
    Vector3 myPosition;

    private void Start()
    {
        myPosition = transform.position;
        myPreviousPosition = myPosition;
    }

    private void Update()
    {
        myPreviousPosition = myPosition;
        myPosition = transform.position;
    }

    private void OnTriggerEnter(Collider anOther)
    {
        myIncomingCollider = anOther;

        // --- Make only one side be the "hit" side, ie. the side hit by the laser --- //
        if (!myOtherSide.myIsHit && myPreviousPosition == myPosition)
        {
            if (anOther.CompareTag("Laser") || anOther.CompareTag("Emitter"))
            {
                myIsHit = true;
                myIncomingCollider = anOther;
                myIncomingLaserCollider = anOther;
            }
        }
    }

    private void OnTriggerStay(Collider anOther)
    {        
        myIncomingCollider = anOther;

        if (!myOtherSide.myIsHit && myPreviousPosition == myPosition)
        {
            if (anOther.CompareTag("Laser") || anOther.CompareTag("Emitter"))
            {
                myIsHit = true;
                myIncomingCollider = anOther;
                myIncomingLaserCollider = anOther;
            }
        }
    }

    // --- OnTriggerExit doesn't work so this substitutes it --- //
    public void CheckIfExited()
    {
        if (myIsHit && !myIncomingCollider.CompareTag("Laser") && !myIncomingLaserCollider.gameObject.activeInHierarchy)
        {
            myIsHit = false;
        }
        else if (myPreviousPosition != myPosition)
        {
            myIsHit = false;
        }
    }
}
