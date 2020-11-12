using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 myDesiredPosition;

    [SerializeField]
    float mySpeed;

    private void Awake()
    {
        myDesiredPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);

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

    }
}
