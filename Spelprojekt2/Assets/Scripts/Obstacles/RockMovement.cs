using UnityEngine;

public class RockMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private float mySpeed = 0.1f;
    
    private Coord myPlayerCoords;
    private Coord myPlayerLastCoords;
    private Coord myCoords;

    private void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        FindObjectOfType<PlayerMovement>().moveEvent += Move;
    }

    private void Move()
    {
        myPlayerCoords = FindObjectOfType<PlayerMovement>().GetCoord();
        if (myCoords == myPlayerCoords)
        {
            if (myPlayerLastCoords.x == myCoords.x - 1)
            {
                myDesiredPosition += new Vector3(1, 0, 0);
                myCoords.x += 1;
            }
            if (myPlayerLastCoords.x == myCoords.x + 1)
            {
                myDesiredPosition += new Vector3(-1, 0, 0);
                myCoords.x -= 1;
            }
            if (myPlayerLastCoords.y == myCoords.y - 1)
            {
                myDesiredPosition += new Vector3(0, 0, 1);
                myCoords.y += 1;
            }
            if (myPlayerLastCoords.y == myCoords.y + 1)
            {
                myDesiredPosition += new Vector3(0, 0, -1);
                myCoords.y -= 1;
            }
        }
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);
        myPlayerLastCoords = myPlayerCoords;
    }

    private void OnDestroy()
    {
        if (FindObjectOfType<PlayerMovement>())
        {
            FindObjectOfType<PlayerMovement>().moveEvent -= Move;
        }
    }
}
