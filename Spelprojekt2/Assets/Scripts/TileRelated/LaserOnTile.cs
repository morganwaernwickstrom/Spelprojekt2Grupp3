using UnityEngine;

public class LaserOnTile : MonoBehaviour
{
    LineRenderer myLineRenderer = null;

    private void Awake()
    {
        Vector3 newPos = new Vector3(transform.position.x +9, transform.position.y, transform.position.x);
        myLineRenderer = GetComponent<LineRenderer>();
        myLineRenderer.startColor = Color.red;
        myLineRenderer.useWorldSpace = true;

        myLineRenderer.SetPosition(0, transform.position);
        myLineRenderer.SetPosition(1, newPos);
    }
}
