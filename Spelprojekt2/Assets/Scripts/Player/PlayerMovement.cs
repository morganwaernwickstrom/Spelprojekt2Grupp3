using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Coord myCoords;
    private Coord myPreviousCoords;

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
        Movement();
    }

    private void Movement()
    {
        Coord originalCoord = myCoords;

        myPreviousCoords = myCoords;
        if (Input.GetKeyDown(KeyCode.W))
        {
            myDesiredPosition += new Vector3(0, 0, 1);
            myCoords.y += 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            myDesiredPosition += new Vector3(0, 0, -1);
            myCoords.y -= 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            myDesiredPosition += new Vector3(-1, 0, 0);
            myCoords.x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            myDesiredPosition += new Vector3(1, 0, 0);
            myCoords.x += 1;
        }

        if (EventHandler.current.PlayerMoveEvent(myCoords, myPreviousCoords))
        {
            myDesiredPosition = transform.position;
            myCoords = originalCoord;
        }
        myDesiredPosition = new Vector3(Mathf.Round(myDesiredPosition.x), myDesiredPosition.y, Mathf.Round(myDesiredPosition.z));
    }
}
