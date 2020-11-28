using System.Collections.Generic;
using UnityEngine;

public class LaserEmitterScript : MonoBehaviour
{
    // --- Origin points for raycast and laser objects --- //
    [SerializeField] private Transform myRaycastOrigin;
    [SerializeField] private Transform myOrigin;
    [SerializeField] private Transform myFirstOrigin;

    // --- Laser Rotation after the way emitter points --- //
    [SerializeField] private Transform myLaserRotation;

    // --- Laser Object pool --- //
    public GameObject myLaser;
    private List<GameObject> myLaserPool = null;
    private int myAmountOfLasers = 20;

    // --- Distances to draw laser with --- //
    [SerializeField] float myPreviousLaserDistance = 0;
    [SerializeField] float myLaserDistance = 0;

    // Coordinates to use for collision checking
    private Coord myCoords;

    private void Start()
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
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
    }

    private void UpdateLaser()
    {
        myPreviousLaserDistance = myLaserDistance;
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

        int amount = Mathf.RoundToInt(myLaserDistance);

        for (int count = 0; count < amount; ++count)
        {
            myLaserPool[count].SetActive(true);
            myLaserPool[count].transform.position = myOrigin.position;
            myLaserPool[count].transform.rotation = myLaserRotation.rotation;
            myOrigin.Translate(Vector3.forward * 1, Space.Self);
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
        Coord direction = new Coord(0, 0);

        if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 0)
        {
            direction.x = 0;
            direction.y = 1;
        }
        else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 90)
        {
            direction.x = 1;
            direction.y = 0;
        }
        else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == -180 || Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 180)
        {
            direction.x = 0;
            direction.y = -1;
        }
        else if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == -90 || Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 270)
        {
            direction.x = -1;
            direction.y = 0;
        }
        myLaserDistance = TileMap.Instance.GetDistance(myCoords, direction);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        UpdateLaser();
        return false;
    }

    private bool OnRockMove(Coord aRockPos)
    {
        if (TileMap.Instance.Get(aRockPos) == eTileType.Laser)
        {
            DrawLaser();
        }
        UpdateLaser();
        return false;
    }
    public Coord GetCoords()
    {
        return myCoords;
    }
    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
    }

    public Coord GetCoords()
    {
        return myCoords;
    }
}