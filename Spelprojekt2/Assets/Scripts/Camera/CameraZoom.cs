using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myDesiredRotation;
    private Vector3 myOriginalPosition;
    private Vector3 myOriginalRotation;
    private float mySpeed = 0.0015f;
    private float myFastSpeed = 0.05f;
    private float lerpSpeed = 0.0f;
    private bool myZoomIn = false;
    private bool myZoomOut = true;
    private bool mySpeedUp = false;
    private PlayerMovement myPlayerMovement;

    private void Start()
    {
        myPlayerMovement = FindObjectOfType<PlayerMovement>();
        myPlayerMovement.enabled = false;
        myOriginalPosition = transform.position;
        myOriginalRotation = transform.eulerAngles;
        myDesiredPosition = new Vector3(3f, 13f, -2f);
        myDesiredRotation = new Vector3(64.5f, 0f, 0f);
    }
    private void Update()
    {
        if (!myZoomIn && !myZoomOut)
        {
            if (Input.GetKeyDown(KeyCode.Z)) myZoomIn = true;
            if (Input.GetKeyDown(KeyCode.X)) myZoomOut = true;
        }

        if (myZoomOut)
        {
            if (!IsFinished(myDesiredPosition, myDesiredRotation, 0.05f)) 
                Zoom(myDesiredPosition, myDesiredRotation);
            else
            {
                myZoomOut = false;
                Lock(true);
            }
        }
        if (myZoomIn)
        {
            if (!IsFinished(myOriginalPosition, myOriginalRotation, 0.05f))
                Zoom(myOriginalPosition, myOriginalRotation);
            else
            {
                myZoomIn = false;
                Lock(true);
            }
        }
    }

    private void Zoom(Vector3 aDestinationPos, Vector3 aDestinationRot)
    {
        if (Input.touchCount > 0)
            mySpeedUp = true;

        if (mySpeedUp)
            lerpSpeed += myFastSpeed * Time.deltaTime;
        else
            lerpSpeed += mySpeed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, aDestinationPos, lerpSpeed);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, aDestinationRot, lerpSpeed);
    }

    private bool IsFinished(Vector3 aDestinationPos, Vector3 aDestinationRot, float offset)
    {
        if (Mathf.Abs(transform.position.x - aDestinationPos.x) < offset &&
            Mathf.Abs(transform.position.y - aDestinationPos.y) < offset &&
            Mathf.Abs(transform.position.z - aDestinationPos.z) < offset &&
            Mathf.Abs(transform.eulerAngles.x - aDestinationRot.x) < offset &&
            Mathf.Abs(transform.eulerAngles.y - aDestinationRot.y) < offset &&
            Mathf.Abs(transform.eulerAngles.z - aDestinationRot.z) < offset)
        {
            return true;
        }
        return false;
    }

    private void Lock(bool aValue)
    {
        myPlayerMovement.enabled = aValue;
        lerpSpeed = 0;
    }
}
