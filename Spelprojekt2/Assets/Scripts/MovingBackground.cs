using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] GameObject[] myImages = new GameObject[4];
    private Vector3 myDefaultPosition = new Vector3(0, 0, 0);
    public float mySpeed = 500;

    private void Start()
    {
        myDefaultPosition = myImages[3].transform.position;
        mySpeed = 100;
        Time.timeScale = 1;
    }

    private void Update()
    {
        foreach (GameObject image in myImages)
        {
            image.transform.Translate(Vector3.up * mySpeed * Time.deltaTime);
        }

        foreach (GameObject image in myImages)
        {
            if (image.transform.position.y >= 3655)
            {
                image.transform.position = myDefaultPosition;
            }
        }

        
    }
}
