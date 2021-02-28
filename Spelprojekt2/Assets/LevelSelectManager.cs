using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject myRightArrow;

    [SerializeField]
    private GameObject myLeftArrow;

    [SerializeField]
    GameObject[] myCanvases;

    private int myCanvasIndex = 0;
    private const int myCanvasIndexMax = 1;

    private void Start()
    {
        myCanvases = new GameObject[2];
        myCanvases[0] = GameObject.Find("Levels1");
        myCanvases[1] = GameObject.Find("Levels2");

        myRightArrow = GameObject.Find("Right Arrow");
        myLeftArrow = GameObject.Find("Left Arrow");

        myLeftArrow.SetActive(false);
        myRightArrow.SetActive(true);

        myCanvases[0].SetActive(true);
        myCanvases[1].SetActive(false);
    }

    public void GoRight()
    {
        if (myCanvasIndex < myCanvasIndexMax)
        {
            myCanvases[myCanvasIndex++].SetActive(false);
            myCanvases[myCanvasIndex].SetActive(true);
        }

        HandleArrows();
    }

    public void GoLeft()
    {
        if (myCanvasIndex > 0)
        {
            myCanvases[myCanvasIndex--].SetActive(false);
            myCanvases[myCanvasIndex].SetActive(true);
        }

        HandleArrows();
    }

    private void HandleArrows()
    {
        myLeftArrow.SetActive(true);
        myRightArrow.SetActive(true);

        if (myCanvasIndex == 0)
        {
            myLeftArrow.SetActive(false);
        }
        else if (myCanvasIndex == myCanvasIndexMax)
        {
            myRightArrow.SetActive(false);
        }
    }
}