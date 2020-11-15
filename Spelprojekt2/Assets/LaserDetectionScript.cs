using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetectionScript : MonoBehaviour
{
    public bool myIsHit = false;
    public Collider myIncomingLaserCollider;
    [SerializeField] private LaserDetectionScript myOtherSide;

    private void OnTriggerStay(Collider anOther)
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
    private void CheckIfExited()
    {
        // funkar inte, först blir detection box 1 "hit" och sen byter den med detection box 2. Fattar nada
        if (myIsHit && !myIncomingLaserCollider)
        {
            myIsHit = false;
            Debug.Log(gameObject.name + " not hit anymore");
        }
    }
    
    float timer = 1f;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            CheckIfExited();
            timer = 1f;
        }
    }
}
