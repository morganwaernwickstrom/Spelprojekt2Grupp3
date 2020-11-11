using UnityEngine;

public class ObjectOnTile : MonoBehaviour
{
    public void Remove()
    {
        DestroyImmediate(gameObject);
    }
}
