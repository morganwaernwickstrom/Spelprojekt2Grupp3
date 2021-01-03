using UnityEngine;

public class FairySin : MonoBehaviour
{
    float offset = 0.001f;
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += Mathf.Sin(Time.time) * offset;
        transform.position = pos;
    }
}
