using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetectionScript : MonoBehaviour
{
    public bool myIsHit = false;
    public Collider myIncomingLaserCollider;
    [SerializeField] private LaserDetectionScript myOtherSide;

    private void OnTriggerEnter(Collider anOther)
    {
        if (!myOtherSide.myIsHit)
        {
            if (anOther.CompareTag("Laser"))
            {
                myIsHit = true;
                


                myIncomingLaserCollider = anOther;
            }
        }
    }
    public void CheckIfExited()
    {
        if ((myIsHit || !myIsHit) && !myIncomingLaserCollider)
        {
            myIsHit = false;
            Debug.Log(gameObject.name + " not hit");
        }
    }
}
