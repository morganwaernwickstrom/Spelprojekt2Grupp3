using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action moveEvent;
    Vector3 myDesiredPosition;
    private Coord myCoords;

    [SerializeField]
    float mySpeed = 0.1f;

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);
        myCoords.x = (int)Mathf.Round(transform.position.x);
        myCoords.y = (int)Mathf.Round(transform.position.z);
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            myDesiredPosition += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            myDesiredPosition += new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            myDesiredPosition += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            myDesiredPosition += new Vector3(1, 0, 0);
        }

        moveEvent?.Invoke();
    }

    public Coord GetCoord()
    {
        return myCoords;
    }
}
