using UnityEngine;

public class LaserOnTile : MonoBehaviour
{
    LineRenderer myLineRenderer = null;

    [ExecuteAlways]
    private void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();

        Vector3 firstPos = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        Vector3 secondPos = new Vector3(transform.position.x + 5, transform.position.y + 0.25f, transform.position.z);
        myLineRenderer.useWorldSpace = true;
        myLineRenderer.SetPosition(0, firstPos);
        myLineRenderer.SetPosition(1, secondPos);
    }
}
