using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            float newZ = transform.position.z + 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float newZ = transform.position.z - 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            float newX = transform.position.x - 1f;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            float newX = transform.position.x + 1f;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
