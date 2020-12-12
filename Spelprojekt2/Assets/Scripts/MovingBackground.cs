using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] GameObject[] myImages = new GameObject[5];
    private Vector3 myDefaultPosition = new Vector3(0, 0, 0);
    private Vector3 myMovePosition = new Vector3(0, 0, 0);
    public float mySpeed = 500;

    private void Awake()
    {
        myMovePosition = myImages[0].transform.position;
        myDefaultPosition = myImages[4].transform.position;
    }

    private void Start()
    {
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
            if (image.transform.position.y >= myMovePosition.y)
            {
                image.transform.position = myDefaultPosition;
            }
        }
    }
}
