using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverScript : MonoBehaviour
{
    //public GameObject myConnectedObject;
    public bool myIsActivated = false;
    private Collider myIncomingLaserCollider;

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
}
