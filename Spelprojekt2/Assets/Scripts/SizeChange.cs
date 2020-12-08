using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour
{
    int myDirection = 1;
    float myStartX;

    private void Start()
    {
        myStartX = transform.localScale.x;
    }

    void Update()
    {
        transform.localScale += new Vector3(2f, 2f, 2f) * myDirection * Time.deltaTime;

        if (transform.localScale.x >= myStartX * 1.05f)
        {
            myDirection *= -1;
        }
        else if (transform.localScale.x <= myStartX * 0.9f)
        {
            myDirection *= -1;
        }
    }
}
