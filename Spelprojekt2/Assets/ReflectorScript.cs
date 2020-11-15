using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorScript : MonoBehaviour
{
    enum Direction
    {
        Null,
        Left,
        Right
    }

    // --- Laser Object Reference & Distances --- //
    [SerializeField] private GameObject myLaser;
    private List<GameObject> myLasers = new List<GameObject>();
    private float myPreviousLaserDistance = 0;
    private float myLaserDistance = 0;

    // --- Transforms used to determine where raycasts should originate from --- //
    [SerializeField] private Transform myRaycastOriginLeft;
    [SerializeField] private Transform myRaycastOriginRight;

    // --- Origin points for lasers --- //
    [SerializeField] private Transform myOrigin;            // The moving origin point from which lasers instantiate. Both myOrigin and myFirstOrigin depend on myLeftOrigin & myRighOrigin
    [SerializeField] private Transform myFirstOrigin;       // Is at the same position as myOrigin at the first laser object, used to see where laser should start at Re-draw
    [SerializeField] private Transform myLeftOrigin;        // The static/not moving origin point for laser going left
    [SerializeField] private Transform myRightOrigin;       // The static/not moving origin point for laser going right
   
    [SerializeField] private Transform myLeftLaserRotation;     // Rotation for instantiated laser objects going left
    [SerializeField] private Transform myRightLaserRotation;    // Rotation for instantiated laser objects going right
    private Transform myLaserRotation;                          // Is assigned to be either myLeftLaserRotation or myRighLaserRotation
   
    // --- Detection boxes to determine where the laser is coming from --- //
    [SerializeField] private LaserDetectionScript myLeftDetectionBox;
    [SerializeField] private LaserDetectionScript myRightDetectionBox;

    // --- Is the reflector hit by laser as well as which direction it should take --- //
    [SerializeField] private bool myIsHit;
    private Direction myDirection = Direction.Null;

    // --- Draws the laser based on information gathered from Raycast and more in Update function --- //
    private void DrawLaser()
    {
        ClearLaser();

        int amount = (int)myLaserDistance;

        if (myLaserDistance > 0.9f && myLaserDistance < 1f)
        {
            amount = 1;
        }

        if (amount > 0 && (myLaserRotation == myLeftLaserRotation || myLaserRotation == myRightLaserRotation))
        {
            for (int count = 1; count <= amount; ++count)
            {
                myLasers.Add(Instantiate(myLaser, myOrigin.position, myLaserRotation.rotation));
                myOrigin.Translate(Vector3.forward * 1, Space.Self);
            }
        }

    }

    // --- Every frame the reflector checks if it is hit my laser and if so do everything needed for laser to go the correct way --- //
   
    // Can probably be made more performance friendly and only check at specific times instead of every frame...
    private void Update()
    {
        bool leftHit = myLeftDetectionBox.myIsHit;
        bool rightHit = myRightDetectionBox.myIsHit;

        myIsHit = (leftHit || rightHit) ? true : false;

        myPreviousLaserDistance = myLaserDistance;

        if (myIsHit)
        {
            if (myLeftDetectionBox.myIsHit)
            {
                myFirstOrigin.position = myRightOrigin.position;
                myFirstOrigin.rotation = myRightOrigin.rotation;
                myLaserRotation = myRightLaserRotation;
                myDirection = Direction.Right;
            }
            else if (myRightDetectionBox.myIsHit)
            {
                myFirstOrigin.position = myLeftOrigin.position;
                myFirstOrigin.rotation = myLeftOrigin.rotation;
                myLaserRotation = myLeftLaserRotation;
                myDirection = Direction.Left;
            }

            myOrigin.position = myFirstOrigin.position;
            myOrigin.rotation = myFirstOrigin.rotation;

            CheckDistance();

            if (myPreviousLaserDistance != myLaserDistance)     // Only draw laser if the distance has changed
            {
                DrawLaser();
                Debug.Log("Drawing laser");
            }
        }
        else
        {
            myDirection = Direction.Null;
        }
    }   
    
    private void ClearLaser()
    {
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

        RaycastHit hit;

        if (myDirection == Direction.Left)
        {
            if (Physics.Raycast(myRaycastOriginLeft.position, myRaycastOriginLeft.forward, out hit, Mathf.Infinity, layerMask))
            {
                myLaserDistance = hit.distance;
            }
            else
            {
                myLaserDistance = 0f;
            }
        }
        else if (myDirection == Direction.Right)
        {
            if (Physics.Raycast(myRaycastOriginRight.position, myRaycastOriginRight.forward, out hit, Mathf.Infinity, layerMask))
            {
                myLaserDistance = hit.distance;
            }
            else
            {
                myLaserDistance = 0f;
            }
        }
    }
}
