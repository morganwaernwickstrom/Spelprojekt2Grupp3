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

    // --- Laser Object pool --- //
    public GameObject myLaser;
    private List<GameObject> myLaserPool;
    private int myAmountOfLasers = 20;

    // --- Distances to draw laser with --- //
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

    // Coordinates to use for collision checking
    private Coord myCoords;

    void Start()
    {
        myLaserPool = new List<GameObject>();

        for (int i = 0; i < myAmountOfLasers; ++i)
        {
            GameObject temp = Instantiate(myLaser);
            temp.SetActive(false);
            myLaserPool.Add(temp);
        }

        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    // --- Every frame the reflector checks if it is hit by a laser and if so do everything needed for laser to go the correct way --- //
    private void Update()
    {
        bool leftHit = myLeftDetectionBox.myIsHit;
        bool rightHit = myRightDetectionBox.myIsHit;
         
        myLeftDetectionBox.CheckIfExited();
        myRightDetectionBox.CheckIfExited();

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

            if ((myPreviousLaserDistance != myLaserDistance))     // Only draw laser if the distance has changed
            {
                DrawLaser();
            }
        }
        else
        {
            CheckDistance();
            myDirection = Direction.Null;
            ClearLaser();
        }
    }

    // --- Draws the laser based on information gathered from Raycast and more in Update function --- //
    private void DrawLaser()
    {
        ClearLaser();       
        int amount = (int)Mathf.Round(myLaserDistance);

        if ((myLaserRotation == myLeftLaserRotation || myLaserRotation == myRightLaserRotation))
        {
            for (int count = 0; count < amount; ++count)
            {
                myLaserPool[count].SetActive(true);
                myLaserPool[count].transform.position = myOrigin.position;
                myLaserPool[count].transform.rotation = myLaserRotation.rotation;
                myOrigin.Translate(Vector3.forward * 1, Space.Self);
            }
        }
    }

    private void ClearLaser()
    {
        foreach (GameObject laser in myLaserPool)
        {
            laser.SetActive(false);
        }
    }

    private void CheckDistance()
    {
        // --- Layermask for raycast to ignore collision with laser-layer --- //
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;

        if (myIsHit)
        {
            // --- If reflector is hit by a laser then send out a raycast to determine length to draw laser with --- //
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
        else
        {
            myLaserDistance = 0f;
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (myCoords == aPlayerCurrentPos);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }
}
