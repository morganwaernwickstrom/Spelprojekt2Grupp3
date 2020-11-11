using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject myTile = null;
    [SerializeField]
    private Material myFirstMat = null;
    [SerializeField]
    private Material mySecondMat = null;

    private int myWidth;
    private int myHeight;

    private List<GameObject> myTileContainer;

    public int MyHeight { get => myHeight; set => myHeight = value; }
    public int MyWidth { get => myWidth; set => myWidth = value; }
    private void OnValidate()
    {
        if (FindObjectOfType<Tile>() != null)
        {
            myTileContainer = new List<GameObject>(FindObjectsOfType<Tile>().Length);
            Tile[] temp = FindObjectsOfType<Tile>();

            for (int i = 0; i < temp.Length; i++)
            {
                myTileContainer.Add(temp[i].gameObject);
            }
        }
        else
        {
            myTileContainer = new List<GameObject>(myHeight * myWidth);
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < myTileContainer.Count - 1; i++)
        {
            Destroy(myTileContainer[i].gameObject);
        }
    }
    public void GenerateTiles()
    {
        ClearTiles();
        for (int i = 0; i < MyHeight; i++)
        {
            for (int j = 0; j < MyWidth; j++)
            {
                if (i % 2 != 0 && j % 2 != 0)
                {
                    myTile.gameObject.GetComponent<Renderer>().material = myFirstMat;
                }
                else if (i % 2 == 0 && j % 2 != 0)
                {
                    myTile.gameObject.GetComponent<Renderer>().material = mySecondMat;
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    myTile.gameObject.GetComponent<Renderer>().material = myFirstMat;
                }
                else
                {
                    myTile.gameObject.GetComponent<Renderer>().material = mySecondMat;
                }
                Vector3 pos = new Vector3(i, 0, j);
                myTileContainer.Add(Instantiate(myTile, pos, transform.rotation, transform));
            }
        }
    }

    public void ClearTiles()
    {
        foreach (GameObject tile in myTileContainer)
        {
            DestroyImmediate(tile.gameObject);
        }
    }
}
