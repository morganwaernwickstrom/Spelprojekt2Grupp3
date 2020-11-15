using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetectionScript : MonoBehaviour
{
    public bool myIsHit = false;
    public bool myIsLocked = false;

    [SerializeField] private LaserDetectionScript myOtherSide;

    private void OnTriggerEnter(Collider anOther)
    {
        if (!myOtherSide.myIsHit)
        {
            if (anOther.CompareTag("Laser") && !myIsLocked)
            {
                myIsHit = true;
                myIsLocked = true;
            }
        }
        else
        {
            myIsLocked = true;
        }       
    }

    private void OnTriggerExit(Collider anOther)
    {
        if (anOther.CompareTag("Laser"))
        {
            myIsHit = false;
            myIsLocked = false;
        }
    }
}
