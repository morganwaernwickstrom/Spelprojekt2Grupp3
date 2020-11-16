using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitterScript : MonoBehaviour
{
    // --- Origin points for raycast and laser objects --- //
    [SerializeField] private Transform myRaycastOrigin;
    [SerializeField] private Transform myOrigin;
    [SerializeField] private Transform myFirstOrigin;

    // --- Laser Rotation, reference and list to hold all lasers in --- //
    [SerializeField] private Transform myLaserRotation;
    [SerializeField] private GameObject myLaser;
    private List<GameObject> myLasers = new List<GameObject>();

    // --- Distances to draw laser with --- //
    [SerializeField] float myPreviousLaserDistance = 0;
    [SerializeField] float myLaserDistance = 0;

    // --- Every frame, check distance to ray-casted object, if it has changed, draw the laser --- //
    private void Update()
    {
        CheckDistance();

        if (myPreviousLaserDistance != myLaserDistance)
        {
            DrawLaser();
        }
    }

    // --- Draws the laser based on information gathered from Raycast and more in CheckDistance function --- //
    private void DrawLaser()
    {
        myOrigin.position = myFirstOrigin.position;
        ClearLaser();

        int amount = (int)myLaserDistance;

        if (myLaserDistance > 0.9f && myLaserDistance < 1f)
        {
            amount = 1;
        }

        if (amount > 0)
        {
            for (int count = 1; count <= amount; ++count)
            {
                myLasers.Add(Instantiate(myLaser, myOrigin.position, myLaserRotation.rotation));
                myOrigin.Translate(Vector3.forward * 1, Space.Self);
            }
        }

    }
    private void ClearLaser()
    {
        // --- Go through list of laser-objects and destroy them before re-drawing the laser --- //
        if (myLasers.Count > 0)
        {
            foreach (GameObject laser in myLasers)
            {
                Destroy(laser);
            }
        }

        myLasers.Clear();
    }   

    private void CheckDistance()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        myPreviousLaserDistance = myLaserDistance;

        RaycastHit hit;

        if (Physics.Raycast(myRaycastOrigin.position, myRaycastOrigin.forward, out hit, Mathf.Infinity, layerMask))
        {
            myLaserDistance = hit.distance;
        }
        else
        {
            myLaserDistance = 0f;
        }
    }
}