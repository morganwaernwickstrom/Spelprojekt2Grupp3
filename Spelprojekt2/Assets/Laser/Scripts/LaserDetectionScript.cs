using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetectionScript : MonoBehaviour
{
    public bool myIsHit = false;
    private Collider myIncomingLaserCollider;
    [SerializeField] private LaserDetectionScript myOtherSide;

    private void OnTriggerEnter(Collider anOther)
    {
        // --- Make only one side be the "hit" side, ie. the side hit by the laser --- //
        if (!myOtherSide.myIsHit)
        {
            if (anOther.CompareTag("Laser"))
            {
                myIsHit = true;
                myIncomingLaserCollider = anOther;
            }
        }
    }

    // --- OnTriggerExit doesn't work so this substitutes it --- //
    public void CheckIfExited()
    {
        if (myIsHit && !myIncomingLaserCollider)
        {
            myIsHit = false;
        }
    }
}
