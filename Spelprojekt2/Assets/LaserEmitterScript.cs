using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitterScript : MonoBehaviour
{
    [SerializeField] private Transform myRaycastOrigin;
    [SerializeField] private Transform myOrigin;
    [SerializeField] private Transform myFirstOrigin;

    [SerializeField] private Transform myLaserRotation;
    [SerializeField] private GameObject myLaser;

    [SerializeField] List<GameObject> myLasers;
    [SerializeField] float myPreviousLaserDistance = 0;
    [SerializeField] float myLaserDistance = 0;

    private void DrawLaser()
    {
        myOrigin.position = myFirstOrigin.position;

        if (myLasers.Count > 0)
        {
            foreach (GameObject laser in myLasers)
            {
                Destroy(laser);
            }
        }

        myLasers.Clear();
       
        int amount = (int)myLaserDistance;

        if (myLaserDistance > 0.9f && myLaserDistance < 1f)
        {
            amount = 1;        }
        
        Debug.LogError("Distance: " + myLaserDistance);
        Debug.LogError("Amount: " + amount);

        if (amount > 0)
        {
            for (int count = 1; count <= amount; ++count)
            {
                myLasers.Add(Instantiate(myLaser, myOrigin.position, myLaserRotation.rotation));
                myOrigin.Translate(Vector3.forward * 1, Space.Self);
            }
        }
        
    }

    private void Update()
    {
        myPreviousLaserDistance = myLaserDistance;

        RaycastHit hit;

        if (Physics.Raycast(myRaycastOrigin.position, myRaycastOrigin.forward, out hit, Mathf.Infinity))
        {
            myLaserDistance = hit.distance;
        }
        else
        {
            //myLaserDistance = myPreviousLaserDistance;
            myLaserDistance = 0f;
        }

        if (myPreviousLaserDistance != myLaserDistance)
        {
            DrawLaser();
        }
    }
}