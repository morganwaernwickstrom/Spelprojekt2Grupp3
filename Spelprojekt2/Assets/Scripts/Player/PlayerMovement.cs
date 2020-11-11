using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int myWidth = 0;
    private int myHeight = 0;

    private void Start()
    {
        myWidth = GameObject.FindGameObjectWithTag("TileEditor").GetComponent<TileEditor>().MyWidth;
        myHeight = GameObject.FindGameObjectWithTag("TileEditor").GetComponent<TileEditor>().MyHeight;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //if (transform.position.z <= myHeight)
            //{
            //    return;
            //}
            float newZ = transform.position.z + 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //if (transform.position.z <= 0)
            //{
            //    return;
            //}
            float newZ = transform.position.z - 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //if (transform.position.x <= 0)
            //{
            //    return;
            //}
            float newX = transform.position.x - 1f;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //if (transform.position.z <= myWidth)
            //{
            //    return;
            //}
            float newX = transform.position.x + 1f;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
