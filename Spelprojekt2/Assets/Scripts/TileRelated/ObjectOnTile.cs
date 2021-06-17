using UnityEngine;

public class ObjectOnTile : MonoBehaviour
{
    public void Remove()
    {
        DestroyImmediate(gameObject, true);
    }

    public void Rotate()
    {
        gameObject.transform.Rotate(0, 90, 0, Space.World);
    }
}
