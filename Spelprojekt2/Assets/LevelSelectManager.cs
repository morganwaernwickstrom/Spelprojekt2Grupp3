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
    private const int myCanvasIndexMax = 3;

    private void Start()
    {
        myCanvases = new GameObject[4];
        myCanvases[0] = GameObject.Find("TutorialLevels");
        myCanvases[1] = GameObject.Find("Levels1");
        myCanvases[2] = GameObject.Find("Levels2");
        myCanvases[3] = GameObject.Find("Levels3");

        myRightArrow = GameObject.Find("Right Arrow");
        myLeftArrow = GameObject.Find("Left Arrow");

        myLeftArrow.SetActive(false);
        myRightArrow.SetActive(true);

        myCanvases[0].SetActive(true);
        myCanvases[1].SetActive(false);
        myCanvases[2].SetActive(false);
        myCanvases[3].SetActive(false);
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